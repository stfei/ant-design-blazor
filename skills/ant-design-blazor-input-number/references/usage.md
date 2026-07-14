# Ant Design Blazor InputNumber 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 小数 | 和原生的数字输入框一样，value 的精度由 step 的小数位数决定。 |
| 格式化展示 | 通过 `formatter` 格式化数字，以展示具有具体含义的数据，往往需要配合 `parser` 一起使用。 |
| 精度 | value 的精度由 Precision 决定。 |
| 本地化 | 可以设置 CultureInfo 以确保能正确解析字符串。在 Server Side 部署中特别有用。 |
| 三种大小 | 三种大小的数字输入框，当 size 分别为 large 和 small 时，输入框高度为 40px 和 24px ，默认高度为 32px。 |
| 不可用 | 点击按钮切换可用状态。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<AntDesign.InputNumber @bind-Value="_quantity"
                       Min="1"
                       Max="100"
                       Step="1"
                       PlaceHolder="请输入数量" />

@code {
    private int _quantity = 1;
}
```

## 2. 小数

关键差异：和原生的数字输入框一样，value 的精度由 step 的小数位数决定。

```razor
<div>
    <AntDesign.InputNumber Step="0.1" Min="-1" Max="5" @bind-Value="myValue" />
</div>

@code{
    private double myValue { get; set; }
}
```

## 3. 格式化展示

关键差异：通过 `formatter` 格式化数字，以展示具有具体含义的数据，往往需要配合 `parser` 一起使用。

```razor
@using System.Text.RegularExpressions
<div>
    <AntDesign.InputNumber Formatter="Format1" Parser="Parse1" DefaultValue="1000d"/>
    <AntDesign.InputNumber Formatter="Format2" Parser="Parse2" DefaultValue="100d" Min="0" Max="100"/>
</div>


@code{

    private double myValue { get; set; }

    private string Format1(double value)
    {
        return "$ " + value.ToString("n0");
    }

    private string Parse1(string value)
    {
       return Regex.Replace(value, @"\$\s?|(,*)", "");
    }

    private string Format2(double value)
    {
        return value.ToString() + "%";
    }

    private string Parse2(string value)
    {
        return value.Replace("%", "");
    }
}
```

## 4. 精度

关键差异：value 的精度由 Precision 决定。

```razor
<div>
    <AntDesign.InputNumber Precision="3" Min="-100" Max="100" @bind-Value="myValue" />
</div>

@code{
    private float myValue { get; set; }
}
```

## 5. 本地化

关键差异：可以设置 CultureInfo 以确保能正确解析字符串。在 Server Side 部署中特别有用。

```razor
@using System.Globalization
<div>
    <AntDesign.InputNumber ValueChanged="(double val)=>OnClickChange(val, 0)"></AntDesign.InputNumber>
    CurrentCulture: "@CultureInfo.CurrentCulture.Name" Input-Number = @myValue[0]
</div>
<div style="margin: 20px 0px 20px 0px;">
    <AntDesign.InputNumber CultureInfo="@CI" ValueChanged="(double val)=>OnClickChange(val, 1)"></AntDesign.InputNumber>
    CultureInfo: "@CI.Name": Input-Number = @myValue[1]
</div>

@code {
    private double[] myValue { get; set; } = new double[2];
    private CultureInfo CI = CultureInfo.GetCultureInfo("de-DE");
    private void OnClickChange(double val, int i)
    {
        Console.WriteLine(val);
        myValue[i] = val;
    }
}
```

## 6. 三种大小

关键差异：三种大小的数字输入框，当 size 分别为 large 和 small 时，输入框高度为 40px 和 24px ，默认高度为 32px。

```razor
<div>
    <AntDesign.InputNumber DefaultValue="3" Size="InputSize.Large" @bind-Value="myValue"/>
    <AntDesign.InputNumber DefaultValue="3" @bind-Value="myValue"/>
    <AntDesign.InputNumber DefaultValue="3" Size="InputSize.Small" @bind-Value="myValue"/>
</div>


@code{
    private double myValue { get; set; }
}
```

## 7. 不可用

关键差异：点击按钮切换可用状态。

```razor
<div>
    <AntDesign.InputNumber DefaultValue="3" Disabled="@disabled" @bind-Value="myValue" />
    <div style="margin: 20px 0px 20px 0px;">
        <Button Type="ButtonType.Primary" OnClick="(e)=> ToggleDisable()">Toggle Disabled</Button>
    </div>
</div>

@code{
    private bool disabled = true;

    private double myValue { get; set; }


    private void ToggleDisable()
    {
        disabled = !disabled;
    }
}
```
