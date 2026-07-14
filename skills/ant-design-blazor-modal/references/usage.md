# Ant Design Blazor Modal 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 异步关闭 | 点击确定后异步关闭对话框，例如提交表单。 > 使用单向绑定的 `Visible` 控制。 |
| 异步确认对话框 | 使用 `Confirm()` 可以快捷地弹出确认框。OnCancel/OnOk 异步事件 可以延迟关闭。 使用`ModalClosingEventArgs.Cancel`决定窗体是否关闭。返回结果: true 如果应取消事件;否则为 false。 |
| 自定义页脚 | 更复杂的例子，自定义了页脚的按钮，点击提交后进入 loading 状态，完成后关闭。 不需要默认确定取消按钮时，你可以把 `footer` 设为 `null`。 |
| 可拖拽的Modal | 使用 `Draggable` 可以创建一个可拖拽的Modal，并且可以通过 `DragInViewport` 控制是否仅仅允许在视窗范围内进行拖动。 |
| 可调整大小 | 通过设置 `Resizable=true`，可以对 Modal 在水平方向进行大小调整。 |
| 最大化 | `Maximizable` 显示最大化按钮，`DefaultMaximized` 控制首次打开时是否默认最大化。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Button Type="ButtonType.Primary" OnClick="() => _visible = true">打开对话框</Button>

<Modal Title="编辑资料" @bind-Visible="_visible" OnOk="Save">
    <Input @bind-Value="_name" Placeholder="姓名" />
</Modal>

@code {
    private bool _visible;
    private string? _name;
    private void Save() => _visible = false;
}
```

## 2. 异步关闭

关键差异：点击确定后异步关闭对话框，例如提交表单。 > 使用单向绑定的 `Visible` 控制。

```razor
<Button Type="ButtonType.Primary" OnClick="@ShowModal">
    Open Modal with async logic
</Button>
<Modal Title="@("Title")"
       Visible="@_visible"
       OnOk="@HandleOk"
       OnCancel="@HandleCancel"
       ConfirmLoading="@_confirmLoading">
    <p>@_modalText</p>
</Modal>


@code{
    bool _visible = false;
    bool _confirmLoading = false;
    string _modalText = "Content of the modal";

    private void ShowModal()
    {
        _visible = true;
    }


    private async Task HandleOk(MouseEventArgs e)
    {
        _modalText = "The modal will be closed after two seconds";
        _confirmLoading = true;
        StateHasChanged();
        await Task.Delay(2000);
        _visible = false;
        _confirmLoading = false;
    }

    private void HandleCancel(MouseEventArgs e)
    {
        Console.WriteLine("Clicked cancel button");
        _visible = false;
    }
}
```

## 3. 异步确认对话框

关键差异：使用 `Confirm()` 可以快捷地弹出确认框。OnCancel/OnOk 异步事件 可以延迟关闭。 使用`ModalClosingEventArgs.Cancel`决定窗体是否关闭。返回结果: true 如果应取消事件;否则为 false。

```razor
@inject ModalService _modalService

<Button OnClick="@ShowConfirm">Confirm</Button>


<Button OnClick="@ShowConfirmNotClose">Confirm</Button>


@code {

    private Func<ModalClosingEventArgs, Task> OnOkClick = async (e) =>
    {
        await Task.Delay(1000);
    };

    private void ShowConfirm()
    {
        RenderFragment icon = @<Icon Type="@IconType.Outline.ExclamationCircle" />;

        _modalService.Confirm(new ConfirmOptions()
        {
            Title = "Do you want to delete these items?",
            Icon = icon,
            Content = "When clicked the OK button, this dialog will be closed after 1 second",
            OnOk = OnOkClick
        });
    }



    private Func<ModalClosingEventArgs, Task> OnNotOkClick = async (e) =>
    {
        await Task.Delay(1000);
        e.Cancel = true;
    };

    private void ShowConfirmNotClose()
    {
        RenderFragment icon = @<Icon Type="@IconType.Outline.ExclamationCircle" />;

        _modalService.Confirm(new ConfirmOptions()
        {
            Title = "Do you want to delete these items?",
            Icon = icon,
            Content = "When clicked the OK button, this dialog will cancel the closing in 1 second",
            OnOk = OnNotOkClick
        });
    }
}
```

## 4. 自定义页脚

关键差异：更复杂的例子，自定义了页脚的按钮，点击提交后进入 loading 状态，完成后关闭。 不需要默认确定取消按钮时，你可以把 `footer` 设为 `null`。

```razor
<Button Type="ButtonType.Primary" OnClick="@ShowModal">
    Open Modal with customized footer
</Button>
@{
    RenderFragment footer = @<Template>
    <Button OnClick="@HandleOk" @key="@( "submit" )"
            Type="ButtonType.Primary"
            Loading="@_loading">
        Submit
    </Button>
    <Button OnClick="@HandleCancel" @key="@( "back" )">Return</Button>
</Template>;
}

<Modal Title="@("Title")"
       Visible="@_visible"
       OnOk="@HandleOk"
       OnCancel="@HandleCancel"
       Footer="@footer">
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
</Modal>


@code{
    bool _visible = false;
    bool _loading = false;

    private void ShowModal()
    {
        _visible = true;
    }


    private async Task HandleOk(MouseEventArgs e)
    {
        _loading = true;
        await Task.Delay(3000);
        _visible = false;
        _loading = false;
    }

    private void HandleCancel(MouseEventArgs e)
    {
        _visible = false;
    }
}
```

## 5. 可拖拽的Modal

关键差异：使用 `Draggable` 可以创建一个可拖拽的Modal，并且可以通过 `DragInViewport` 控制是否仅仅允许在视窗范围内进行拖动。

```razor
<Space>
    <SpaceItem>
        <Button Type="ButtonType.Primary" OnClick="@(()=>{ _visible1 = true; })">
            DragInViewport
        </Button>
    </SpaceItem>

    <SpaceItem>
        <Button Type="ButtonType.Primary" OnClick="@(()=>{ _visible2 = true; })">
            DragAcrossViewport
        </Button>
    </SpaceItem>
</Space>

<Modal Title="@("DraggableModal")"
       @bind-Visible="@_visible1"
       OnOk="(e)=>{_visible1 = false;}"
       Draggable="@(true)"
       Centered
       OnCancel="(e)=>{_visible1 = false;}">
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
</Modal>

<Modal Title="@("DraggableModal")"
       @bind-Visible="@_visible2"
       OnOk="(e)=>{_visible2 = false;}"
       Draggable="@(true)"
       DragInViewport="@(false)"
       OnCancel="(e)=>{_visible2 = false;}">
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
</Modal>

@code{
    string title = "DraggableModal";
    bool _visible1 = false;
    bool _visible2 = false;
}
```

## 6. 可调整大小

关键差异：通过设置 `Resizable=true`，可以对 Modal 在水平方向进行大小调整。

```razor
<Button Type="ButtonType.Primary" OnClick="@(()=>{ _visible = true; })">
    Open Modal
</Button>
<Modal Title="BasicModal"
       @bind-Visible="@_visible"
       Resizable="@true"
       >
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
</Modal>

@code{
    bool _visible = false;
}
```

## 7. 最大化

关键差异：`Maximizable` 显示最大化按钮，`DefaultMaximized` 控制首次打开时是否默认最大化。

```razor
<Button Type="ButtonType.Primary" OnClick="@(()=>{ _visible = true; })">
    Open Modal
</Button>
<Modal Title="BasicModal"
       @bind-Visible="@_visible"
       Maximizable="@true"
       Centered="@true"
       DefaultMaximized="@true">
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
</Modal>

@code{

    bool _visible = false;

}
```
