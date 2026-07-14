# Ant Design Blazor：AI 项目工作指南

## 1. 项目定位

Ant Design Blazor 是基于 Ant Design 视觉规范实现的 Blazor UI 组件库。核心产物是 `components/AntDesign.csproj` 对应的 `AntDesign` Razor 类库，同时仓库还包含文档站、文档生成工具、本地化扩展、测试应用和自动化测试。

核心类库当前实际面向 `netstandard2.1` 以及 `net5.0` 到 `net10.0`。不要只依据 README 中的历史版本描述判断兼容范围，应以项目文件中的 `TargetFrameworks` 和条件编译为准。

## 2. 目录与职责

| 路径 | 职责 | 修改时重点 |
| --- | --- | --- |
| `components/` | `AntDesign` Razor 组件库；按组件名分目录 | `.razor`、`.razor.cs`、公共参数、生命周期和渲染行为 |
| `components/core/JsInterop/` | 浏览器侧 TypeScript 互操作及 .NET 调用常量 | 事件注册/注销必须成对，注意组件释放、多实例和全局监听 |
| `components/style/` | 组件样式源文件 | 样式改动需要经过前端构建链，不要只检查 C# 编译 |
| `site/AntDesign.Docs/` | 文档共享 UI、组件 Demo 和站点静态内容 | Demo 同时承担用法说明和人工回归入口 |
| `site/AntDesign.Docs.Build/` | 文档构建编排 | Debug 构建会运行 Gulp 和文档 CLI，并复制/生成站点资源 |
| `site/AntDesign.Docs.Build.CLI/` | Demo、菜单、Markdown/API 元数据生成工具 | 翻译元数据变更要确认是人工输入还是生成流程结果 |
| `site/AntDesign.Docs.Server/`、`Wasm/`、`WebApp/` | 文档站的不同宿主 | 本地默认 `npm start` 启动 Server 宿主（`net10.0`） |
| `site/AntDesign.Docs.MCP/` | 文档元数据的 MCP 发布产物 | 文档生成会把 meta JSON 复制到其数据目录 |
| `src/AntDesign.Extensions.Localization/` | 独立的本地化扩展包 | 与核心组件库分开打包，也采用多目标框架 |
| `tests/AntDesign.Tests/` | Razor/bUnit 风格的 xUnit 组件测试 | 优先在对应组件目录补渲染、状态和交互测试 |
| `tests/AntDesign.Tests.Js/` | TypeScript/Mocha/jsdom 测试 | JS 事件、DOM、上传等互操作改动必须关注此处 |
| `tests/AntDesign.TestApp/` | 手工验证与集成测试宿主 | 浏览器行为无法由单元测试覆盖时用于复现 |
| `docs/` | 被文档构建复制进站点的 Markdown 资料 | `DOCUMENTATION.md` 说明 API 文档和 FAQ 生成约定 |

## 3. 关键实现链路

### 3.1 普通组件

通常由同目录中的 `.razor` 负责标记和渲染，`.razor.cs` 负责参数、状态与生命周期。公共 API 以 `[Parameter]`、公共方法和配置对象为主。修改时应同时检查：

- 参数首次赋值与后续更新是否都生效；
- `OnInitialized`、`OnParametersSet`、`OnAfterRenderAsync` 的调用时机；
- `StateHasChanged` 是否运行在渲染器同步上下文；
- 泛型、级联参数和子组件引用在首次渲染前是否可能为空；
- 条件编译是否让不同目标框架产生不同行为。

### 3.2 JavaScript 互操作

典型调用链如下：

`Razor/C# 组件` → `JSInteropConstants` → `components/core/JsInterop/modules` 中的 TypeScript → 浏览器 DOM/API → `[JSInvokable]` 回调。

新增或修改 DOM 监听时，至少检查：

- 注册和注销使用的是不是同一个函数引用；
- 重渲染、禁用切换和组件销毁会不会重复注册；
- 多个组件实例是否共享或争抢 `document`/`window` 级事件；
- `DotNetObjectReference`、对象 URL、观察器和全局事件是否释放；
- TypeScript 构建产物是否通过 `npm run build:lib` 重新生成。

### 3.3 文档生成

站点 API 文档主要来自组件代码中的 XML 注释和文档特性，组件示例位于 `site/AntDesign.Docs/Demos/Components/<组件>/demo/`。`site/AntDesign.Docs.Build` 在 Debug 构建中调用 CLI 生成 Demo、菜单和 Markdown/API 元数据。

因此，公共 API 变更不能只修改 C#：还要检查 XML 注释、对应 Demo、翻译元数据以及生成后的站点表现。详细格式见根目录 `DOCUMENTATION.md`。

## 4. 常用命令

首次准备环境：

```powershell
npm install
dotnet build .\site\AntDesign.Docs.Build\AntDesign.Docs.Build.csproj
```

启动默认文档站：

```powershell
npm start
```

按改动范围验证：

```powershell
# 核心库或解决方案编译
dotnet build .\components\AntDesign.csproj -f net10.0
dotnet build .\AntDesign.sln

# C#/Razor 组件测试；CI 会分别覆盖 net6、net8、net9、net10.0
dotnet test .\tests\AntDesign.Tests\AntDesign.Tests.csproj -c Release -f net10.0

# TypeScript 代码风格与构建
npm run lint
npm run build:lib

# JS/TS 测试
npm install .\tests\AntDesign.Tests.Js
npm --prefix .\tests\AntDesign.Tests.Js run test-cov
```

文档或 Demo 变更应额外构建：

```powershell
dotnet build .\site\AntDesign.Docs.Build\AntDesign.Docs.Build.csproj
```

如果只修改文档，不必机械运行全套测试；如果修改公共组件、跨目标框架代码或 JS 互操作，应扩大验证范围。

## 5. 修改约定

- 遵循 `.editorconfig`：使用空格；C# 四空格、项目/XML/JSON 两空格；C# 文件使用 UTF-8 BOM 并保留文件头。
- 公共 API 应有 XML 注释。新功能或缺陷修复按 `CONTRIBUTING.md` 要求补测试。
- 提交信息采用 Conventional Commit 风格，组件作用域通常写作 `module:<组件名>`。
- 保持最小改动，不把格式化、生成文件刷新和业务修复混在同一改动中。
- 不直接假设同名组件在所有模式下走同一渲染分支。例如 Select 的 default、multiple、tags 模式有不同标记路径。
- 不把文档 Demo 当成自动化测试；Demo 用于说明和人工验证，回归行为仍应落到 `tests/`。

## 6. 分支文档

- [dev1.6.2 相对 master 的改动说明](ai-branches/dev1.6.2.md)

新增长期分支或较大功能线时，在 `docs/ai-branches/` 下添加独立说明，并在本节建立索引。分支文档应只描述已提交差异，明确排除本地未提交文件。
