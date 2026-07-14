# Ant Design Blazor Anchor 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 静态位置 | 不浮动，状态不随页面滚动变化。 |
| 自定义 onClick 事件 | 点击锚点不记录历史。 |
| 设置锚点滚动偏移量 | 锚点目标滚动到屏幕正中间。 |
| 自定义锚点高亮 | 自定义锚点高亮。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Anchor>
    <AnchorLink Href="#overview" Title="概览" />
    <AnchorLink Href="#api" Title="API" />
</Anchor>

<h2 id="overview">概览</h2>
<h2 id="api">API</h2>
```

## 2. 静态位置

关键差异：不浮动，状态不随页面滚动变化。

```razor
<Anchor Affix="false">
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-basic" Title="@("Basic demo")" />
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-onClick" Title="@("OnClick demo")" />
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-targetOffset" Title="@("TargetOffset demo")" />
</Anchor>
```

## 3. 自定义 onClick 事件

关键差异：点击锚点不记录历史。

```razor
<Anchor OnClick="(e)=>OnLinkClick(e)">
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-basic" Title="@("Basic demo")" />
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-onClick" Title="@("OnClick demo")" />
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-targetOffset" Title="@("TargetOffset demo")" />
</Anchor>

@code{
    public void OnLinkClick(Tuple<MouseEventArgs, AnchorLink> tuple)
    {
        Console.WriteLine($"OnClick {tuple.Item2.Href}");
    }
}
```

## 4. 设置锚点滚动偏移量

关键差异：锚点目标滚动到屏幕正中间。

```razor
<Anchor OffsetTop="200">
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-basic" Title="@("Basic demo")" />
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-onClick" Title="@("OnClick demo")" />
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-targetOffset" Title="@("TargetOffset demo")" />
</Anchor>
```

## 5. 自定义锚点高亮

关键差异：自定义锚点高亮。

```razor
<Anchor Affix="false" GetCurrentAnchor="GetHref">
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-basic" Title="@("Basic demo")" />
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-onClick" Title="@("OnClick demo")" />
    <AnchorLink Href="/en-US/components/anchor#components-anchor-demo-targetOffset" Title="@("TargetOffset demo")" />
</Anchor>

@code{
    public string GetHref()
    {
        return "/en-US/components/anchor#components-anchor-demo-OnClick";
    }
}
```
