# Ant Design Blazor Comment 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 嵌套评论 | 评论可以嵌套。 |
| 头像位置 | 可以设置头像位置，实现对话风格。 |
| 回复框 | 评论编辑器组件提供了相同样式的封装以支持自定义评论编辑器。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Comment Author="张三"
         Avatar="/images/avatar.png"
         Content="这个方案已经验证通过。" />
```

## 2. 嵌套评论

关键差异：评论可以嵌套。

```razor
<Comment Author="@author" Avatar="@avatar" Content="@content" Actions="@(new []{replyAction})">
    <Comment Author="@author" Avatar="@avatar" Content="@content" Actions="@(new []{replyAction})">
        <Comment Author="@author" Avatar="@avatar" Content="@content" Actions="@(new []{replyAction})"/>
        <Comment Author="@author" Avatar="@avatar" Content="@content" Actions="@(new []{replyAction})"/>
    </Comment>
</Comment>


@code{
    string author = "Han Solo";
    string avatar = @"https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png";
    string content = "We supply a series of design principles, practical patterns and high quality design resources (Sketch and Axure), to help people create their product prototypes beautifully and efficiently.";
    RenderFragment replyAction =@<span>Reply to</span>;
}
```

## 3. 头像位置

关键差异：可以设置头像位置，实现对话风格。

```razor
<Comment Author="@author" Datetime="9:00" Avatar="@avatar" Content="@content" Actions="@(new []{replyAction})" Placement="CommentPlacement.Left"/>
<Comment Author="@author" Datetime="9:00" Avatar="@avatar" Content="@content" Actions="@(new []{replyAction})" Placement="CommentPlacement.Right" />
<Comment Author="@author" Datetime="9:00" Avatar="@avatar" Content="@content" Actions="@(new []{replyAction})" Placement="CommentPlacement.Left" />
<Comment Author="@author" Datetime="9:00" Avatar="@avatar" Content="@content" Actions="@(new []{replyAction})" Placement="CommentPlacement.Right" />

@code {
    string author = "Han Solo";
    string avatar = @"https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png";
    string content = "We supply a series of design principles, practical patterns and high quality design resources (Sketch and Axure), to help people create their product prototypes beautifully and efficiently.";
    RenderFragment replyAction =@<span>Reply to</span>;
}
```

## 4. 回复框

关键差异：评论编辑器组件提供了相同样式的封装以支持自定义评论编辑器。

```razor
<div>
    @if (datas.Count > 0)
    {
        <AntList DataSource="datas" TItem="Data" Header="@header">
            <ListItem>
                <Comment Avatar="@context.Avatar" Author="@context.Author" Datetime="@context.Datetime" Content="@context.Content"></Comment>
            </ListItem>
        </AntList>
    }
    <Comment Avatar="@(@"https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png")">
        <ContentTemplate>
            @GetEditor(@onSubmit)
        </ContentTemplate>
    </Comment>
</div>


@code{
    List<Data> datas=new List<Data>();
    RenderFragment replyAction =@<span>Reply to</span>;

    RenderFragment header =>
        @<div>
            @if (datas.Any())
            {
                <span>
                    @($"{datas.Count} {(datas.Count > 1 ? "replies" : "reply")}")
                </span>
            }
        </div>;

    async Task onSubmit()
    {
        submitting = true;

        await Task.Delay(1000);
        this.datas.Add(new Data()
        {
            Author = "Han Solo",
            Avatar = @"https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png",
            Content = _value,
            Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        });
        submitting = false;
        _value = "";
        await InvokeAsync(StateHasChanged);
    }

    bool submitting=false;
    string _value = "";

    RenderFragment GetEditor(Func<Task> onSubmit)
    {
        return
            @<div>
                <TextArea MinRows="4" @bind-Value="@_value" />
                <br/>
                <br/>
                <Button Loading="@submitting" OnClick="onSubmit" Type="ButtonType.Primary">
                    Add Comment
                </Button>
            </div>;
    }


    class Data
    {
        public string Author { get; set; }
        public string Avatar { get; set; }
        public string Content { get; set; }
        public string Datetime;
    }
}
```
