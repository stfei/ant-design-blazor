# Ant Design Blazor Tooltip 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 位置 | 位置有 12 个方向。 |
| 箭头指向 | 设置了 `arrowPointAtCenter` 后，箭头将指向目标元素的中心。 |
| 复杂标题 | 在提示中显示图标等复杂的组件，可以使用 `TitleTemplate`。 |
| 自动调整位置 | 气泡框不可见时自动调整位置 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Tooltip Title="保存当前修改" Placement="Placement.Top">
    <Button Type="ButtonType.Primary">保存</Button>
</Tooltip>
```

## 2. 位置

关键差异：位置有 12 个方向。

```razor
<div class="demo">
    <div style="margin-left: @($"{ButtonWidth}px"); white-space: nowrap;">
        <Tooltip Placement="Placement.TopLeft" Title="@Text">
            <Button>TL</Button>
        </Tooltip>
        <Tooltip Placement="Placement.Top" Title="@Text">
            <Button>Top</Button>
        </Tooltip>
        <Tooltip Placement="Placement.TopRight" Title="@Text">
            <Button>TR</Button>
        </Tooltip>
    </div>
    <div style="width: @($"{ButtonWidth}px"); float: left;">
        <Tooltip Placement="Placement.LeftTop" Title="@Text">
            <Button>LT</Button>
        </Tooltip>
        <Tooltip Placement="Placement.Left" Title="@Text">
            <Button>Left</Button>
        </Tooltip>
        <Tooltip Placement="Placement.LeftBottom" Title="@Text">
            <Button>LB</Button>
        </Tooltip>
    </div>
    <div style="width: @($"{ButtonWidth}px"); margin-left: @($"{ButtonWidth * 4 + 24}px");">
        <Tooltip Placement="Placement.RightTop" Title="@Text">
            <Button>RT</Button>
        </Tooltip>
        <Tooltip Placement="Placement.Right" Title="@Text">
            <Button>Right</Button>
        </Tooltip>
        <Tooltip Placement="Placement.RightBottom" Title="@Text">
            <Button>RB</Button>
        </Tooltip>
    </div>
    <div style="margin-left: @($"{ButtonWidth}px"); clear: both; white-space: nowrap;">
        <Tooltip Placement="Placement.BottomLeft" Title="@Text">
            <Button>BL</Button>
        </Tooltip>
        <Tooltip Placement="Placement.Bottom" Title="@Text">
            <Button>Bottom</Button>
        </Tooltip>
        <Tooltip Placement="Placement.BottomRight" Title="@Text">
            <Button>BR</Button>
        </Tooltip>
    </div>
</div>

@code
{
    public string Text = "prompt text";
    const int ButtonWidth = 70;
}
```

## 3. 箭头指向

关键差异：设置了 `arrowPointAtCenter` 后，箭头将指向目标元素的中心。

```razor
<div>
    <Tooltip Placement="Placement.TopLeft" Title="Prompt Text">
        <Button>Align edge / 边缘对齐</Button>
    </Tooltip>
    <Tooltip Placement="Placement.TopLeft" Title="Prompt Text" ArrowPointAtCenter="true">
        <Button>Arrow points to center / 箭头指向中心</Button>
    </Tooltip>
</div>
```

## 4. 复杂标题

关键差异：在提示中显示图标等复杂的组件，可以使用 `TitleTemplate`。

```razor
<Tooltip>
    <TitleTemplate>
        <Icon Type="@IconType.Outline.Smile"/> Good day!
    </TitleTemplate>
    <ChildContent>
        <span>Tooltip with icon.</span>
    </ChildContent>
</Tooltip>
```

## 5. 自动调整位置

关键差异：气泡框不可见时自动调整位置

```razor
<Tooltip Title="prompt text">
    <span>Tooltip will show on mouse enter.</span>
</Tooltip>
```
