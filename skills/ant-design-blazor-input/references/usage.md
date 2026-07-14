# Ant Design Blazor Input 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 前置/后置标签 | 用于配置一些固定组合。 |
| 密码框 | 密码框。 |
| 文本域 | 用于多行输入。 |
| 搜索框 | 带有搜索按钮的输入框 |
| 带移除图标 | 带移除图标的输入框，点击图标删除所有内容。 |
| 带字数提示 | 展示字数提示。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Input @bind-Value="_keyword"
       Placeholder="请输入关键字"
       AllowClear />

@code {
    private string? _keyword;
}
```

## 2. 前置/后置标签

关键差异：用于配置一些固定组合。

```razor
<div>
    <AntDesign.Input DefaultValue="@("mysite")"  >
        <AddOnBefore>https://</AddOnBefore>
        <AddOnAfter>.com</AddOnAfter>
    </AntDesign.Input>
    <br />
    <br />
    <AntDesign.Input TValue="string" @bind-Value=@CoreAddress>
        <AddOnBefore>
            <SimpleSelect @bind-Value=@_prefix Style="width: auto;" OnSelectedItemChanged="@OnItemChanged">
                <SelectOptions>
                    <SimpleSelectOption Value="http://" Label="http://"></SimpleSelectOption>
                    <SimpleSelectOption Value="https://" Label="https://"></SimpleSelectOption>
                </SelectOptions>
            </SimpleSelect>
        </AddOnBefore>
        <AddOnAfter>
            <SimpleSelect @bind-Value=@_suffix Style="width: auto;" OnSelectedItemChanged="@OnItemChanged">
                <SelectOptions>
                    <SimpleSelectOption Value=".com" Label=".com"></SimpleSelectOption>
                    <SimpleSelectOption Value=".jp" Label=".jp"></SimpleSelectOption>
                    <SimpleSelectOption Value=".cn" Label=".cn"></SimpleSelectOption>
                    <SimpleSelectOption Value=".org" Label=".org"></SimpleSelectOption>
                </SelectOptions>
            </SimpleSelect>
        </AddOnAfter>
    </AntDesign.Input>
    <br />
    <br />
    <AntDesign.Input DefaultValue="@("mysite")"  >
        <AddOnAfter><Icon Type="@IconType.Outline.Setting" /></AddOnAfter>
    </AntDesign.Input>
    <br />
    <br />
    <AntDesign.Input DefaultValue="@("mysite")" >
        <AddOnBefore>https://</AddOnBefore>
    </AntDesign.Input>

</div>
@code {
    string _prefix = "http://";
    string _suffix = ".com";
    string _coreAddress = "mysite";
    string CoreAddress
    {
        get => _coreAddress;
        set
        {
            _coreAddress = value;
            OnItemChanged(value);
        }
    }

    private void OnItemChanged(string value)
    {
        Console.WriteLine($"{_prefix}{_coreAddress.ToString()}{_suffix}");
    }
}
```

## 3. 密码框

关键差异：密码框。

```razor
<Space Direction="SpaceDirection.Vertical">
	<SpaceItem>
		<InputPassword @bind-Value="@txtValue1" Placeholder="large Password" Size="InputSize.Large" OnPressEnter="(e)=>Submit(e, txtValue1)" />
	</SpaceItem>
	<SpaceItem>
		<InputPassword
			@ref=@_inputPassword
			@bind-Value="@txtValue2"
			Placeholder="large Password"
			Size="InputSize.Large"
			OnPressEnter="(e)=>Submit(e, txtValue2)"
			IconRender="@actionSetting(() => OnClick(), attributes)"
			ShowPassword="@visible"/>
	</SpaceItem>
</Space>

<style>
    .custom-password-icon {
        color: rgba(0,0,0,.45);
        cursor: pointer;
        transition: all .3s;
    }
</style>
@code{
    RenderFragment actionSetting(Func<Task> clickActionAsync, Dictionary<string, object> iconAttributes) =>
        @<Icon @attributes="@iconAttributes" OnClick="@clickActionAsync" />;

    private string txtValue1 { get; set; }
    private string txtValue2 { get; set; }
    private bool visible = false;
    private InputPassword _inputPassword;

    Dictionary<string, object> hideAttributes  = new() {
        ["Type"] = IconType.Outline.EyeInvisible,
        ["Style"] = ""
    };

    Dictionary<string, object> showAttributes  = new() {
        ["Type"] = IconType.Outline.Eye,
        ["Style"] = "color: #1890ff"
    };

    Dictionary<string, object> attributes = new() {
        ["Class"] = "custom-password-icon",
        ["Type"] = IconType.Outline.EyeInvisible,
        ["Theme"] = IconThemeType.Outline
    };

    private void Submit(KeyboardEventArgs args, string password)
    {
        Console.WriteLine($"password: {password}");
    }

    private async Task OnClick()
    {
        Console.WriteLine("Custom icon clicked");
        visible = !visible;
        if (visible)
            attributes = showAttributes;
        else
            attributes = hideAttributes;
        await _inputPassword.Focus();
    }
}
```

## 4. 文本域

关键差异：用于多行输入。

```razor
<TextArea @bind-Value="value" Rows="4" />

@code {
    string value;
}
```

## 5. 搜索框

关键差异：带有搜索按钮的输入框

```razor
<Space Direction="SpaceDirection.Vertical">
    <SpaceItem>
        <Search Placeholder="input search text" WrapperStyle="width: 200px;" ClassicSearchIcon />
    </SpaceItem>
    <SpaceItem>
        <Search Placeholder="input search text" WrapperStyle="width: 200px;" AllowClear />
    </SpaceItem>
    <SpaceItem>
        <Search AddonBefore="@("https://".ToRenderFragment())" Placeholder="input search text" AllowClear Style="width: 304px; " />
    </SpaceItem>
    <SpaceItem>
        <Search Placeholder="input search text" EnterButton="true"/>
    </SpaceItem>
    <SpaceItem>
        <Search Placeholder="input search text" AllowClear EnterButton="@("Search")" Size="InputSize.Large"/>
    </SpaceItem>
    <SpaceItem>
        <Search Placeholder="input search text" EnterButton="@("Search")" Size="InputSize.Large" Suffix="@_audioIcon"/>
    </SpaceItem>
</Space>

@code {
    RenderFragment _audioIcon =@<Icon Type="@IconType.Outline.Audio" Style="color: #1890ff"/>;
}
```

## 6. 带移除图标

关键差异：带移除图标的输入框，点击图标删除所有内容。

```razor
<div>
    <Input Placeholder="input with clear icon" AllowClear="true" OnChange="onChange" TValue="string"/>
    <br />
    <br />
    <Input Placeholder="clear icon always shown" AllowClear="true" ShowClear="true" OnChange="onChange" TValue="string" />
    <br />
    <br />
    <TextArea Placeholder="textarea with clear icon"  AllowClear="true" OnChange="onChange" />
</div>
@code{

    private void onChange(string value)
    {
        Console.WriteLine("onChange =>" + value);
    }
}
```

## 7. 带字数提示

关键差异：展示字数提示。

```razor
<div>
    <AntDesign.Input TValue="string" ShowCount MaxLength="20" OnChange="OnChange" AllowClear Suffix="@("RMB".ToRenderFragment())" />
    <br />
    <br />
    <TextArea ShowCount MaxLength="100" OnChange="OnChange" />
</div>

@code {
    void OnChange(string value)
    {
        Console.WriteLine(value);
    }
}
```
