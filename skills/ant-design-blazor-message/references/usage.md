# Ant Design Blazor Message 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 修改延时 | 自定义时长 `10s`，默认时长为 `3s`。 |
| 加载中 | 进行全局 loading，异步自行移除。 |
| 更新消息内容 | 可以通过唯一的 `key` 来更新内容。 |
| 其他提示类型 | 包括成功、失败、警告。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
@inject IMessageService Message

<Button OnClick="ShowMessage">保存</Button>

@code {
    private void ShowMessage()
    {
        Message.Success("保存成功");
    }
}
```

## 2. 修改延时

关键差异：自定义时长 `10s`，默认时长为 `3s`。

```razor
@inject IMessageService _message

<Button Type="ButtonType.Default" OnClick="OnClick">
    Customized display duration
</Button>

@code{
    private void OnClick()
    {
        _message.Success("This is a prompt message for success, and it will disappear in 10 seconds", 10);
    }
}
```

## 3. 加载中

关键差异：进行全局 loading，异步自行移除。

```razor
@inject IMessageService _message

    <Button Type="ButtonType.Default" OnClick="OnClick">
        Display a loading indicator
    </Button>

@code{

    private async Task OnClick()
    {
        var cofig = new MessageConfig()
        {
            Content = "Action in progress..",
            Duration = 0
        };
        var task = _message.LoadingAsync(cofig);
        await Task.Delay(2000);
        task.Start();
    }

}
```

## 4. 更新消息内容

关键差异：可以通过唯一的 `key` 来更新内容。

```razor
@inject IMessageService _message

<Button Type="ButtonType.Primary" OnClick="OnClick">
    Display normal message
</Button>

@code{
    private async Task OnClick()
    {
        string key = $"updatable-{DateTime.Now.Ticks}";
        var config = new MessageConfig()
        {
            Content = "Loading...",
            Key = key
        };
        _message.Loading(config);

        await Task.Delay(2000);

        config.Content = "Loaded";
        config.Duration = 2;
        _message.Success(config);
    }
}
```

## 5. 其他提示类型

关键差异：包括成功、失败、警告。

```razor
@inject IMessageService _message

<Space>
    <SpaceItem>
        <Button Type="ButtonType.Default" OnClick="Success">
            Success
        </Button>
    </SpaceItem>
    <SpaceItem>
        <Button Type="ButtonType.Default" OnClick="Error">
            Error
        </Button>
    </SpaceItem>
    <SpaceItem>
        <Button Type="ButtonType.Default" OnClick="Warning">
            Warning
        </Button>
    </SpaceItem>
</Space>

@code{

    private async Task Success()
    {
        _message.Success("This is a success message");
    }

    private async Task Error()
    {
        _message.Error("This is an error message");

    }

    private async Task Warning()
    {
        _message.Warning("This is a warning message");
    }

}
```
