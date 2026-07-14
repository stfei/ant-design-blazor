# Ant Design Blazor Popconfirm 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 条件触发 | 可以判断是否需要弹出。 |
| 自定义 Icon 图标 | 自定义提示 `icon`。 |
| 位置 | 位置有十二个方向。如需箭头指向目标元素中心，可以设置 `arrowPointAtCenter`。 |
| 设置按钮参数 | 设置 `OkButtonProps` 与 `CancelButtonProps` 属性可修改按钮的基本属性。设为 null 可隐藏按钮。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Popconfirm Title="确定删除这条记录吗？"
            OnConfirm="Delete"
            OnCancel="Cancel">
    <Button Danger>删除</Button>
</Popconfirm>

@code {
    private void Delete() { }
    private void Cancel() { }
}
```

## 2. 条件触发

关键差异：可以判断是否需要弹出。

```razor
@inject IMessageService _message

<div>
    <Popconfirm Title="Are you sure delete this task?"
                Visible="_visible"
                OnVisibleChange="OnVisibleChange"
                OnConfirm="Confirm"
                OnCancel="Cancel"
                OkText="Yes"
                CancelText="No">
        <a>Delete a task</a>
    </Popconfirm>
    <br />
    <br />
    Whether directly execute:
    <Switch Checked="_condition" OnChange="OnConditionChange" />
</div>

@code{
    private void Confirm()
    {
        _visible = false;
        _message.Success("Next step.");
    }

    private void Cancel()
    {
        _visible = false;
        _message.Error("Click on cancel.");
    }

    private bool _visible = false;
    private void OnVisibleChange(bool visible)
    {
        if (!visible)
        {
            _visible = visible;
            return;
        }
        if (_condition)
        {
            Confirm();
        }
        else
        {
            _visible = visible;
        }
    }

    private bool _condition = true;
    private void OnConditionChange(bool condition)
    {
        _condition = condition;
    }
}
```

## 3. 自定义 Icon 图标

关键差异：自定义提示 `icon`。

```razor
<Popconfirm Title="Are you sure?" Icon="close-circle">
    <a>Delete</a>
</Popconfirm>
<br />
<Popconfirm Title="Are you sure?">
    <IconTemplate>
        <Icon Type="@IconType.Outline.QuestionCircle" Style="color: red" />
    </IconTemplate>
    <ChildContent>
        <a>Delete</a>
    </ChildContent>
</Popconfirm>
```

## 4. 位置

关键差异：位置有十二个方向。如需箭头指向目标元素中心，可以设置 `arrowPointAtCenter`。

```razor
<div class="demo">
    <div style="margin-left: @($"{ButtonWidth}px"); white-space: nowrap;">
        <Popconfirm Placement="Placement.TopLeft" Title="@_title" OnConfirm="Confirm">
            <Button>TL</Button>
        </Popconfirm>
        <Popconfirm Placement="Placement.Top" Title="@_title" OnConfirm="Confirm">
            <Button>Top</Button>
        </Popconfirm>
        <Popconfirm Placement="Placement.TopRight" Title="@_title" OnConfirm="Confirm">
            <Button>TR</Button>
        </Popconfirm>
    </div>
    <div style="width: @($"{ButtonWidth}px"); float: left;">
        <Popconfirm Placement="Placement.LeftTop" Title="@_title" OnConfirm="Confirm">
            <Button>LT</Button>
        </Popconfirm>
        <Popconfirm Placement="Placement.Left" Title="@_title" OnConfirm="Confirm">
            <Button>Left</Button>
        </Popconfirm>
        <Popconfirm Placement="Placement.LeftBottom" Title="@_title" OnConfirm="Confirm">
            <Button>LB</Button>
        </Popconfirm>
    </div>
    <div style="width: @($"{ButtonWidth}px"); margin-left: @($"{ButtonWidth * 4 + 24}px");">
        <Popconfirm Placement="Placement.RightTop" Title="@_title" OnConfirm="Confirm">
            <Button>RT</Button>
        </Popconfirm>
        <Popconfirm Placement="Placement.Right" Title="@_title" OnConfirm="Confirm">
            <Button>Right</Button>
        </Popconfirm>
        <Popconfirm Placement="Placement.RightBottom" Title="@_title" OnConfirm="Confirm">
            <Button>RB</Button>
        </Popconfirm>
    </div>
    <div style="margin-left: @($"{ButtonWidth}px"); clear: both; white-space: nowrap;">
        <Popconfirm Placement="Placement.BottomLeft" Title="@_title" OnConfirm="Confirm">
            <Button>BL</Button>
        </Popconfirm>
        <Popconfirm Placement="Placement.Bottom" Title="@_title" OnConfirm="Confirm">
            <Button>Bottom</Button>
        </Popconfirm>
        <Popconfirm Placement="Placement.BottomRight" Title="@_title" OnConfirm="Confirm">
            <Button>BR</Button>
        </Popconfirm>
    </div>
</div>

@inject IMessageService _message

@code
{
    const int ButtonWidth = 70;

    private string _title = "Are you sure to delete this task?";
    private void Confirm()
    {
        _message.Info("Clicked on Yes");
    }
}
```

## 5. 设置按钮参数

关键差异：设置 `OkButtonProps` 与 `CancelButtonProps` 属性可修改按钮的基本属性。设为 null 可隐藏按钮。

```razor
<Popconfirm Title="Are you sure delete this task?"
            OnConfirm="Confirm"
            OkButtonProps="new(){ Loading = _loading }"
            CancelButtonProps="new(){ Disabled = true }">
    <a>Delete a task</a>
</Popconfirm>

<br />

<Popconfirm Title="Are you sure delete this task?"
            OnConfirm="Confirm"
            OkButtonProps="new(){ Loading = _loading }"
            CancelButtonProps="null">
    <a>Hide cancel button</a>
</Popconfirm>


@inject IMessageService _message;
@code{
    private bool _loading = false;
    private bool _visible;
    private async Task Confirm()
    {
        _loading = true;
        StateHasChanged();

        await Task.Delay(1000);

        _visible = false;
        _message.Success("Next step.");

        _loading = false;
    }
}
```
