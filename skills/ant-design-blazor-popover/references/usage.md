# Ant Design Blazor Popover 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 从浮层内关闭 | 使用 `visible` 属性控制浮层显示。 |
| 三种触发方式 | 鼠标移入、聚集、点击。 |
| 悬停点击弹出窗口 | 以下示例显示如何创建可悬停和单击的弹出窗口。 |
| 位置 | 位置有十二个方向。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Popover Title="详情" Content="这里是补充说明。" Trigger="@(new[] { Trigger.Click })">
    <Button>查看详情</Button>
</Popover>
```

## 2. 从浮层内关闭

关键差异：使用 `visible` 属性控制浮层显示。

```razor
<Popover OnVisibleChange="OnVisibleChange" Visible="_visible" ContentTemplate="@_content" Title="Title" Trigger="@(new AntDesign.Trigger[] { AntDesign.Trigger.Click})">
  <Button Type="ButtonType.Primary">Click me</Button>
</Popover>


@code{

    private RenderFragment _content =>
        @<a @onclick="_=>this._visible = false">Close</a>;

    private bool _visible = false;

    private void OnVisibleChange(bool visible)
    {
        _visible = visible;
    }
}
```

## 3. 三种触发方式

关键差异：鼠标移入、聚集、点击。

```razor
<div>
    <Popover ContentTemplate="@_content" Title="Title" Trigger="@(new AntDesign.Trigger[] { AntDesign.Trigger.Hover})">
        <AntDesign.Button>Hover me</AntDesign.Button>
    </Popover>
    <Popover ContentTemplate="@_content" Title="Title" Trigger="@(new AntDesign.Trigger[] { AntDesign.Trigger.Focus})">
        <AntDesign.Button>Focus me</AntDesign.Button>
    </Popover>
    <Popover ContentTemplate="@_content" Title="Title" Trigger="@(new AntDesign.Trigger[] { AntDesign.Trigger.Click})">
        <AntDesign.Button>Click me</AntDesign.Button>
    </Popover>
</div>

@code{
    private RenderFragment _content =
    @<div>
        <p>Content</p>
        <p>Content</p>
    </div>;
}
```

## 4. 悬停点击弹出窗口

关键差异：以下示例显示如何创建可悬停和单击的弹出窗口。

```razor
<Popover Style="{width: 500}" OnVisibleChange="OnHoverVisibleChange" Visible="_hoverVisible" Title="Hover title" Trigger="@(new AntDesign.Trigger[] { AntDesign.Trigger.Hover})">
    <ContentTemplate>
        <div> This is hover content.</div>
    </ContentTemplate>
    <ChildContent>
        <Popover OnVisibleChange="OnClickVisibleChange" Visible="_clickVisible" Content="_clickContent" Title="Click title" Trigger="@(new AntDesign.Trigger[] { AntDesign.Trigger.Click})">
            <ContentTemplate>
                <div>
                    <div>This is click content.</div>
                    <a @onclick="_=>Close()">Close</a>
                </div>
            </ContentTemplate>
            <ChildContent>
                <Button>Hover and click / 悬停并单击</Button>
            </ChildContent>

        </Popover>
    </ChildContent>
</Popover>

@code{

    private bool _hoverVisible = false;
    private bool _clickVisible = false;

    private void OnHoverVisibleChange(bool visible)
    {
        _hoverVisible = visible;
    }

    private void OnClickVisibleChange(bool visible)
    {
        _clickVisible = visible;
        _hoverVisible = false;
    }

    private void Close()
    {
        _hoverVisible = false;
        _clickVisible = false;
    }
}
```

## 5. 位置

关键差异：位置有十二个方向。

```razor
<div class="demo">
    <div style="margin-left: @($"{ButtonWidth}px"); white-space: nowrap;">
        <Popover Placement="Placement.TopLeft" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>TL</Button>
        </Popover>
        <Popover Placement="Placement.Top" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>Top</Button>
        </Popover>
        <Popover Placement="Placement.TopRight" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>TR</Button>
        </Popover>
    </div>
    <div style="width: @($"{ButtonWidth}px"); float: left;">
        <Popover Placement="Placement.LeftTop" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>LT</Button>
        </Popover>
        <Popover Placement="Placement.Left" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>Left</Button>
        </Popover>
        <Popover Placement="Placement.LeftBottom" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>LB</Button>
        </Popover>
    </div>
    <div style="width: @($"{ButtonWidth}px"); margin-left: @($"{ButtonWidth * 4 + 24}px");">
        <Popover Placement="Placement.RightTop" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>RT</Button>
        </Popover>
        <Popover Placement="Placement.Right" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>Right</Button>
        </Popover>
        <Popover Placement="Placement.RightBottom" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>RB</Button>
        </Popover>
    </div>
    <div style="margin-left: @($"{ButtonWidth}px"); clear: both; white-space: nowrap;">
        <Popover Placement="Placement.BottomLeft" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>BL</Button>
        </Popover>
        <Popover Placement="Placement.Bottom" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>Bottom</Button>
        </Popover>
        <Popover Placement="Placement.BottomRight" TitleTemplate="@_text" ContentTemplate="@_content" Trigger="@(new[] {AntDesign.Trigger.Click})">
            <Button>BR</Button>
        </Popover>
    </div>
</div>

@code
{
    private RenderFragment _text =@<span>Title</span>;

    const int ButtonWidth = 70;

    private RenderFragment _content =
        @<div>
            <p>Content</p>
            <p>Content</p>
        </div>;
}
```
