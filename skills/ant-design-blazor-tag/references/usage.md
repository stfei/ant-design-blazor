# Ant Design Blazor Tag 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 可选择 | 可通过 CheckableTag 实现类似 Checkbox 的效果，点击切换选中效果。 |
| 控制关闭状态 | 通过 `visible` 属性控制关闭状态。 |
| 动态添加和删除 | 用数组生成一组标签，可以动态添加和删除。 |
| 预设状态的标签 | 预设五种状态颜色，可以通过设置 color 为 `success`、 `processing`、`error`、`default`、`warning` 来代表不同的状态。 |
| 多彩标签 | 我们添加了多种预设色彩的标签样式，用作不同场景使用。如果预设值不能满足你的需求，可以设置为具体的色值。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Tag Color="TagColor.Blue">处理中</Tag>
<Tag Closable OnClosing="HandleClosing">可关闭</Tag>

@code {
    private void HandleClosing(CloseEventArgs<MouseEventArgs> args)
    {
        args.Cancel = false;
    }
}
```

## 2. 可选择

关键差异：可通过 CheckableTag 实现类似 Checkbox 的效果，点击切换选中效果。

```razor
<div>
    <span style="margin-right:8px">Categories:</span>
    <Tag Checkable Checked="lstCheck[0]">Movies</Tag>
    <Tag Checkable Checked="lstCheck[1]">Books</Tag>
    <Tag Checkable Checked="lstCheck[2]">Music</Tag>
    <Tag Checkable Checked="lstCheck[3]">Sports</Tag>
</div>

@code{
    bool[] lstCheck = new bool[] { false, true, false, false };
}
```

## 3. 控制关闭状态

关键差异：通过 `visible` 属性控制关闭状态。

```razor
<div>
    <Tag Closable Visible="bVisible">Movies</Tag>
    <br />
    <br />
    <Button Size="ButtonSize.Small" OnClick="onClick">Toggle</Button>
</div>

@code{
    bool bVisible { get; set; } = true;
    void onClick()
    {
        bVisible = !bVisible;
    }
}
```

## 4. 动态添加和删除

关键差异：用数组生成一组标签，可以动态添加和删除。

```razor
<div>
    <Tag>Unremovable</Tag>
    @foreach (var item in lstTags)
    {
        <Tag @key="item" Closable OnClose="()=>OnClose(item)" >@item</Tag>
    }
    @if (inputVisible)
    {
        <Input @ref="_inputRef" Style="width: 78px" Size="InputSize.Small" @bind-Value="_inputValue" OnBlur="HandleInputConfirm" OnPressEnter="HandleInputConfirm" AutoFocus/>
    }
    else
    {
        <Tag Class="site-tag-plus" OnClick="@(() => inputVisible = !inputVisible)">
            <Icon Type="@IconType.Outline.Plus" />New Tag
        </Tag>
    }
</div>
<style>
    .site-tag-plus {
        background: #fff;
        border-style: dashed;
    }
</style>
@code{
    private bool inputVisible { get; set; } = false;
    string _inputValue;
    Input<string> _inputRef;
    List<string> lstTags { get; set; } = new List<string>();

    protected override void OnInitialized()
    {
        lstTags.Add("Tag 2");
        lstTags.Add("Tag 3");
    }

    void ValueChange(ChangeEventArgs value)
    {
        lstTags.Add(value.Value.ToString());
    }

    void OnClose(string item)
    {
        lstTags.Remove(item);
    }

    void HandleInputConfirm()
    {
        if (string.IsNullOrEmpty(_inputValue))
        {
            CancelInput();
            return;
        }

        string res = lstTags.Find(s => s == _inputValue);

        if (string.IsNullOrEmpty(res))
        {
            lstTags.Add(_inputValue);
        }

        CancelInput();
    }

    void CancelInput()
    {
        this._inputValue = "";
        this.inputVisible = false;
    }

}
```

## 5. 预设状态的标签

关键差异：预设五种状态颜色，可以通过设置 color 为 `success`、 `processing`、`error`、`default`、`warning` 来代表不同的状态。

```razor
<Divider Orientation="DividerOrientation.Left">Without icon</Divider>
<div id="tag-status-tag-demo">
    <Tag Color="TagColor.Success">success</Tag>
    <Tag Color="TagColor.Processing">processing</Tag>
    <Tag Color="TagColor.Error">error</Tag>
    <Tag Color="TagColor.Warning">warning</Tag>
    <Tag Color="TagColor.Default">default</Tag>

</div>
<Divider Orientation="DividerOrientation.Left">With icon</Divider>
<div id="tag-status-tag-demo">
    <Tag Icon="@IconType.Outline.CheckCircle" Color="TagColor.Success">Success</Tag>
    <Tag Color="TagColor.Processing">
        <Icon Type="@IconType.Outline.Sync" Spin />
        Processing
    </Tag>
    <Tag Icon="@IconType.Outline.CloseCircle" Color="TagColor.Error">Error</Tag>
    <Tag Icon="@IconType.Outline.ExclamationCircle" Color="TagColor.Warning">Warning</Tag>
    <Tag Icon="@IconType.Outline.ClockCircle" Color="TagColor.Default">Waiting</Tag>
    <Tag Icon="@IconType.Outline.MinusCircle" Color="TagColor.Default">Stop</Tag>
</div>

<style>
	#tag-status-tag-demo > * {
		margin-bottom: 12px;
		margin-right: 8px;
	}
</style>
```

## 6. 多彩标签

关键差异：我们添加了多种预设色彩的标签样式，用作不同场景使用。如果预设值不能满足你的需求，可以设置为具体的色值。

```razor
<Divider Orientation="DividerOrientation.Left">Presets</Divider>
<div class="tag-colorful-demo">
    <Tag Color="TagColor.Magenta">magenta</Tag>
    <Tag Color="TagColor.Pink">pink</Tag>
    <Tag Color="TagColor.Red">red</Tag>
    <Tag Color="TagColor.Volcano">volcano</Tag>
    <Tag Color="TagColor.Orange">orange</Tag>
    <Tag Color="TagColor.Green">green</Tag>
    <Tag Color="TagColor.Cyan">cyan</Tag>
    <Tag Color="TagColor.Blue">blue</Tag>
    <Tag Color="TagColor.Lime">lime</Tag>
    <Tag Color="TagColor.GeekBlue">geekblue</Tag>
    <Tag Color="TagColor.Purple">purple</Tag>
    <Tag Color="TagColor.Yellow">yellow</Tag>
    <Tag Color="TagColor.Gold">gold</Tag>
</div>
<Divider Orientation="DividerOrientation.Left">Inverse</Divider>
<div class="tag-colorful-demo">
    <Tag Icon="taobao" Color="TagColor.BlueInverse">blue-inverse</Tag>
    <Tag Color="TagColor.OrangeInverse">orange-inverse</Tag>
    <Tag Icon="skype" Color="TagColor.RedInverse">red-inverse</Tag>
    <Tag Icon="weibo" Color="TagColor.PurpleInverse">purple-inverse</Tag>
</div>
<Divider Orientation="DividerOrientation.Left">Custom</Divider>
<div class="tag-colorful-demo">
    <Tag Color="@("#f50")">#f50</Tag>
    <Tag Color="@("#2db7f5")">#2db7f5</Tag>
    <Tag Color="@("#87d068")">#87d068</Tag>
    <Tag Color="@("#108ee9")">#108ee9</Tag>
    <Tag Color="@("HotPink")">HotPink</Tag>
    <Tag Color="@("DarkRed")">DarkRed</Tag>
    <Tag Color="@("rgb(143, 201, 146)")">rgb(143, 201, 146)</Tag>
    <Tag Color="@("rgb(105, 58, 236)")">rgb(105, 58, 236)</Tag>
</div>

<style>
	.tag-colorful-demo > * {
		margin-bottom: 12px;
		margin-right: 8px;
	}
</style>
```
