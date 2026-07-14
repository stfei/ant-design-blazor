---
name: ant-design-blazor-progress
description: 使用自包含的多种 Razor 用法实现、修改和审查 Ant Design Blazor Progress 组件。覆盖基础配置、状态变化、数据绑定、事件、模板与该组件的代表性高级模式；用于需要比较不同写法并选择正确模式的任务，无需访问源码仓库或文档站。
---

# Ant Design Blazor Progress

## 使用流程

1. 必须先读取 [多种用法与差异代码](references/usage.md)。
2. 根据“关键差异”选择最接近需求的模式，不要把多个模式的互斥参数机械合并。
3. 以选中模式的代码为骨架，替换业务数据、文案和回调；补齐片段引用的模型与服务。
4. 如果需求同时涉及多个能力，只组合彼此兼容的参数、模板和事件。

## 选择重点

- 区分非受控默认值、受控值与 `@bind-*` 双向绑定。
- 区分静态子标签、`DataSource` 数据驱动和自定义模板。
- 区分只改变视觉的参数与会改变事件、生命周期或数据结构的模式。
- 复杂组件优先复用完整模式，不从 React Ant Design API 猜测 Blazor 参数。

## 实施规则

- 默认应用已引用 `AntDesign` 包、完成所需服务注册，并在 `_Imports.razor` 中包含 `@using AntDesign`。
- 异步事件返回 `Task` 并等待完成，不使用 `async void`。
- 泛型组件保持 `TItem`、`TItemValue`、`TValue` 与实际模型一致。
- 关键代码全部保存在当前技能目录内，可脱离本仓库直接使用。
- 版本存在差异时，以使用方已安装包的公共 API 和编译结果为准。
