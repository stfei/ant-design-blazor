# Ant Design Blazor AutoComplete 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 自定义输入组件 | 自定义输入组件。 |
| 使用内部过滤 | 如果希望预先加载可选项并使用内部过滤，需设置 `AllowFilter=true`，设置 `FilterExpression` 可修改过滤逻辑。 |
| 使用对象类型选项 | 当 `Value` 类型为 `object` 时使用 `compareWith`. |
| 修复滚动区域的浮层移动问题 | 修复滚动区域的浮层移动问题。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<AutoComplete @bind-Value="_value"
              Options="_options"
              Placeholder="输入城市" />

@code {
    private string? _value;
    private readonly string[] _options = new[] { "北京", "上海", "深圳" };
}
```

## 2. 自定义输入组件

关键差异：自定义输入组件。

```razor
<Divider>Input</Divider>
<AutoComplete Options="@options">
    <AutoCompleteInput Placeholder="input here" @bind-Value="@inputValue" />
</AutoComplete>
<br />
<span>bind-Value:@inputValue</span>

<Divider>Search</Divider>
<AutoComplete Options="@options">
    <AutoCompleteSearch Placeholder="input here" @bind-Value="@searchValue" />
</AutoComplete>
<br />
<span>bind-Value:@searchValue</span>


@code
{
    private string inputValue;
    private string searchValue;

    List<string> options = new List<string>(){
        "Beijing","Shanghai","Guangzhou","Shenzhen","Chongqing","Wuhan"
    };
}
```

## 3. 使用内部过滤

关键差异：如果希望预先加载可选项并使用内部过滤，需设置 `AllowFilter=true`，设置 `FilterExpression` 可修改过滤逻辑。

```razor
<span>Contains</span>
<AutoComplete AllowFilter Options="@options" FilterExpression="@((option,value)=>option.Label.Contains(value??"", StringComparison.InvariantCultureIgnoreCase))" />
<span>StartsWith</span>
<AutoComplete AllowFilter Options="@options" FilterExpression="@((option,value)=>option.Label.StartsWith(value??"", StringComparison.InvariantCultureIgnoreCase))" />

@code
{
    private List<string> options = new List<string>() { "Burns Bay Road", "Downing Street", "Wall Street" };
}
```

## 4. 使用对象类型选项

关键差异：当 `Value` 类型为 `object` 时使用 `compareWith`.

```razor
<AutoComplete @bind-Value="@value" Options="options" CompareWith="CompareWith" OnSelectionChange="OnSelectionChange">
    <OptionTemplate Context="option">
        <AutoCompleteOption Value="@option.Value" Label="@option.Label">
        </AutoCompleteOption>
    </OptionTemplate>
</AutoComplete>
<Divider></Divider>
<span>bind-Value:@value</span>
<br />
<span>SelectedValue:@(System.Text.Json.JsonSerializer.Serialize(selectItem?.Value))</span>

@code
{
    private string value;

    ObjectValueOption[] options = new ObjectValueOption[] {
                                    new ObjectValueOption()   { label= "Lucy", value= "lucy", age= 20 },
                                    new ObjectValueOption()  { label= "Jack", value= "jack", age= 22 },
                                    };


    Func<object, object, bool> CompareWith = (a, b) =>
    {
        if (a is ObjectValueOption o1 && b is ObjectValueOption o2)
        {
            return o1.value == o2.value;
        }
        else
        {
            return false;
        }
    };

    private AutoCompleteOption selectItem;

    void OnSelectionChange(AutoCompleteOption item)
    {
        selectItem = item;
    }

    public class ObjectValueOption
    {
        public string label { get; set; }
        public string value { get; set; }
        public int age { get; set; }

        public override string ToString()
        {
            return $"{label}-{age}";
        }
    }
}
```

## 5. 修复滚动区域的浮层移动问题

关键差异：修复滚动区域的浮层移动问题。

```razor
<div style="margin: 10px; overflow: scroll; height: 200px">
    <div style="padding: 100px; height: 1000px; background: #eee; position: relative " id="area">
        <AutoComplete PopupContainerSelector="body"
                      @bind-Value="@value"
                      Options="@options"
                      BoundaryAdjustMode="@TriggerBoundaryAdjustMode.InView"/>
    </div>
</div>

@code
{
    private string value;

    List<string> options = new List<string>(){
        "Beijing","Shanghai","Guangzhou","Shenzhen","Chongqing","Wuhan"
    };

}
```
