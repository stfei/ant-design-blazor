# dev1.6.2 分支改动说明

## 1. 比较口径

- 分支：`dev1.6.2`
- 用户所称主分支：本仓库实际为 `master`，不存在 `main` 引用。
- 分析命令：`git diff master...HEAD`。
- 分析快照：2026-07-13，`HEAD` 为 `4a04e3d7`，合并基点为 `0c3881dcc7a05869d454607a849a1a8dc2a3a687`。
- 已提交差异：5 个提交，12 个文件，约 265 行新增、202 行删除。

本说明只覆盖已提交分支差异。分析时工作区另有以下用户本地内容，不属于本分支说明，也不应被后续任务覆盖：

- `site/AntDesign.Docs.Build.CLI/KnownChineseTranslations.json` 在 `HEAD` 之上还有未提交删除；
- 未跟踪的 `components/components.sln`；
- 未跟踪的 `site/AntDesign.Docs/AntDesign.Docs.sln`。

## 2. 改动总览

| 模块 | 目标 | 主要文件 |
| --- | --- | --- |
| Modal | Confirm 对话框按钮支持自定义 CSS class | `ButtonProps.cs`、`Confirm.razor` |
| Upload | 暴露文件 input id；允许点击上传与粘贴上传并存；无输入控件时监听页面粘贴 | `Upload.razor.cs`、`UploadButton.razor.cs`、`uploadHelper.ts`、`Paste.razor` |
| Select | 修复 .NET 10 下 `Loading` 动态变化后缀图标不刷新的问题 | `SelectBase.razor.cs`、`SelectContent.*`、`SelectSuffixIcon.razor.cs`、`SelectUsers.razor` |
| 文档翻译 | 删除 3 个已失配的中文翻译键 | `KnownChineseTranslations.json` |

分支未新增自动化测试；当前行为主要通过文档 Demo 展示，后续修改应优先补对应回归测试。

## 3. Modal：Confirm 按钮 class

### 公共面变化

`ButtonProps` 新增公共属性：

```csharp
public string Class { get; set; }
```

`Confirm.razor` 的本地 `BuildButton` 将 `ButtonProps.Class` 传给内部 `<Button Class="@props.Class">`。因此 `ConfirmConfig.Button1Props`、`Button2Props` 等使用 `ButtonProps` 的入口可以为确认按钮添加自定义 class。

### 修改边界

- 这是 Confirm 对话框内部按钮配置能力，不是对基础 `Button` 组件新增参数。
- 后续调整 `ButtonProps` 时，要搜索所有构造和消费位置，避免只更新 Confirm 的某一种按钮布局。
- 公共属性应补充清晰的 XML 注释，并建议增加渲染测试，断言 class 最终出现在按钮 DOM 上。

## 4. Upload：点击与粘贴并存

### 4.1 新增公共面

`Upload` 新增：

```csharp
[Parameter]
public bool Pastable { get; set; } = false;

public string FileId => _uploadButton.Fileid;
```

- `Pastable=true` 用于保留默认 `UploadTrigger.Click` 的同时再注册粘贴事件；原有 `Trigger="UploadTrigger.Paste"` 仍是纯粘贴入口。
- `FileId` 暴露内部 `<input type="file">` 的 id，供外部代码定位或控制真实文件输入元素。

### 4.2 当前调用链

```text
Upload 参数
  -> UploadButton.OnAfterRenderAsync / Disabled 切换
  -> JSInteropConstants.AddPasteEventListener
  -> uploadHelper.addPasteEventListener
  -> 剪贴板 File 转入 DataTransfer，并赋给 input.files
  -> UploadButton.OnPasteResult（JSInvokable）
  -> HandleFileList -> 校验、生成文件 id、进入等待或上传流程
```

`uploadHelper.addPasteEventListener` 依次寻找上传按钮内容中的非 file `input`、`textarea`；两者都不存在时，退化为 `document` 级 `paste` 监听。文档 Demo 新增了“页面粘贴或点击上传”的用例。

### 4.3 需重点复核的现状

以下是当前实现的维护边界，不应在不验证的情况下继续复制：

1. 注册时使用匿名箭头函数包装 `handlePaste`，注销时却传入 `handlePaste` 本身。浏览器要求相同函数引用，当前注销很可能无法移除既有监听。
2. `document` 级监听会让组件影响整页；多个 `Pastable` Upload 实例可能同时接收一次粘贴。
3. `handlePaste` 在判断剪贴板是否包含文件前就调用 `preventDefault()`。页面级监听存在拦截普通文本粘贴的风险。
4. Disabled 往返切换可能重复注册监听；组件当前也没有围绕这些新增监听展示明确的销毁链路。
5. `FileId` 依赖 `_uploadButton` 已完成实例化。首次渲染前、没有按钮内容或特定 PictureCard 渲染条件下读取时需要防范空引用和时机问题。
6. `uploadHelper.ts` 的大量变更是整体缩进调整，真实逻辑变化集中在监听目标和 document 回退。后续评审应忽略纯格式噪声，聚焦事件生命周期。

建议至少覆盖：Click+Pastable 并存、纯 Paste、TextArea、document 回退、Disabled 切换、组件卸载、多实例、非文件剪贴板和 Drag 模式。

## 5. Select：Loading 动态刷新

### 5.1 问题与实现

分支将 `SelectBase.Loading` 从自动属性改为带后备字段的属性。值被设置时调用 `_selectContent?.RefreshComponentState()`；`SelectContent` 保存 `SelectSuffixIcon` 的组件引用，并在 `NET10_0_OR_GREATER` 下调用其 `Refresh()`，最终由 `StateHasChanged()` 重新渲染 loading 后缀。

链路如下：

```text
父组件更新 Select.Loading
  -> SelectBase.Loading setter
  -> SelectContent.RefreshComponentState
  -> [仅 NET10_0_OR_GREATER] SelectSuffixIcon.Refresh
  -> StateHasChanged
  -> loading / arrow / search / clear 图标重新判定
```

`SelectUsers.razor` Demo 增加按钮，用于在运行时切换 `_loading`，为人工验证提供入口。

### 5.2 模式和框架边界

- 强制刷新被 `#if NET10_0_OR_GREATER` 包围，验证必须至少包含 `net10.0`，不能只用较低目标框架的结果代替。
- default 模式的 `SelectSuffixIcon` 传入了 `Loading="@Loading"`；multiple/tags 渲染分支当前没有传入 `Loading`。因此后续若宣称所有模式均已修复，必须先补齐并验证非 default 模式。
- 首次参数赋值时 `_selectContent` 可能尚未建立，空条件访问避免了异常；后续改造不要移除这个时序保护。
- 当前 setter 不判断新旧值，每次参数赋值都可能触发子组件刷新。若优化，应先确认 Blazor .NET 10 的原始刷新问题及生命周期原因。

建议增加针对运行时 `Loading: true -> false -> true` 的渲染测试，并分别覆盖 default、multiple/tags 和目标框架差异。

## 6. 文档翻译元数据

分支从 `KnownChineseTranslations.json` 删除了三个键：

- FormValidationRule 子元素规则说明；
- Mentions 动态加载说明；
- Table 行标识说明。

这些删除与 Modal、Upload、Select 功能链没有直接耦合。继续处理时先运行或检查文档 CLI 的生成结果，确认删除源于上游英文键消失，而不是误删人工翻译。工作区在 `HEAD` 之上还有更多未提交删除，必须与分支内这三项分开审阅。

## 7. 后续 AI 的验证顺序

1. 运行 `git status --short`，保护用户本地翻译文件和两个未跟踪 solution 文件。
2. 用 `git diff master...HEAD -- <目标文件>` 读取分支真实差异，不把纯格式化当成新逻辑。
3. Modal 改动至少构建核心组件，并补/跑确认按钮渲染测试。
4. Select 改动使用 `net10.0` 验证动态 Loading；若涉及 multiple/tags，单独覆盖其渲染分支。
5. Upload 改动同时运行 .NET 测试、TypeScript lint/测试，并在浏览器验证事件注册与注销。
6. Demo 或 XML 文档有变化时，构建 `site/AntDesign.Docs.Build`，检查生成元数据而不是手工猜测结果。
