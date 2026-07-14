# Ant Design Blazor Calendar 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 卡片模式 | 用于嵌套在空间有限的容器中。 |
| 选择功能 | 一个通用的日历面板，支持年/月切换。 |
| 自定义头部 | 自定义日历头部内容。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Calendar OnPanelChange="HandlePanelChange" />

@code {
    private void HandlePanelChange(DateTime value, DatePickerType type)
    {
        Console.WriteLine($"{value:yyyy-MM-dd} / {type}");
    }
}
```

## 2. 卡片模式

关键差异：用于嵌套在空间有限的容器中。

```razor
<div class="site-calendar-demo-card">
    <Calendar FullScreen="@false" OnPanelChange="OnPanelChange" />
</div>

@code
{
    private void OnPanelChange(DateTime value, DatePickerType type)
    {
        Console.WriteLine($"{value.ToString("YYYY-MM-DD")} {type}");
    }
}

<Style>
    .site-calendar-demo-card {
        width: 300px;
        border: 1px solid #f0f0f0;
        border-radius: 2px;
    }
</Style>
```

## 3. 选择功能

关键差异：一个通用的日历面板，支持年/月切换。

```razor
<div>
    <Alert Message=@($"You selected date: {selectedValue.ToString("yyyy-MM-dd")}") />
    <Calendar Value="@value" OnSelect="OnSelect" OnPanelChange="OnPanelChange" />
</div>

@code
{
    private DateTime selectedValue = new DateTime(2017, 1, 25);
    private DateTime value = new DateTime(2017, 1, 25);

    private void OnSelect(DateTime value)
    {
        this.value = value;
        selectedValue = value;
    }

    private void OnPanelChange(DateTime value, DatePickerType type)
    {
        this.value = value;
    }
}
```

## 4. 自定义头部

关键差异：自定义日历头部内容。

```razor
<Calendar FullScreen="false" HeaderRender="HeaderRender" />

@code {
    private RenderFragment HeaderRender(CalendarHeaderRenderArgs args)
    {
        return @<Row Gutter="8">
            <RadioGroup TValue="CalendarMode"
                        Value="args.Type"
                        OnChange="args.OnTypeChange">
                <Radio RadioButton Value="CalendarMode.Month">月</Radio>
                <Radio RadioButton Value="CalendarMode.Year">年</Radio>
            </RadioGroup>
            <select value="@args.Value.Year"
                    @onchange="e => ChangeYear(e, args)">
                @foreach (var year in Enumerable.Range(args.Value.Year - 5, 11))
                {
                    <option value="@year">@year 年</option>
                }
            </select>
        </Row>;
    }

    private void ChangeYear(ChangeEventArgs e, CalendarHeaderRenderArgs args)
    {
        var year = Convert.ToInt32(e.Value);
        args.OnChange(new DateTime(year, args.Value.Month, 1));
    }
}
```
