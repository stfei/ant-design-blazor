# Ant Design Blazor Checkbox 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 不可用 | checkbox 不可用。 |
| Checkbox组 | 方便的从数组生成 Checkbox 组。 |
| 全选 | 在实现全选效果时，你可能会用到 `indeterminate` 属性。 |
| 受控的Checkbox | 联动 checkbox。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Checkbox @bind-Checked="_accepted">我已阅读并同意协议</Checkbox>

@code {
    private bool _accepted;
}
```

## 2. 不可用

关键差异：checkbox 不可用。

```razor
<div>
            <Checkbox Disabled="true"/>
            <br />
            <Checkbox Checked="true" Disabled="true"/>
</div>
```

## 3. Checkbox组

关键差异：方便的从数组生成 Checkbox 组。

```razor
<div>
    <CheckboxGroup Options="@plainOptions" @bind-Value="@_value" TValue="string" OnChange="OnChange" />
    <br />
    <br />
    <CheckboxGroup Options="@options" @bind-Value="@_value" TValue="string" OnChange="OnChange2" />
    <br />
    <br />
    <CheckboxGroup Options="@OptionsWithDisabled" Disabled @bind-Value="@_value" TValue="string" OnChange="OnChange" />
    <br />
    <br />
    <CheckboxGroup Disabled @bind-Value="@_value" TValue="string" OnChange="OnChange">
        <Checkbox Label="Apple" />
        <Checkbox Label="Pear" />
        <Checkbox Label="Orange" />
    </CheckboxGroup>
</div>

@string.Join(",",_value)

@using System.Text.Json
@code {
    string[] _value = new[] { "Apple" };

    string[] plainOptions = { "Apple", "Pear", "Orange" };

    CheckboxOption<string>[] options = new CheckboxOption<string>[]
    {
        new() {Label = "Apple", Value = "Apple"},
        new() {Label = "Pear", Value = "Pear"},
        new() {Label = "Orange", Value = "Orange"},
    };

    CheckboxOption<string>[] OptionsWithDisabled = new CheckboxOption<string>[]
    {
        new() {Label = "Apple", Value = "Apple"},
        new() {Label = "Pear", Value = "Pear"},
        new() {Label = "Orange", Value = "Orange", Disabled = false},
    };

    void OnChange(string[] checkedValues)
    {
        Console.WriteLine($"checked = {JsonSerializer.Serialize(checkedValues)}");
    }

    void OnChange2(string[] checkedValues)
    {
        Console.WriteLine($"2 checked = {JsonSerializer.Serialize(checkedValues)}");
    }
}
```

## 4. 全选

关键差异：在实现全选效果时，你可能会用到 `indeterminate` 属性。

```razor
<div>
    <Checkbox Indeterminate="@indeterminate" Checked="@checkAll" CheckedChange="@CheckAllChanged">
    Check all
    </Checkbox>
    <br />
    <AntDesign.CheckboxGroup Options="@ckeckAllOptions" ValueChanged="@OnChanged" TValue="string"/>
</div>

@code{

    static CheckboxOption<string>[] ckeckAllOptions = new CheckboxOption<string>[]{
        new() { Label="Apple",Value="Apple" ,Checked=true},
        new() { Label="Pear", Value="Pear" },
        new(){ Label="Orange", Value="Orange",Checked=true },
    };

    void CheckAllChanged()
    {
        bool allChecked = checkAll;
        ckeckAllOptions.ForEach(o => o.Checked = !allChecked);
    }

    void OnChanged()
    {

    }

    bool indeterminate => ckeckAllOptions.Count(o => o.Checked) > 0 && ckeckAllOptions.Count(o => o.Checked) < ckeckAllOptions.Count();

    bool checkAll => ckeckAllOptions.All(o => o.Checked);

}
```

## 5. 受控的Checkbox

关键差异：联动 checkbox。

```razor
<div>
    <p style="margin-bottom: 20px">
        <Checkbox Checked="@checkValue"
            Disabled="@disableValue"
            CheckedChange="CheckChanged">
        @label
        </Checkbox>
    </p>
    <p>
        <Button Type="ButtonType.Primary" Size="ButtonSize.Small" OnClick="CheckClick">@checkTitle</Button>
        <Button Type="ButtonType.Primary" Size="ButtonSize.Small" OnCLick="DisableClick">@disableTitle</Button>
    </p>
</div>

@code {
      private bool checkValue { get; set; } = false;
      private bool disableValue { get; set; } = false;
      private string label { get; set; } = "Check-Enable";
      private string checkTitle { get; set; } = "Check";
      private string disableTitle { get; set; } = "Enable";

      void CheckChanged()
      {
          CheckClick();
      }

      void CheckClick()
      {
          if (checkValue)
          {
              checkTitle = "Check";
              checkValue = false;
          }
          else
          {
              checkTitle = "Uncheck";
              checkValue = true;
          }

          label = $"{checkTitle}-{disableTitle}";
      }



      void DisableClick()
      {
          if (disableValue)
          {
              disableTitle = "Enabled";
              disableValue = false;
          }
          else
          {
              disableTitle = "Disabled";
              disableValue = true;
          }
          label = $"{checkTitle}-{disableTitle}";
      }
  }
```
