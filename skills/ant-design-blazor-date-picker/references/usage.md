# Ant Design Blazor DatePicker 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 范围选择 | 使用 `RangePicker` 选择日期范围，并通过 `Picker` 切换周、月、季度或年份粒度。 |
| 不可选择日期和时间 | 可用 `disabledDate` 和 `disabledTime` 分别禁止选择部分日期和时间，其中 `disabledTime` 需要和 `showTime` 一起使用。 |
| 日期时间选择 | 增加选择时间功能，当 `showTime` 为一个对象时，其属性会传递给内建的 `TimePicker`。 |
| 日期格式 | 使用 `format` 属性，可以自定义日期显示格式。 |
| 预设范围 | 可以预设常用的日期范围以提高用户体验。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<DatePicker TValue="DateOnly?" @bind-Value="_date" />
<RangePicker TValue="DateTime?[]" @bind-Value="_range" />

@code {
    private DateOnly? _date;
    private DateTime?[]? _range;
}
```

## 2. 范围选择

关键差异：使用 `RangePicker` 选择日期范围，并通过 `Picker` 切换周、月、季度或年份粒度。

```razor
@using System.Text.Json;

<RangePicker TValue="DateTime?[]" />
<br />
<RangePicker TValue="DateTime?[]" ShowTime="@true" />
<br />
<RangePicker TValue="DateTime?[]" Picker="DatePickerType.Week" />
<br />
<RangePicker TValue="DateTime?[]" Picker="DatePickerType.Month" />
<br />
<RangePicker TValue="DateTime?[]" Picker="DatePickerType.Quarter" />
<br />
<RangePicker TValue="DateTime?[]" Picker="DatePickerType.Year" />

<RangePicker TValue="DateTime?[]" ShowTime="@("HH:mm")" OnChange="OnTimeRangeChange" />

@code {
    private void OnTimeRangeChange(DateRangeChangedEventArgs<DateTime?[]> args)
    {
        Console.WriteLine($"Selected Time: {JsonSerializer.Serialize(args.Dates)}");
        Console.WriteLine($"Formatted Selected Time: {JsonSerializer.Serialize(args.DateStrings)}");
    }
}
```

## 3. 不可选择日期和时间

关键差异：可用 `disabledDate` 和 `disabledTime` 分别禁止选择部分日期和时间，其中 `disabledTime` 需要和 `showTime` 一起使用。

```razor
<DatePicker TValue="DateTime?" ShowTime="@true"
               Format="@("yyyy-MM-dd HH:mm:ss")"
               DisabledDate="date => date <= DateTime.Today"
               DisabledTime="date => GetDisabledTime(date)"
               />
<br />
<DatePicker TValue="DateTime?" Picker="DatePickerType.Month"
               DisabledDate="date => date <= DateTime.Now"
                />
<br />
<RangePicker TValue="DateTime?[]" DisabledDate="date => date <= DateTime.Now"/>
<br />
<RangePicker TValue="DateTime?[]" ShowTime='@true'
                DisabledDate="date => date <= DateTime.Now"
                />

@code {
    private DatePickerDisabledTime GetDisabledTime(DateTime date)
    {
        int[] timeRange = new int[60];
        for (int i = 0; i < timeRange.Length; i++)
        {
            timeRange[i] = i;
        }

        return new DatePickerDisabledTime(timeRange[4..15], timeRange[20..55], timeRange[3..19]);
    }
}
```

## 4. 日期时间选择

关键差异：增加选择时间功能，当 `showTime` 为一个对象时，其属性会传递给内建的 `TimePicker`。

```razor
@using System.Text.Json;

<DatePicker TValue="DateTime?" ShowTime="@true" OnChange="OnChange" />
<br />
<RangePicker TValue="DateTime?[]" ShowTime='@("HH:mm")' OnChange="OnRangeChange" />

@code
{
    private void OnChange(DateTimeChangedEventArgs<DateTime?> args)
    {
        Console.WriteLine($"Selected Time: {args.Date}");
        Console.WriteLine($"Formatted Selected Time: {args.DateString}");
    }

    private void OnRangeChange(DateRangeChangedEventArgs<DateTime?[]> args)
    {
        Console.WriteLine($"Selected Time: {JsonSerializer.Serialize(args.Dates)}");
        Console.WriteLine($"Formatted Selected Time: {JsonSerializer.Serialize(args.DateStrings)}");
    }
}
```

## 5. 日期格式

关键差异：使用 `format` 属性，可以自定义日期显示格式。

```razor
<DatePicker @bind-Value="@_format1" Format="yyyy/MM/dd" />
<br />
<DatePicker @bind-Value="@_format2" Format="dd/MM/yyyy" />
<br />
<DatePicker @bind-Value="@_format3" Format="yyyy/MM"/>
<br />
<DatePicker @bind-Value="@_format4" Format="yyyy/MM/dd" />

@code {
	DateOnly? _format1 = new (2015, 1, 1);
    DateOnly? _format2 = new (2015, 1, 1);
    DateOnly? _format3 = new (2015, 1, 1);
    DateOnly? _format4 = new (2015, 1, 1);
}
```

## 6. 预设范围

关键差异：可以预设常用的日期范围以提高用户体验。

```razor
<div>
   <RangePicker  TValue="DateTime?[]" Ranges="dayRanges" ShowTime="true" OnChange="OnRangeChange" />
</div>
<div>
  <RangePicker TValue="DateTime?[]" Picker="@DatePickerType.Month" Ranges="monthRanges" />
</div>
<div>
    <RangePicker TValue="DateTime?[]" Picker="@DatePickerType.Year" Ranges="yearRanges" />
</div>

@code{
    Dictionary<string,DateTime?[]> dayRanges = new Dictionary<string, DateTime?[]>() {
        { "NextWeek",new DateTime?[] { DateTime.Now, DateTime.Now.AddDays(7) }},
        { "ThisMonth",new DateTime?[] { new DateTime(DateTime.Now.Year,DateTime.Now.Month,1), new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddMonths(1).AddDays(-1) }},
        { "LastThreeDays",new DateTime?[] { DateTime.Now.AddDays(-3), DateTime.Now }},
    };

    Dictionary<string,DateTime?[]> monthRanges = new Dictionary<string, DateTime?[]>() {
        { "FirstHalfYear",new DateTime?[] { new DateTime(DateTime.Now.Year,1,1), new DateTime(DateTime.Now.Year,6,30)}},
        { "SecondHalfYear",new DateTime?[] { new DateTime(DateTime.Now.Year,7,1), new DateTime(DateTime.Now.Year,12,31)}}
    };

    Dictionary<string,DateTime?[]> yearRanges = new Dictionary<string, DateTime?[]>() {
        { "ThisCentury",new DateTime?[] { new DateTime(2000,1,1), new DateTime(2099,12,31)}}
    };

    private void OnRangeChange(DateRangeChangedEventArgs<DateTime?[]> args)
    {
        Console.WriteLine($"Selected Time: {JsonSerializer.Serialize(args.Dates)}");
        Console.WriteLine($"Formatted Selected Time: {JsonSerializer.Serialize(args.DateStrings)}");
    }
}
```
