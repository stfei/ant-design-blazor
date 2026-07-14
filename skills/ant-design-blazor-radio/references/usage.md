# Ant Design Blazor Radio 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 单选组合 | 一组互斥的 Radio 配合使用。 |
| 填底的按钮样式 | 实色填底的单选按钮样式。 |
| 不可用 | Radio 不可用. |
| 大小 | 大中小三种组合，可以和表单输入框进行对应配合。 |
| Radio.Group 组合 - 配置方式 | 通过配置 options 参数来渲染单选框。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<RadioGroup @bind-Value="_level">
    <Radio Value="1">低</Radio>
    <Radio Value="2">中</Radio>
    <Radio Value="3">高</Radio>
</RadioGroup>

@code {
    private int _level = 2;
}
```

## 2. 单选组合

关键差异：一组互斥的 Radio 配合使用。

```razor
<div>
    <RadioGroup @bind-Value="@_value" >
        <Radio Value="1">A</Radio>
        <Radio Value="2">B</Radio>
        <Radio Value="3">C</Radio>
        <Radio Value="4">D</Radio>
    </RadioGroup>
</div>
@code
 {

    int _value = 1;
}
```

## 3. 填底的按钮样式

关键差异：实色填底的单选按钮样式。

```razor
<div>
    <RadioGroup @bind-Value="radioValue4" ButtonStyle="RadioButtonStyle.Solid">
        <Radio RadioButton Value="@("A")">Hangzhou</Radio>
        <Radio RadioButton Value="@("B")">Shanghai</Radio>
        <Radio RadioButton Value="@("C")">Beijing</Radio>
        <Radio RadioButton Value="@("D")">Chengdu</Radio>
    </RadioGroup>
</div>
@code
{
    string radioValue4 = "A";
}
```

## 4. 不可用

关键差异：Radio 不可用.

```razor
<div>
    <Radio Checked Disabled="@Disabled" TValue="bool">
        Disabled
    </Radio>
    <br />
    <Radio Disabled="@Disabled" TValue="bool">
        Disabled
    </Radio>
    <div style="margin-top: 20px">
        <Button Type="ButtonType.Primary" OnClick="_=>Disabled=!Disabled">Toggle Disabled</Button>
    </div>
</div>

@code
{
    bool Disabled = true;
}
```

## 5. 大小

关键差异：大中小三种组合，可以和表单输入框进行对应配合。

```razor
<div>
    <RadioGroup @bind-Value="radioValue5" Size="InputSize.Large">
        <Radio RadioButton Value="@("A")">Hangzhou</Radio>
        <Radio RadioButton Value="@("B")">Shanghai</Radio>
        <Radio RadioButton Value="@("C")">Beijing</Radio>
        <Radio RadioButton Value="@("D")">Chengdu</Radio>
    </RadioGroup>
    <br />
    <br />
    <RadioGroup @bind-Value="radioValue5">
        <Radio RadioButton Value="@("A")">Hangzhou</Radio>
        <Radio RadioButton Value="@("B")">Shanghai</Radio>
        <Radio RadioButton Value="@("C")">Beijing</Radio>
        <Radio RadioButton Value="@("D")">Chengdu</Radio>
    </RadioGroup>
    <br />
    <br />
    <RadioGroup @bind-Value="radioValue5" Size="InputSize.Small">
        <Radio RadioButton Value="@("A")">Hangzhou</Radio>
        <Radio RadioButton Value="@("B")">Shanghai</Radio>
        <Radio RadioButton Value="@("C")">Beijing</Radio>
        <Radio RadioButton Value="@("D")">Chengdu</Radio>
    </RadioGroup>
</div>
@code
{
    string radioValue5 = "A";
}
```

## 6. Radio.Group 组合 - 配置方式

关键差异：通过配置 options 参数来渲染单选框。

```razor
<RadioGroup Options="@options" @bind-Value="_radioValue"></RadioGroup>
<br />
<RadioGroup Options="@options2" @bind-Value="_radioValue"></RadioGroup>
<br/>
<RadioGroup Options="@options2" @bind-Value="_radioValue" ButtonStyle="RadioButtonStyle.Outline"></RadioGroup>


@code {
	string _radioValue = "Apple";
    string[] options = new string[] { "Apple", "Pear", "Orange" };

	RadioOption<string>[] options2 = new RadioOption<string>[]
	{
		new(){ Value = "Apple", Label="🍎 Apple", },
		new(){ Value = "Pear", Label="🍐 Pear", },
		new(){ Value = "Orange", Label="🍊 Orange", },
	};
}
```
