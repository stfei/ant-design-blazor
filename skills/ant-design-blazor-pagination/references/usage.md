# Ant Design Blazor Pagination 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 受控 | 受控制的页码。 |
| 改变 | 改变每页显示条目数。 |
| 简洁 | 简单的翻页。 |
| 总数 | 通过设置 `showTotal` 展示总共有多少数据。 |
| 上一步和下一步 | 修改上一步和下一步为文字链接。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Pagination PageIndex="_pageIndex"
            PageSize="20"
            Total="240"
            PageIndexChanged="LoadPage" />

@code {
    private int _pageIndex = 1;
    private void LoadPage(int pageIndex) => _pageIndex = pageIndex;
}
```

## 2. 受控

关键差异：受控制的页码。

```razor
<div>
    <Pagination Current="@current" OnChange="OnChange" Total="50" />
</div>

Page: @current

@code {

    int current = 3;

    void OnChange(PaginationEventArgs args)
    {
        Console.WriteLine(args.Page);
        current = args.Page;
    }
}
```

## 3. 改变

关键差异：改变每页显示条目数。

```razor
<Pagination
    ShowSizeChanger
    OnShowSizeChange="OnShowSizeChange"
    DefaultCurrent="3"
    Total="500"/>
<br/>
<Pagination
    ShowSizeChanger
    OnShowSizeChange="OnShowSizeChange"
    DefaultCurrent="3"
    Total="500"
    Disabled/>

@code {

    private void OnShowSizeChange(PaginationEventArgs args)
    {
        var(current, pageSize) = args;
        Console.WriteLine($"{current}, {pageSize}");
    }

}
```

## 4. 简洁

关键差异：简单的翻页。

```razor
<Pagination Simple DefaultCurrent="2" Total="50" />
```

## 5. 总数

关键差异：通过设置 `showTotal` 展示总共有多少数据。

```razor
<div>
    <Pagination
        Total="85"
        ShowTotal=showTotal
        PageSize="20"
        DefaultCurrent="1"
    />
    <br />
    <Pagination
        Total="85"
        ShowTotal=showTotal2
        PageSize="20"
        DefaultCurrent="1"
    />
</div>

@code{
    Func<PaginationTotalContext, string> showTotal =  ctx => $"Total {ctx.Total} items";
    Func<PaginationTotalContext, string> showTotal2 =  ctx => $"{ctx.Range.Item1}-{ctx.Range.Item2} of {ctx.Total} items";
}
```

## 6. 上一步和下一步

关键差异：修改上一步和下一步为文字链接。

```razor
<Pagination Total="500" ItemRender="itemRender" />

@code {

    RenderFragment<PaginationItemRenderContext> itemRender = ctx =>
        @<Template>
            @if (ctx.Type == PaginationItemType.Prev) {
                <a disabled="@ctx.Disabled">Previous</a>
            }
            else if (ctx.Type == PaginationItemType.Next) {
                <a disabled="@ctx.Disabled">Next</a>
            }
            else
            {
                @ctx.OriginalElement(ctx)
            }
        </Template>;

}
```
