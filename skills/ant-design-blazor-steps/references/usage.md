# Ant Design Blazor Steps 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 可点击 | 设置 `onChange` 后，Steps 变为可点击状态。 |
| 步骤运行错误 | 使用 Steps 的 `status` 属性来指定当前步骤的状态。 |
| 带有进度的步骤 | 带有进度的步骤。 |
| 竖直方向的步骤条 | 简单的竖直方向的步骤条。 |
| 步骤切换 | 通常配合内容及按钮使用，表示一个流程的处理进度。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Steps Current="_current">
    <Step Title="填写信息" />
    <Step Title="确认提交" />
    <Step Title="完成" />
</Steps>

<Button OnClick="() => _current++" Disabled="@(_current >= 2)">下一步</Button>

@code {
    private int _current;
}
```

## 2. 可点击

关键差异：设置 `onChange` 后，Steps 变为可点击状态。

```razor
<div>
    <Steps Current="current" OnChange="onChange">
        <Step Title="Step 1" Description="This is a description." />
        <Step Title="Step 2" Description="This is a description." />
        <Step Title="Step 3" Description="This is a description." />
    </Steps>

    <Divider />

    <Steps Current="current" OnChange="onChange" Direction="StepsDirection.Vertical">
        <Step Title="Step 1" Description="This is a description." />
        <Step Title="Step 2" Description="This is a description." />
        <Step Title="Step 3" Description="This is a description." />
    </Steps>
</div>

@code{
    int current;

    void onChange(int current)
    {
        this.current = current;
    }
}
```

## 3. 步骤运行错误

关键差异：使用 Steps 的 `status` 属性来指定当前步骤的状态。

```razor
<div>
    <Steps Current="1" Status="StepsStatus.Error">
        <Step Title="Finished" Description="This is a description." />
        <Step Title="In Progress" Description="This is a description." />
        <Step Title="Waiting" Description="This is a description." />
    </Steps>
</div>
```

## 4. 带有进度的步骤

关键差异：带有进度的步骤。

```razor
<div>
    <Steps Percent="60" Current="1">
        <Step Title="Finished" Description="This is a description." />
        <Step Title="In Progress" Description="This is a description." />
        <Step Title="Waiting" Description="This is a description." />
    </Steps>
</div>
```

## 5. 竖直方向的步骤条

关键差异：简单的竖直方向的步骤条。

```razor
<div>
    <Steps Direction="StepsDirection.Vertical" Current="1">
        <Step Title="Finished" Description="This is a description." />
        <Step Title="In Progress" Description="This is a description." />
        <Step Title="Waiting" Description="This is a description." />
    </Steps>
</div>
```

## 6. 步骤切换

关键差异：通常配合内容及按钮使用，表示一个流程的处理进度。

```razor
<div>
    <Steps Current="current">
        @foreach (var item in steps)
        {
            <Step Title="@item.Title" Subtitle="@item.Content" />
        }
    </Steps>

    <div class="steps-content">
        @steps[current].Content
    </div>
    <div class="steps-action">
        @if (current > 0)
        {
            <Button Type="ButtonType.Primary" OnClick="OnPreClick">Previous</Button>
        }
        @if (current < steps.Length - 1)
        {
            <Button Type="ButtonType.Primary" OnClick="OnNextClick">Next</Button>
        }
        @if (current == steps.Length - 1)
        {
            <Button Type="ButtonType.Primary" OnClick=@(() => message.Success("Processing complete!"))>
                Done
            </Button>
        }
    </div>
</div>

<style>
    .steps-content {
        margin-top: 16px;
        border: 1px dashed #e9e9e9;
        border-radius: 6px;
        background-color: #fafafa;
        min-height: 200px;
        text-align: center;
        padding-top: 80px;
    }

    .steps-action {
        margin-top: 24px;
    }
</style>

@inject IMessageService message
@code {

    public class StepItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public int current { get; set; } = 0;

    public StepItem[] steps =
    {
        new StepItem {Title = "First", Content = "First-content"},
        new StepItem {Title = "Second", Content = "Second-content"},
        new StepItem {Title = "Third", Content = "Third-content"},
        new StepItem {Title = "Last", Content = "Last-content"}
    };

    void OnPreClick()
    {
        current--;
    }

    void OnNextClick()
    {
        current++;
    }
}
```
