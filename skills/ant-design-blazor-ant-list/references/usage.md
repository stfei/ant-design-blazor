# Ant Design Blazor AntList 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 栅格列表 | 可以通过设置 `List` 的 `grid` 属性来实现栅格列表，`column` 可设置期望显示的列数。 |
| 可拖拽(Simple) | 为ListItem子组件包装一层div, 设置draggable='true', 并为之加上ondrop/ondragstart/ondragover事件处理函数. |
| 竖排列表样式 | 通过设置 `itemLayout` 属性为 `vertical` 可实现竖排列表样式。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<AntList TItem="TodoItem" DataSource="_items">
    <ListItem>
        <ListItemMeta Title="@context.Title"
                      Description="@context.Description" />
    </ListItem>
</AntList>

@code {
    private readonly TodoItem[] _items = new[]
    {
        new("检查订单", "确认客户和金额"),
        new("安排发货", "选择仓库和物流")
    };

    private sealed record TodoItem(string Title, string Description);
}
```

## 2. 栅格列表

关键差异：可以通过设置 `List` 的 `grid` 属性来实现栅格列表，`column` 可设置期望显示的列数。

```razor
<AntList Grid="grid" DataSource="@Data">
    <ListItem >
        <Card Bordered Title="@(context.Title)">
            <Body>
                Card context
            </Body>
        </Card>
    </ListItem>
</AntList>

@code{

    public ListGridType grid = new ListGridType { Gutter = 16, Column = 4 };

    public List<BasicItem> Data = new List<BasicItem>
    {
        new BasicItem { Title = "Title 1"},
        new BasicItem { Title = "Title 2"},
        new BasicItem { Title = "Title 3"},
        new BasicItem { Title = "Title 4"},
    };

    public class BasicItem
    {
        public string Title { get; set; }
    }
}
```

## 3. 可拖拽(Simple)

关键差异：为ListItem子组件包装一层div, 设置draggable='true', 并为之加上ondrop/ondragstart/ondragover事件处理函数.

```razor
<Divider Orientation="DividerOrientation.Left">Draggable</Divider>

<AntList Bordered DataSource="@data">
    <Header>Header</Header>
    <ChildContent Context="item">
        <ListItem>
            <div draggable="true" @ondrop="e=>OnDrop(e, item)" @ondragstart="e=>OnDragStart(e, item)" ondragover="event.preventDefault()">
                <span><Text Mark>[ITEM]</Text></span>@item
            </div>
        </ListItem>
    </ChildContent>

    <Footer>Footer</Footer>
</AntList>

@code {
    string _dragging;
    void OnDrop(DragEventArgs e, string s)
    {
        if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(_dragging)) {
            System.Diagnostics.Trace.WriteLine(s);
            int index = data.IndexOf(s);
            data.Remove(_dragging);
            data.Insert(index, _dragging);
            _dragging = null;
            StateHasChanged();
        }
    }

    void OnDragStart(DragEventArgs e, string s)
    {
        e.DataTransfer.DropEffect = "move";
        e.DataTransfer.EffectAllowed = "move";
        _dragging = s;
    }

    public List<string> data = new List<string> {
        "Racing car sprays burning fuel into crowd.",
        "Japanese princess to wed commoner.",
        "Australian walks 100km after outback crash.",
        "Man charged over missing wedding girl.",
        "Los Angeles battles huge wildfires."
    };
}
```

## 4. 竖排列表样式

关键差异：通过设置 `itemLayout` 属性为 `vertical` 可实现竖排列表样式。

```razor
@inject HttpClient HttpClient

<AntList DataSource="@ListData" ItemLayout="@ListItemLayout.Vertical">
    <ChildContent Context="item">
        <ListItem Extra="@extra" Actions="@(new[] { iconText(("start","156")),iconText(("like","156")),iconText(("message","2")) })">
            <ListItemMeta Description="@item.Description">
                <AvatarTemplate>
                    @avatar
                </AvatarTemplate>
                <TitleTemplate>
                    <a href="@item.Href">@item.Title</a>
                </TitleTemplate>
            </ListItemMeta>
            @item.Content
        </ListItem>
    </ChildContent>
    <Footer>
        <div>
            <b>ant design</b> footer part
        </div>
    </Footer>
</AntList>

@code {

    RenderFragment avatar =@<Avatar Src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png"></Avatar>;

    RenderFragment extra = @<img width="272" alt="logo" src="https://gw.alipayobjects.com/zos/rmsportal/mqaQswcyDLcXyDKnZfES.png" />;

    RenderFragment<(string icon, string text)> iconText = context =>
    @<Template>
        <Space>
            <SpaceItem><Icon Type="@context.icon" /></SpaceItem>
            <SpaceItem>@context.text</SpaceItem>
        </Space>
    </Template> ;

    public int count = 3;

    public string FakeDataUrl { get { return $"https://randomuser.me/api/?results={count}&inc=name,gender,email,nat&noinfo"; } }

    public List<DataModel> ListData { get; set; } = new List<DataModel>();

    public bool Loading { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        for (int i = 0; i < 3; i++)
        {
            ListData.Add(new DataModel
            {
                Href = "http://ant.design",
                Title = $"ant design part {i}",
                Avatar = "https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png",
                Description = "Ant Design, a design language for background applications, is refined by Ant UED Team.",
                Content = "We supply a series of design principles, practical patterns and high quality design resources (Sketch and Axure), to help people create their product prototypes beautifully and efficiently.",
            });
        }
        await base.OnInitializedAsync();
    }


    public class DataModel
    {
        public string Href { get; set; }

        public string Title { get; set; }

        public string Avatar { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }
    }

}
```
