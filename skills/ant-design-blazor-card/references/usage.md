# Ant Design Blazor Card 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 简洁卡片 | 只包含内容区域。 |
| 更灵活的内容展示 | 可以利用 Card.Meta 支持更灵活的内容。 |
| 预加载的卡片 | 数据读入前会有文本块样式。 |
| 网格型内嵌卡片 | 一种常见的卡片内容区隔模式。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Card Title="订单信息" Style="width: 320px">
    <Extra><a href="/orders">更多</a></Extra>
    <ChildContent>
        <p>订单号：SO-1001</p>
        <p>状态：待发货</p>
    </ChildContent>
</Card>
```

## 2. 简洁卡片

关键差异：只包含内容区域。

```razor
<div>
    <Card Bordered="true" Style="width: 300px">
        <p>Card content</p>
        <p>Card content</p>
        <p>Card content</p>
    </Card>
</div>
```

## 3. 更灵活的内容展示

关键差异：可以利用 Card.Meta 支持更灵活的内容。

```razor
<div>
    <Card Hoverable Style="width: 240px" Cover="coverTemplate">
        <CardMeta Title="Europe Street beat" Description="www.instagram.com"/>
    </Card>
</div>


@code
{

    private RenderFragment coverTemplate =@<img alt="example"  src="https://os.alipayobjects.com/rmsportal/QBnOOoLaAfKPirc.png" />;

}
```

## 4. 预加载的卡片

关键差异：数据读入前会有文本块样式。

```razor
<div>

    <Switch Checked="!loading" OnChange="OnChange">
        Toggle loading
    </Switch>

    <Card Loading="loading" Style="width: 300px; margin-top: 16px">
        <CardMeta Avatar="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png"
                  Title="Card title"
                  Description="This is the description" />
    </Card>

    <Card Style="width: 300px; margin-top: 16px"
          Actions="new[] { actionSetting, actionEdit, actionEllipsis }">
        <Skeleton Loading="loading" Avatar Active>
            <CardMeta Title="Card title"
                      Description="This is the description"
                      Avatar="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png" />
        </Skeleton>
    </Card>

</div>

@code
{
    bool loading = true;
    void OnChange()
    {
        loading = !loading;
    }

    private RenderFragment actionSetting =@<Template>
        <Icon Type="@IconType.Outline.Setting" />
    </Template>;

    private RenderFragment actionEdit =@<Template>
        <Icon Type="@IconType.Outline.Edit" />
    </Template>;

    private RenderFragment actionEllipsis =@<Template>
        <Icon Type="@IconType.Outline.Ellipsis" />
    </Template>;

}
```

## 5. 网格型内嵌卡片

关键差异：一种常见的卡片内容区隔模式。

```razor
<div>
    <Card Title=@("Card Title")>
        <CardGrid Style="width:25%;text-align:center" Hoverable="true">
            Content
        </CardGrid>
        <CardGrid Style="width:25%;text-align:center" Hoverable="false">
            Content
        </CardGrid>
        <CardGrid Style="width:25%;text-align:center" Hoverable="true">
            Content
        </CardGrid>
        <CardGrid Style="width:25%;text-align:center" Hoverable="true">
            Content
        </CardGrid>
        <CardGrid Style="width:25%;text-align:center" Hoverable="true">
            Content
        </CardGrid>
        <CardGrid Style="width:25%;text-align:center"Hoverable="true">
            Content
        </CardGrid>
        <CardGrid Style="width:25%;text-align:center" Hoverable="true">
            Content
        </CardGrid>
        <CardGrid Style="width:25%;text-align:center" Hoverable="true">
            Content
        </CardGrid>

    </Card>
</div>
```
