# Ant Design Blazor Button 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 图标按钮 | 当需要在 `Button` 内嵌入 `Icon` 时，可以设置 `icon` 属性，或者直接在 `Button` 内使用 `Icon` 组件。 如果想控制 `Icon` 具体的位置，只能直接使用 `Icon` 组件，而非 `icon` 属性。 如果想在`Button` 内嵌IconFont图标，可以在引用相关的JS后，直接设置`IconFont`属性。 |
| 加载中状态 | 添加 `Loading` 属性即可让按钮处于加载状态，最后两个按钮演示点击后进入加载状态。 使用 `AutoLoading` 属性可为绑定 `OnClick` 回调的异步方法自动处理加载状态。 |
| 不可用状态 | 添加 `disabled` 属性即可让按钮处于不可用状态，同时按钮样式也会改变。 |
| 按钮尺寸 | 按钮有大、中、小三种尺寸。 通过设置 `size` 为 `large` `small` 分别把按钮设为大、小尺寸。若不设置 `size`，则尺寸为中。 |
| 危险按钮 | 在 4.0 之后，危险成为一种按钮属性而不是按钮类型。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Button Type="ButtonType.Primary" OnClick="SaveAsync" AutoLoading>
    保存
</Button>
<Button Danger>删除</Button>

@code {
    private async Task SaveAsync()
    {
        await Task.Delay(300);
    }
}
```

## 2. 图标按钮

关键差异：当需要在 `Button` 内嵌入 `Icon` 时，可以设置 `icon` 属性，或者直接在 `Button` 内使用 `Icon` 组件。 如果想控制 `Icon` 具体的位置，只能直接使用 `Icon` 组件，而非 `icon` 属性。 如果想在`Button` 内嵌IconFont图标，可以在引用相关的JS后，直接设置`IconFont`属性。

```razor
<div>
    <Tooltip Title="@IconType.Outline.Search">
        <Button Type="ButtonType.Primary" Shape="ButtonShape.Circle" Icon="@IconType.Outline.Search" />
    </Tooltip>
    <Button Type="ButtonType.Primary" Shape="ButtonShape.Circle">A</Button>
    <Button Type="ButtonType.Primary" Icon="@IconType.Outline.Search">Search</Button>
    <Tooltip Title="@IconType.Outline.Search">
        <Button Shape="ButtonShape.Circle" Icon="@IconType.Outline.Search" />
    </Tooltip>
    <Button Icon="@IconType.Outline.Search">Search</Button>

    <br />

    <Tooltip Title="@IconType.Outline.Search">
        <Button Shape="ButtonShape.Circle" Icon="@IconType.Outline.Search" />
    </Tooltip>
    <Button Icon="@IconType.Outline.Search">Search</Button>
    <Tooltip Title="@IconType.Outline.Search">
        <Button Type="ButtonType.Dashed" Shape="ButtonShape.Circle" Icon="@IconType.Outline.Search" />
    </Tooltip>
    <Button Type="ButtonType.Dashed" Icon="@IconType.Outline.Search">Search</Button>

    <br />


	<Button IconFont="icon-tuichu">Exit</Button>
</div>

<script src="//at.alicdn.com/t/font_8d5l8fzk5b87iudi.js"></script>
```

## 3. 加载中状态

关键差异：添加 `Loading` 属性即可让按钮处于加载状态，最后两个按钮演示点击后进入加载状态。 使用 `AutoLoading` 属性可为绑定 `OnClick` 回调的异步方法自动处理加载状态。

```razor
<div>
    <Button Type="ButtonType.Primary" Loading>Loading</Button>
    <Button Type="ButtonType.Primary" Size="ButtonSize.Small" Loading>Loading</Button>
    <Button Type="ButtonType.Primary" Icon="@IconType.Outline.Poweroff" Loading />

    <br />

    <Button Type="ButtonType.Primary"
            AutoLoading
            OnClick="EnterNoIconLoading">
        Click me!
    </Button>
    <Button Type="ButtonType.Primary"
            Icon="@IconType.Outline.Poweroff"
            AutoLoading
            OnClick="EnterWithIconLoading">
        Click me!
    </Button>
    <Button Type="ButtonType.Primary"
            Icon="@IconType.Outline.Poweroff"
            Loading="_onlyIconLoading"
            OnClick="EnterOnlyIconLoading" />
</div>

@code
{
    private bool _onlyIconLoading;

    private async Task EnterNoIconLoading()
    {
        await Task.Delay(8000);
    }

    private async Task EnterWithIconLoading()
    {
        await Task.Delay(8000);
    }

    private async Task EnterOnlyIconLoading()
    {
        _onlyIconLoading = true;
        await Task.Delay(8000);
        _onlyIconLoading = false;
    }
}
```

## 4. 不可用状态

关键差异：添加 `disabled` 属性即可让按钮处于不可用状态，同时按钮样式也会改变。

```razor
<div>
    <Button Type="ButtonType.Primary">Primary</Button>
    <Button Type="ButtonType.Primary" Disabled>Primary (disabled)</Button>
    <br />
    <Button>Default</Button>
    <Button Disabled>Default (disabled)</Button>
    <br />
    <Button Type="ButtonType.Dashed">Dashed</Button>
    <Button Type="ButtonType.Dashed" Disabled>Dashed (disabled)</Button>
    <br />
    <Button Type="ButtonType.Text">Text</Button>
    <Button Type="ButtonType.Text" Disabled>Text (disabled)</Button>
    <br />
    <Button Type="ButtonType.Link">Link</Button>
    <Button Type="ButtonType.Link" Disabled>Link (disabled)</Button>
    <br />
    <Button Type="ButtonType.Link" Danger>Danger Link</Button>
    <Button Type="ButtonType.Link" Danger Disabled>Danger Link (disabled)</Button>
    <br />
    <Button Danger>Danger Default</Button>
    <Button Danger Disabled>Danger Default (disabled)</Button>
    <br />
    <Button Type="ButtonType.Text" Danger>Danger Text</Button>
    <Button Type="ButtonType.Text" Danger Disabled>Danger Text (disabled)</Button>
    <br />
    <Button Type="ButtonType.Link" Danger>Danger Link</Button>
    <Button Type="ButtonType.Link" Danger Disabled>Danger Link (disabled)</Button>
    <div class="site-Button-Ghost-wrapper">
        <Button Ghost>Ghost</Button>
        <Button Ghost Disabled>Ghost (disabled)</Button>
    </div>
</div>
```

## 5. 按钮尺寸

关键差异：按钮有大、中、小三种尺寸。 通过设置 `size` 为 `large` `small` 分别把按钮设为大、小尺寸。若不设置 `size`，则尺寸为中。

```razor
<div>
    <RadioGroup @bind-Value="_size">
        <Radio RadioButton Value="ButtonSize.Large">Large</Radio>
        <Radio RadioButton Value="ButtonSize.Default">Default</Radio>
        <Radio RadioButton Value="ButtonSize.Small">Small</Radio>
    </RadioGroup>

    <br />

    <Button Type="ButtonType.Primary" Size="_size">Primary</Button>
    <Button Size="_size">Default</Button>
    <Button Type="ButtonType.Dashed" Size="_size">Dashed</Button>

    <br />

    <Button Type="ButtonType.Link" Size="_size">Link</Button>

    <br />

    <Button Type="ButtonType.Primary" Icon="@IconType.Outline.Download" Size="_size" />
    <Button Type="ButtonType.Primary" Icon="@IconType.Outline.Download" Size="_size" Shape="ButtonShape.Round" />
    <Button Type="ButtonType.Primary" Icon="@IconType.Outline.Download" Size="_size" Shape="ButtonShape.Round">Download</Button>
    <Button Type="ButtonType.Primary" Icon="@IconType.Outline.Download" Size="_size">Download</Button>
</div>

@code
{
    ButtonSize _size = ButtonSize.Large;
}
```

## 6. 危险按钮

关键差异：在 4.0 之后，危险成为一种按钮属性而不是按钮类型。

```razor
<div>
    <Button Danger Type="ButtonType.Primary">
        Primary
    </Button>
    <Button Danger>
        Default
    </Button>
    <Button Danger Type="ButtonType.Dashed">
        Dashed
    </Button>
    <Button Danger Type="ButtonType.Text">
        Text
    </Button>
    <Button Danger Type="ButtonType.Link">
        Link
    </Button>
</div>
```
