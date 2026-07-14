# Ant Design Blazor Grid 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 区块间隔 | 使用 `Row.Gutter` 设置水平或垂直间距，并可按断点提供响应式间距。 |
| 左右偏移 | 列偏移。 使用 `offset` 可以将列向右侧偏。例如，`offset={4}` 将元素向右侧偏移了 4 个列（column）的宽度。 |
| 响应式布局 | 参照 Bootstrap 的 [响应式设计](http://getbootstrap.com/css/#grid-media-queries)，预设六个响应尺寸：`xs` `sm` `md` `lg` `xl` `xxl`。 |
| 监听 Breakpoint | 使用 `OnBreakpoint` 事件个性化布局。 |
| 排序 | 通过 `order` 来改变元素的排序。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<GridRow Gutter="16">
    <GridCol Span="16">主内容</GridCol>
    <GridCol Span="8">侧栏</GridCol>
</GridRow>
```

## 2. 区块间隔

关键差异：使用 `Row.Gutter` 设置水平或垂直间距，并可按断点提供响应式间距。

```razor
@{
    string style="background: #0092ff; padding: 8px 0;";
    Dictionary<string, int> gutter = new()
    {
        ["xs"] = 8,
        ["sm"] = 16,
        ["md"] = 24,
        ["lg"] = 32,
        ["xl"] = 48,
        ["xxl"] = 64
    };
}
<div>
    <Divider Orientation="DividerOrientation.Left">Horizontal</Divider>
    <GridRow Gutter="16">
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
    </GridRow>
    <Divider Orientation="DividerOrientation.Left">Responsive</Divider>
    <GridRow Gutter="@gutter">
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
    </GridRow>
    <Divider Orientation="DividerOrientation.Left">Vertical</Divider>
    <GridRow Gutter="(16,24)">
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
        <GridCol Class="gutter-row" Span="6">
            <div style="@style">col-6</div>
        </GridCol>
    </GridRow>

</div>

<style>
    .gutter-box {
        padding: 8px 0;
        background: #00a0e9;
    }
</style>
```

## 3. 左右偏移

关键差异：列偏移。 使用 `offset` 可以将列向右侧偏。例如，`offset={4}` 将元素向右侧偏移了 4 个列（column）的宽度。

```razor
<div>
    <GridRow>
        <GridCol Span="8">
            col-8
        </GridCol>
        <GridCol Span="8" Offset="8">
            col-8
        </GridCol>
    </GridRow>
    <GridRow>
        <GridCol Span="6" Offset="6">
            col-6 col-offset-6
        </GridCol>
        <GridCol Span="6" Offset="6">
            col-6 col-offset-6
        </GridCol>
    </GridRow>
    <GridRow>
        <GridCol Span="12" Offset="6">
            col-12 col-offset-6
        </GridCol>
    </GridRow>
</div>
@code{

}
```

## 4. 响应式布局

关键差异：参照 Bootstrap 的 [响应式设计](http://getbootstrap.com/css/#grid-media-queries)，预设六个响应尺寸：`xs` `sm` `md` `lg` `xl` `xxl`。

```razor
<GridRow>
    <GridCol Xs="2" Sm="4" Md="6" Lg="8" Xl="10">
      GridCol
    </GridCol>
    <GridCol Xs="20" Sm="16" Md="12" Lg="8" Xl="4">
      GridCol
    </GridCol>
    <GridCol Xs="2" Sm="4" Md="6" Lg="8" Xl="10">
      GridCol
    </GridCol>
</GridRow>
```

## 5. 监听 Breakpoint

关键差异：使用 `OnBreakpoint` 事件个性化布局。

```razor
<GridRow OnBreakpoint="HandleBreakpoint">
    Current break point:
    @foreach (var type in types)
    {
        if ((int)type <= (int)current)
        {
            <Tag Color="TagColor.Blue">@type</Tag>
        }
    }
</GridRow>

@code{

    BreakpointType[] types = new[] { BreakpointType.Xxl, BreakpointType.Xl, BreakpointType.Lg, BreakpointType.Md, BreakpointType.Sm, BreakpointType.Xs };

    BreakpointType current;

    void HandleBreakpoint(BreakpointType breakpoint)
    {
        current = breakpoint;
    }
}
```

## 6. 排序

关键差异：通过 `order` 来改变元素的排序。

```razor
<div>
    <Divider Orientation="DividerOrientation.Left">Normal</Divider>
    <GridRow>
        <GridCol Span="6" Order="4">
        1 col-order-4
        </GridCol>
        <GridCol Span="6" Order="3">
        2 col-order-3
        </GridCol>
        <GridCol Span="6" Order="2">
        3 col-order-2
        </GridCol>
        <GridCol Span="6" Order="1">
        4 col-order-1
        </GridCol>
    </GridRow>
    <Divider Orientation="DividerOrientation.Left">Responsive</Divider>
    <GridRow>
        <GridCol Span="6" Xs="new EmbeddedProperty{ Order= 1 }" Sm="new EmbeddedProperty{ Order= 2 }" Md="new EmbeddedProperty{ Order= 3 }" Lg="new EmbeddedProperty{ Order= 4 }">
        1 col-order-responsive
        </GridCol>
        <GridCol Span="6" Xs="new EmbeddedProperty{ Order= 2 }"  Sm="new EmbeddedProperty{ Order= 1 }"  Md="new EmbeddedProperty{ Order= 4 }"  Lg="new EmbeddedProperty{ Order= 3 }" >
        2 col-order-responsive
        </GridCol>
        <GridCol Span="6" Xs="new EmbeddedProperty{ Order= 3 }"  Sm="new EmbeddedProperty{ Order= 4 }"  Md="new EmbeddedProperty{ Order= 2 }"  Lg="new EmbeddedProperty{ Order= 1 }" >
        3 col-order-responsive
        </GridCol>
        <GridCol Span="6" Xs="new EmbeddedProperty{ Order= 4 }"  Sm="new EmbeddedProperty{ Order= 3 }"  Md="new EmbeddedProperty{ Order= 1 }"  Lg="new EmbeddedProperty{ Order= 2 }" >
        4 col-order-responsive
        </GridCol>
    </GridRow>
</div>

<style>
    #components-grid-demo-flex-order [class~='ant-row'] {
        background: rgba(128, 128, 128, 0.08);
    }
</style>
```
