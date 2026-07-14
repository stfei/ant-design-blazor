# Ant Design Blazor Mentions 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 异步加载 | 匹配内容列表为异步返回时。 |
| 配合 Form 使用 | 受控模式，例如配合 Form 使用。 |
| 自定义触发字符 | 通过 `prefix` 属性自定义触发字符。默认为 `@`, 可以定义为数组。 |
| 向上展开 | 向上展开建议。 |
| 文本区域模板 | 自定义文本区域的呈现。最常见的用例是使用 TextArea 组件。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Mentions @bind-Value="_content" Placeholder="@("输入 @ 提及成员")">
    <MentionsOption Value="alice">Alice</MentionsOption>
    <MentionsOption Value="bob">Bob</MentionsOption>
</Mentions>

@code {
    private string? _content;
}
```

## 2. 异步加载

关键差异：匹配内容列表为异步返回时。

```razor
@using System.Threading;
<div>
    <Mentions Style="width:100%" Loading="true" LoadOptions="@LoadMentions">
    </Mentions>
</div>

@code
{
    private RenderFragment<string> OptionDisplay => value =>@<span>Display: @value</span>;

    public async Task<IEnumerable<MentionsDynamicOption>> LoadMentions(string search, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

        if (cancellationToken.IsCancellationRequested)
        {
            return Enumerable.Empty<MentionsDynamicOption>();
        }

        var fakeAsyncResults = new List<string>()
        {
            search
        };

        for (var i = 0; i < 25; i++){
            fakeAsyncResults.Add(search + i);
        }

        return fakeAsyncResults.Select(x => new MentionsDynamicOption
        {
            Value = x,
            Display = OptionDisplay(x)
        });
    }
}
```

## 3. 配合 Form 使用

关键差异：受控模式，例如配合 Form 使用。

```razor
@using System.ComponentModel.DataAnnotations;
@using System.Text.Json;
@using System.ComponentModel

<AntDesign.Form Model="@model"
        OnFinish="OnFinish"
        OnFinishFailed="OnFinishFailed"
        LabelColSpan="8"
        WrapperColSpan="16">
    <FormItem Label="Comment">
        <Mentions @bind-Value=@model.Comment>
            <MentionsOption Value="afc163">afc163</MentionsOption>
            <MentionsOption Value="zombieJ">zombieJ</MentionsOption>
            <MentionsOption Value="yesmeck">yesmeck</MentionsOption>
        </Mentions>
    </FormItem>
    <FormItem Label="Another Comment">
        <Mentions @bind-Value="@model.AnotherComment">
            <ChildContent>
                <MentionsOption Value="afc163">afc163</MentionsOption>
                <MentionsOption Value="zombieJ">zombieJ</MentionsOption>
                <MentionsOption Value="yesmeck">yesmeck</MentionsOption>
            </ChildContent>
            <TextareaTemplate Context="mentionsContext">
                <TextArea RefBack=@mentionsContext.RefBack
                          OnInput=@mentionsContext.OnInput
                          BindOnInput=false
                          OnKeyDown=@mentionsContext.OnKeyDown
                          Value=@mentionsContext.Value
                          ShowCount=true
                          MaxLength="200" />
            </TextareaTemplate>
        </Mentions>
    </FormItem>
    <FormItem WrapperColOffset="8" WrapperColSpan="16">
        <Button Type="ButtonType.Primary" HtmlType="submit">
            Submit
        </Button>
    </FormItem>
</AntDesign.Form>
@code
{
    public class Model
    {
        [Required]
        public string Comment { get; set; }

        [Required]
        public string AnotherComment{ get; set; }
    }

    private Model model = new Model();

    private void OnFinish(EditContext editContext)
    {
        Console.WriteLine($"Success:{JsonSerializer.Serialize(model)}");
    }

    private void OnFinishFailed(EditContext editContext)
    {
        Console.WriteLine($"Failed:{JsonSerializer.Serialize(model)}");
    }
}
```

## 4. 自定义触发字符

关键差异：通过 `prefix` 属性自定义触发字符。默认为 `@`, 可以定义为数组。

```razor
@using System.Text.Json


<Mentions @bind-Value="value"
          Prefix="@("@,#")"
          OnSearch="HandleSearch">
    @foreach (var option in currentOptions)
    {
        <MentionsOption Value="@option.Value" @key="@option.Value">
            @if (option.Type == "user")
            {
                <Space>
                    <SpaceItem>
                        <Avatar Size="AvatarSize.Small">@option.Value[0].ToString().ToUpper()</Avatar>
                    </SpaceItem>
                    <SpaceItem>
                        @option.Value
                    </SpaceItem>
                </Space>
            }
            else
            {
                <Space>
                    <SpaceItem>
                        <Icon Type="tag" />
                    </SpaceItem>
                    <SpaceItem>
                        @option.Value
                    </SpaceItem>
                </Space>
            }
        </MentionsOption>
    }
</Mentions>

@code {
    string value = string.Empty;
    List<OptionItem> currentOptions = new();

    readonly List<OptionItem> users = new()
    {
        new() { Value = "afc163", Type = "user" },
        new() { Value = "zombieJ", Type = "user" },
        new() { Value = "yesmeck", Type = "user" },
        new() { Value = "yangxiaodong", Type = "user" },
        new() { Value = "cipchk", Type = "user" }
    };

    readonly List<OptionItem> tags = new()
    {
        new() { Value = "angular", Type = "tag" },
        new() { Value = "ant-design", Type = "tag" },
        new() { Value = "blazor", Type = "tag" },
        new() { Value = "components", Type = "tag" },
        new() { Value = "design", Type = "tag" }
    };

    private void HandleSearch((string, string) args)
    {
        var (searchValue, prefix) = args;

        // 根据前缀选择要显示的选项列表
        var sourceList = prefix == "@" ? users : tags;

        // 根据搜索文本过滤选项
        currentOptions = sourceList
            .Where(item => string.IsNullOrEmpty(searchValue) ||
                          item.Value.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
            .ToList();

        StateHasChanged();
    }

    class OptionItem
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
```

## 5. 向上展开

关键差异：向上展开建议。

```razor
<div>

     To do
    @*<Mentions Placement="top" Placeholder="placement on bottom">
        <MentionsOption Value="afc163">afc163</MentionsOption>
        <MentionsOption Value="zombieJ">zombieJ</MentionsOption>
        <MentionsOption Value="yesmeck">yesmeck</MentionsOption>
    </Mentions>

    <br />

    <Mentions Placement="bottom" Placeholder="placement on bottom">
        <MentionsOption Value="afc163">afc163</MentionsOption>
        <MentionsOption Value="zombieJ">zombieJ</MentionsOption>
        <MentionsOption Value="yesmeck">yesmeck</MentionsOption>
    </Mentions>

    <br />*@
</div>
```

## 6. 文本区域模板

关键差异：自定义文本区域的呈现。最常见的用例是使用 TextArea 组件。

```razor
<Mentions>
    <ChildContent>
        <MentionsOption Value="afc163">afc163</MentionsOption>
        <MentionsOption Value="zombieJ">zombieJ</MentionsOption>
        <MentionsOption Value="yesmeck">yesmeck</MentionsOption>
    </ChildContent>
    <TextareaTemplate>
        <TextArea RefBack=@context.RefBack
                  OnInput=@context.OnInput
                  BindOnInput=false
                  OnKeyDown=@context.OnKeyDown
                  Value=@context.Value
                  ShowCount=true
                  MaxLength="200" />
    </TextareaTemplate>
</Mentions>

@code {
    string value { get; set; }
}
```
