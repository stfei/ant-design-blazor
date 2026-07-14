# Ant Design Blazor Select 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 简化选择器 | `SimpleSelect` 省略泛型声明，分别用单值和 `DefaultValues` 实现单选、多选。 |
| 多选 | 多选，从已有条目中选择。 |
| 标签 | tags select，随意输入的内容（scroll the menu） |
| 带搜索框 | 展开后可对选项进行搜索。可设置 AutoClearSearchValue = false 来保留搜索框的值。 |
| 分组 | 使用 `GroupName` 按模型字段分组选项，并可通过 `SortByGroup`、`SortByLabel` 排序。 |
| 获得选项的文本 | 设置 `LabelInValue`，让选中项回调同时包含显示文本，而不只有值。 |
| 自定义选择标签 | 允许自定义选择标签的样式。 当要使用 Enabled/Disabled 状态, 需要通过 `LabelTemplateItem` 和 `LabelTemplateItemContent` 的 `Style` 属性设置禁用样式。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Select TItem="string"
        TItemValue="string"
        @bind-Value="_city"
        Placeholder="选择城市"
        AllowClear
        Style="width: 200px">
    <SelectOption Value="@("beijing")" Label="北京" />
    <SelectOption Value="@("shanghai")" Label="上海" />
</Select>

@code {
    private string? _city;
}
```

## 2. 简化选择器

关键差异：`SimpleSelect` 省略泛型声明，分别用单值和 `DefaultValues` 实现单选、多选。

```razor
<SimpleSelect DefaultValue="lucy" Style="width:120px;" OnSelectedItemChanged="handleChange">
    <SelectOptions>
        <SimpleSelectOption Value="jack" Label="Jack"></SimpleSelectOption>
        <SimpleSelectOption Value="lucy" Label="Lucy"></SimpleSelectOption>
        <SimpleSelectOption Value="disabled" Label="Disabled" Disabled></SimpleSelectOption>
        <SimpleSelectOption Value="Yiminghe" Label="yiminghe"></SimpleSelectOption>
    </SelectOptions>
</SimpleSelect>

<SimpleSelect DefaultValues=@(new[]{"lucy","jack"}) Mode="SelectMode.Multiple" Style="width:200px;" OnSelectedItemsChanged="handleItemsChange">
    <SelectOptions>
        <SimpleSelectOption Value="jack" Label="Jack"></SimpleSelectOption>
        <SimpleSelectOption Value="lucy" Label="Lucy"></SimpleSelectOption>
        <SimpleSelectOption Value="disabled" Label="Disabled" Disabled></SimpleSelectOption>
        <SimpleSelectOption Value="Yiminghe" Label="yiminghe"></SimpleSelectOption>
    </SelectOptions>
</SimpleSelect>

@code{
    void handleChange(string value)
    {
        Console.WriteLine(value);
    }

    void handleItemsChange(IEnumerable<string> value)
    {
        Console.WriteLine(value);
    }
}
```

## 3. 多选

关键差异：多选，从已有条目中选择。

```razor
<Select Mode="SelectMode.Multiple"
        Placeholder="Please select"
		@bind-Values="@_selectedValues1"
		TItemValue="string"
		TItem="string"
		OnSelectedItemsChanged="OnSelectedItemsChangedHandler"
		Style="width: 100%; margin-bottom: 8px;"
		EnableSearch
		AllowClear>
		<SelectOptions>
			@foreach(var item in _items)
			{
				<SelectOption TItemValue="string" TItem="string" Value=@item Label=@item />
			}
		</SelectOptions>
</Select>
<Select Mode="SelectMode.Multiple"
        Placeholder="Please select"
		@bind-Values="@_selectedValues2"
		TItemValue="string"
		TItem="string"
		Disabled>
		<SelectOptions>
			@foreach(var item in _items)
			{
				<SelectOption TItemValue="string" TItem="string" Value=@item Label=@item />
			}
		</SelectOptions>
</Select>
@code
{
    List<string> _items;
    IEnumerable<string> _selectedValues1, _selectedValues2;

	protected override void OnInitialized()
    {
        const int min = 10;
        const int max = 36;
        _items = new List<string>();

        for (var i = min; max > i; i++)
        {
            var value = Convert.ToString(i, 16).PadLeft(2, '0') + i.ToString();
            _items.Add(value);
        }

		_selectedValues1 = new List<string> { "0a10" , "0c12"};
		_selectedValues2 = new List<string> { "0a10" , "0c12"};
    }

    private void OnSelectedItemsChangedHandler(IEnumerable<string> values)
    {
		if (values != null)
			Console.WriteLine($"selected: ${string.Join(",", values)}");
    }
}
```

## 4. 标签

关键差异：tags select，随意输入的内容（scroll the menu）

```razor
<Select Mode="SelectMode.Tags"
        Placeholder="Please select"
		@bind-Values="@_selectedValues"
		TItemValue="string"
		TItem="string"
		OnSelectedItemsChanged="OnSelectedItemsChangedHandler"
		EnableSearch>
		<SelectOptions>
			@foreach (var item in _items)
			{
				<SelectOption TItemValue="string" TItem="string" Value="@item" Label="@item" />
			}
		</SelectOptions>
</Select>
@code
{
    List<string> _items;
    IEnumerable<string> _selectedValues;

	protected override void OnInitialized()
    {
        const int min = 10;
        const int max = 36;
        _items = new List<string>();

        for (var i = min; max > i; i++)
        {
            var value = Convert.ToString(i, 16).PadLeft(2, '0') + i.ToString();
            _items.Add(value);
        }

		_selectedValues = new List<string> { "0f15"};
    }

    private void OnSelectedItemsChangedHandler(IEnumerable<string> values)
    {
		if (values != null)
			Console.WriteLine($"selected: ${string.Join(",", values)}");
    }
}
```

## 5. 带搜索框

关键差异：展开后可对选项进行搜索。可设置 AutoClearSearchValue = false 来保留搜索框的值。

```razor
<Select DataSource="@_persons"
        @bind-Value="@_selectedValue"
        ItemValue="p=>p.Value"
        ItemLabel="p=>p.Name"
        Placeholder="Select a person"
        EnableSearch
        AutoClearSearchValue="false"
        OnSearch="OnSearch">
</Select>
<br />
<br />
<p>
    Selected Value: @_selectedValue <br />
    Selected Item Name: @_selectedItem?.Name
</p>

@code
{
    class Person
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }

    List<Person> _persons;
    string _selectedValue;
    Person _selectedItem;

    protected override void OnInitialized()
    {
        _persons = new List<Person>
        {
            new Person { Value = "jack", Name = "Jack" },
            new Person { Value = "lucy", Name = "Lucy" },
            new Person { Value = "tom" , Name = "Tom" }
        };
    }

    private void OnSelectedItemChangedHandler(Person value)
    {
        _selectedItem = value;
        Console.WriteLine($"selected: ${value?.Name}");
    }

    private void OnBlur()
    {
        Console.WriteLine("blur");
    }

    private void OnFocus()
    {
        Console.WriteLine("focus");
    }

    private void OnSearch(string value)
    {
        Console.WriteLine($"search: {value}");
    }
}
```

## 6. 分组

关键差异：使用 `GroupName` 按模型字段分组选项，并可通过 `SortByGroup`、`SortByLabel` 排序。

```razor
<Select TItem="Person"
        TItemValue="string"
        DataSource="@_persons"
        @bind-Value="@_selectedValue"
        ValueName="@nameof(Person.Value)"
        LabelName="@nameof(Person.Name)"
        GroupName="@nameof(Person.Role)"
        SortByLabel="SortDirection.Ascending"
        SortByGroup="SortDirection.Ascending"
        OnSelectedItemChanged="OnSelectedItemChangedHandler"
		DefaultActiveFirstOption="true"
		Style="width: 200px;">
</Select>
<br /><br />
<p>
    Selected Value: @_selectedValue <br/>
    Selected Item Name: @_selectedItem?.Name
</p>
@code
{
    class Person
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    List<Person> _persons;
    string _selectedValue;
    Person _selectedItem;

    protected override void OnInitialized()
    {
        _persons = new List<Person>
        {
            new Person {Value = "jack", Name = "Jack", Role = "Manager"},
            new Person {Value = "lucy", Name = "Lucy", Role = "Manager"},
            new Person {Value = "yaoming", Name = "Yaoming", Role = "Engineer"}
        };
    }

    private void OnSelectedItemChangedHandler(Person value)
    {
        _selectedItem = value;
		Console.WriteLine($"selected: ${value?.Name}");
    }
}
```

## 7. 获得选项的文本

关键差异：设置 `LabelInValue`，让选中项回调同时包含显示文本，而不只有值。

```razor
<Select @bind-Value="@_selectedValue"
        DefaultValue="@("jack")"
		TItemValue="string"
        TItem="string"
        LabelInValue="true"
        OnSelectedItemChanged="@(item => _selectedItem = item)"
		Style="width: 120px;">
		<SelectOptions>
			<SelectOption TItemValue="string" TItem="string" Value="@("jack")" Label="Jack (100)" />
			<SelectOption TItemValue="string" TItem="string" Value="@("lucy")" Label="Jack (101)" />
		</SelectOptions>
</Select>
<br /><br />
<p>
    Selected Value: @_selectedValue <br />
    Selected Item: @_selectedItem
</p>

@code
{
	string _selectedValue;
    string _selectedItem;
}
```

## 8. 自定义选择标签

关键差异：允许自定义选择标签的样式。 当要使用 Enabled/Disabled 状态, 需要通过 `LabelTemplateItem` 和 `LabelTemplateItemContent` 的 `Style` 属性设置禁用样式。

```razor
@using AntDesign.Select
<Select Mode="SelectMode.Multiple"
        DataSource="@_myColors"
        Disabled="@_disabled"
        Style="min-width:200px;"
        @bind-Values="@_selectedColorValues"
        ValueName="@nameof(MyColor.Value)"
        LabelName="@nameof(MyColor.Name)"
        ShowArrowIcon>
    <LabelTemplate>
        <LabelTemplateItem Context="item"
                           TItem="MyColor"
                           TItemValue="string"
                           Class=@($"ant-tag {(!_disabled?$"ant-tag-{context.Name}":"")}")
                           Style="margin-right: 4px;"
                           RemoveIconStyle="margin-top: 1px; display: inline-block;">
            <LabelTemplateItemContent>
                <span style=@($"color: {(_disabled? "rgba(0,0,0,.5)" :item.Name)}")>@item.Name</span>
            </LabelTemplateItemContent>
        </LabelTemplateItem>
    </LabelTemplate>
</Select>

<Switch @bind-Value=@_disabled CheckedChildren="Enabled" UnCheckedChildren="Disabled" />

@code
{
  bool _toggleSearch;
  bool _disabled = false;

  MyColor[] _myColors =
  {
      new MyColor { Value = "gold", Name = "gold"},
      new MyColor { Value = "lime", Name = "lime"},
      new MyColor { Value = "green", Name = "green"},
      new MyColor { Value = "cyan", Name = "cyan"}
  };

  IEnumerable<string> _selectedColorValues = new[] { "gold", "cyan" };

  class MyColor
  {
    public string Value { get; set; }
    public string Name { get; set; }
  }
}
```
