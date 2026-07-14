# Ant Design Blazor Notification 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 自动关闭的延时 | 自定义通知框自动关闭的延时，默认`4.5s`，取消自动关闭只要将该值设为 `0` 即可。 |
| 位置 | 通知从右上角、右下角、左下角、左上角弹出。 |
| 自定义按钮 | 自定义关闭按钮的样式和文字。 |
| 更新消息内容 | 可以通过唯一的 key 来更新内容。 |
| 自定义图标 | 图标可以被自定义。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
@inject NotificationService Notification

<Button OnClick="OpenNotification">显示通知</Button>

@code {
    private async Task OpenNotification()
    {
        await Notification.Open(new NotificationConfig
        {
            Message = "任务完成",
            Description = "后台处理已经结束。"
        });
    }
}
```

## 2. 自动关闭的延时

关键差异：自定义通知框自动关闭的延时，默认`4.5s`，取消自动关闭只要将该值设为 `0` 即可。

```razor
@inject INotificationService _notice

<Button Type="ButtonType.Primary" OnClick="OnClick">
    Open the notification box
</Button>

@code {
    private async Task OnClick()
    {
        await _notice.Open(new NotificationConfig()
            {
                Message = "Notification Title",
                Duration = 0,
                Description = "I will never close automatically. This is a purposely very very long description that has many many characters and words."
            });
    }
}
```

## 3. 位置

关键差异：通知从右上角、右下角、左下角、左上角弹出。

```razor
@inject INotificationService _notice

<Space>
    <SpaceItem><Button OnClick="() => Open(NotificationPlacement.TopLeft)">左上</Button></SpaceItem>
    <SpaceItem><Button OnClick="() => Open(NotificationPlacement.BottomRight)">右下</Button></SpaceItem>
</Space>

@code {
    private Task Open(NotificationPlacement placement)
    {
        return _notice.Open(new NotificationConfig
        {
            Message = $"通知位置：{placement}",
            Description = "通过 Placement 控制弹出位置。",
            Placement = placement
        });
    }
}
```

## 4. 自定义按钮

关键差异：自定义关闭按钮的样式和文字。

```razor
@inject INotificationService _notice

<Button Type="ButtonType.Primary" OnClick="OnClick">
    custom button
</Button>

@code{

    private async Task OnClick()
    {
        string key = $"open{DateTime.Now}";
        RenderFragment btn = @<Button Type="ButtonType.Primary" OnClick="() => { _notice.Close(key); }">
                                confirm
                            </Button>;
        await _notice.Open(new NotificationConfig()
        {
            Message = "Notification Title",
            Key = key,
            Description = "A function will be be called after the notification is closed (automatically after the \"duration\" time of manually).",
            Btn = btn
        });
    }
}
```

## 5. 更新消息内容

关键差异：可以通过唯一的 key 来更新内容。

```razor
@inject INotificationService _notice

    <Button Type="ButtonType.Primary" OnClick="OnClick">
        Open the notification box
    </Button>

@code{
    private async Task OnClick()
    {
        string key = $"open{DateTime.Now}";
        var task = _notice.Open(new NotificationConfig()
        {
            Message = "Notification Title",
            Key = key,
            Description = "description.",
        });
        await Task.Delay(1000);
        await _notice.Open(new NotificationConfig()
        {
            Message = "New Title",
            Key = key,
            Description = "New description.",
        });
    }
}
```

## 6. 自定义图标

关键差异：图标可以被自定义。

```razor
@inject INotificationService _notice

<Button Type="ButtonType.Primary" OnClick="OnClick">
    Open the notification box
</Button>

@code{

    private async Task OnClick()
    {
        RenderFragment customIcon = @<Icon Type="@IconType.Outline.Smile" Style="color:#108ee9;" />;
        await _notice.Open(new NotificationConfig()
        {
            Message = "Notification Title",
            Description = "This is the content of the notification. This is the content of the notification. This is the content of the notification.",
            Icon = customIcon
        });
    }
}
```
