# Ant Design Blazor Switch 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 基本 | `Switch` 的状态完全由用户接管，不再自动根据点击事件改变数据。 |
| 加载中 | 标识开关操作仍在执行中。 |
| 文字和图标。 | 带有文字和图标。 |
| 不可用 | Switch 失效状态 |
| 两种大小 | size="small" 表示小号开关。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Switch @bind-Checked="_enabled"
        CheckedChildren="启用"
        UnCheckedChildren="停用" />

@code {
    private bool _enabled = true;
}
```

## 2. 基本

关键差异：`Switch` 的状态完全由用户接管，不再自动根据点击事件改变数据。

```razor
<div>
    <Switch Checked="@_switchValue" Loading="@_isLoading" Control OnClick="Click"/>
</div>

@code
{
    bool _switchValue;
    bool _isLoading = false;

    async Task Click()
    {
        if (!_isLoading)
        {
            _isLoading = true;
            await Task.Delay(3000);
            _switchValue = !_switchValue;
            _isLoading = false;
        }
    }
}
```

## 3. 加载中

关键差异：标识开关操作仍在执行中。

```razor
<div>
    <Switch Checked Loading />
    <br />
    <Switch Size="InputSize.Small" Loading />
</div>
```

## 4. 文字和图标。

关键差异：带有文字和图标。

```razor
<div>
    <Switch Checked="true" CheckedChildren="开" UnCheckedChildren="关" />
    <br />
    <Switch Checked="false" CheckedChildren="开" UnCheckedChildren="关" />
    <br />
    <Switch Checked="true">
        <CheckedChildrenTemplate>
            <Icon Type="@IconType.Outline.Check" />
        </CheckedChildrenTemplate>
        <UnCheckedChildrenTemplate>
            <Icon Type="@IconType.Outline.Close" />
        </UnCheckedChildrenTemplate>
    </Switch>
</div>
```

## 5. 不可用

关键差异：Switch 失效状态

```razor
<div>
    <Switch @bind-Checked="switchValue" Disabled="isDisabled" />
    <br />
    <br />
    <Button Type="ButtonType.Primary" @onclick="(_) => isDisabled = !isDisabled">Toggle Disabled</Button>
</div>

@code{
    bool switchValue = false;

    bool isDisabled = true;
}
```

## 6. 两种大小

关键差异：size="small" 表示小号开关。

```razor
<div>
    <Switch Checked="true"/>
    <br />
    <Switch Size="InputSize.Small" Checked="true"/>
</div>
```
