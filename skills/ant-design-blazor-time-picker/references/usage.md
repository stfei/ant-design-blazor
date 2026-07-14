# Ant Design Blazor TimePicker 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 受控组件 | value 和 onChange 需要配合使用。 |
| 12 小时制 | 12 小时制的时间选择器，默认的 format 为 `hh:mm:ss tt`。 |
| 附加内容 | 在 TimePicker 选择框底部显示自定义的内容。 |
| 禁用 | 禁用时间选择。 |
| 三种大小 | 三种大小的输入框，大的用在表单中，中的为默认。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<TimePicker TValue="TimeOnly?"
            Value="_time"
            OnChange="HandleChange"
            Format="HH:mm" />

@code {
    private TimeOnly? _time = new(9, 0);
    private void HandleChange(DateTimeChangedEventArgs<TimeOnly?> args)
    {
        _time = args.Date;
    }
}
```

## 2. 受控组件

关键差异：value 和 onChange 需要配合使用。

```razor
<TimePicker TValue="TimeOnly?" Value="_value" OnChange="OnChange" />

@code
{
    private TimeOnly? _value = TimeOnly.FromDateTime(DateTime.Now);

    private void OnChange(DateTimeChangedEventArgs<TimeOnly?> args)
    {
        _value = args.Date;
    }
}
```

## 3. 12 小时制

关键差异：12 小时制的时间选择器，默认的 format 为 `hh:mm:ss tt`。

```razor
<div>
    <TimePicker TValue="TimeOnly?" Use12Hours />
</div>
```

## 4. 附加内容

关键差异：在 TimePicker 选择框底部显示自定义的内容。

```razor
<TimePicker TValue="TimeOnly?" RenderExtraFooter="ExtraFooter" />

@code {
    private RenderFragment ExtraFooter =@<Button Type="ButtonType.Primary">Ok</Button>;
}
```

## 5. 禁用

关键差异：禁用时间选择。

```razor
<TimePicker TValue="TimeOnly?" Disabled="@true"  Format="HH:mm:ss" DefaultValue="new TimeOnly(12, 08, 23)" />
```

## 6. 三种大小

关键差异：三种大小的输入框，大的用在表单中，中的为默认。

```razor
<TimePicker TValue="TimeOnly?" Size="InputSize.Large"
            Format="HH:mm:ss"
            DefaultValue="new TimeOnly( 12, 08, 23)" />
<br />
<TimePicker TValue="TimeOnly?" Format="HH:mm:ss"
            DefaultValue="new TimeOnly( 12, 08, 23)" />
<br />
<TimePicker TValue="TimeOnly?" Size="InputSize.Small"
            Format="HH:mm:ss"
            DefaultValue="new TimeOnly( 12, 08, 23)" />
```
