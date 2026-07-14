# Ant Design Blazor Form 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 自定义列布局 | 使用`ColLayoutParam`对列宽度自定义设置 |
| 动态属性 | 绑定字典可以不必事先定义属性，因此能够方便地实现动态表单。 |
| 判断表单是否被修改 | 通过 `IForm.IsModified` 判断表单修改。 |
| 自定义表单验证器 | 用 `Validator` 或 `<Validator>` 替换内置验证器；使用命名内容时，其他表单项放进 `<ChildContent>`。 |
| 开启组件值变更时验证 | （v0.5+）为了性能考虑，默认关闭内容变更验证，在调用 `form.Validate()` 时才验证。使用 `ValidateOnChange` 属性可开启。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
@using System.ComponentModel.DataAnnotations

<Form Model="_model" OnFinish="HandleSubmit">
    <FormItem Label="用户名">
        <Input @bind-Value="context.UserName" />
    </FormItem>
    <FormItem Label="邮箱">
        <Input @bind-Value="context.Email" />
    </FormItem>
    <FormItem>
        <Button Type="ButtonType.Primary" HtmlType="submit">提交</Button>
    </FormItem>
</Form>

@code {
    private readonly UserModel _model = new();

    private void HandleSubmit(EditContext _) { }

    private sealed class UserModel
    {
        [Required] public string UserName { get; set; } = string.Empty;
        [EmailAddress] public string Email { get; set; } = string.Empty;
    }
}
```

## 2. 自定义列布局

关键差异：使用`ColLayoutParam`对列宽度自定义设置

```razor
@using System.ComponentModel.DataAnnotations;
@using System.Text.Json;

<Form Model="@model"
      LabelCol="new ColLayoutParam { Span = 8 }"
      WrapperCol="new ColLayoutParam { Span = 16 }">
    <FormItem Label="Username">
        <Input @bind-Value="@context.Username" />
    </FormItem>
    <FormItem Label="Password">
        <InputPassword @bind-Value="@context.Password" />
    </FormItem>
    <FormItem WrapperCol="new ColLayoutParam{ Offset = 8, Span = 16 }">
        <Checkbox @bind-Value="context.RememberMe">Remember me</Checkbox>
    </FormItem>
    <FormItem WrapperCol="new ColLayoutParam{ Offset = 8, Span = 16 }">
        <Button Type="ButtonType.Primary" HtmlType="submit">
            Submit
        </Button>
    </FormItem>
</Form>
@code
{
    public class Model
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }

    private Model model = new Model();

}
```

## 3. 动态属性

关键差异：绑定字典可以不必事先定义属性，因此能够方便地实现动态表单。

```razor
@using System.ComponentModel.DataAnnotations;
@using System.ComponentModel
@using System.Reflection

<Form Model="@model"
      OnFinish="OnFinish"
      OnFinishFailed="OnFinishFailed"
      LabelColSpan="8"
      WrapperColSpan="16" ValidateMode="@FormValidateMode.Rules">

    @foreach (var field in model)
    {
        <FormItem Name="@field.Key" Rules="[new FormValidationRule(){ Required = true }]" Required>
            <Input TValue="string" />
        </FormItem>
    }

    <FormItem WrapperColOffset="8" WrapperColSpan="16">
        <Button OnClick="AddField" Style="width:100%;">Add Field</Button>
    </FormItem>
    <FormItem WrapperColOffset="8" WrapperColSpan="16">
        <Button Type="ButtonType.Primary" HtmlType="submit">
            Submit
        </Button>
    </FormItem>
</Form>

@code
{
    private Dictionary<string, object> model = new() { ["Field1"] = "" };

    private void AddField()
    {
        model.Add("Field" + model.Count + 1, "");
    }

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

## 4. 判断表单是否被修改

关键差异：通过 `IForm.IsModified` 判断表单修改。

```razor
@using System.ComponentModel.DataAnnotations;
@using System.Text.Json;

    <Form @ref="form" Model="@model"
          ValidateOnChange="@true"
          OnFinish="OnFinish"
          OnFinishFailed="OnFinishFailed"
      LabelColSpan="8"
      WrapperColSpan="16">
        <FormItem Label="Username">
            <Input @bind-Value="@context.Username" />
        </FormItem>
        <FormItem Label="Password">
            <InputPassword @bind-Value="@context.Password" />
        </FormItem>
        <FormItem WrapperColOffset="8" WrapperColSpan="16">
            <Checkbox @bind-Value="context.RememberMe">Remember me</Checkbox>
        </FormItem>
        <FormItem WrapperColOffset="8" WrapperColSpan="16">
            <Button Type="ButtonType.Primary" HtmlType="submit" Disabled="!form.IsModified">
                Submit
            </Button>
        </FormItem>
    </Form>
@code
{
    public class Model
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }

    Form<Model> form;

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

## 5. 自定义表单验证器

关键差异：用 `Validator` 或 `<Validator>` 替换内置验证器；使用命名内容时，其他表单项放进 `<ChildContent>`。

```razor
@using System.ComponentModel.DataAnnotations;

<Form Model="@model1"
LabelColSpan="8"
WrapperColSpan="16"
Validator="null">
	<FormItem Label="No Validator">
		<AntDesign.InputNumber @bind-Value="@context.Number"/>
	</FormItem>
</Form>

<Form Model="@model2"
	LabelColSpan="8"
	WrapperColSpan="16">
	<Validator>
		@validator
	</Validator>
	<ChildContent>
		<FormItem Label="CustomValidator">
			<AntDesign.InputNumber @bind-Value="@context.Number"/>
		</FormItem>
	</ChildContent>
</Form>

@code
{
	public class Model1
	{
		[Required]
		public int Number { get; set; }
	}

	public class Model2
	{
		[Required]
		public int Number { get; set; }
	}

	private Model1 model1 = new Model1();

	private Model2 model2 = new Model2();

	private RenderFragment validator = b =>
	{
		b.OpenComponent<CustomValidator>(0);
		b.CloseComponent();
	};

	public class CustomValidator : ComponentBase
	{
		[CascadingParameter]
		internal EditContext EditContext { get; set; }

		protected override void OnInitialized()
		{
			var messages = new ValidationMessageStore(EditContext);
			EditContext.OnFieldChanged += (sender, args) =>
			{
				messages.Clear();
				messages.Add(args.FieldIdentifier, "Message from custom validator");
				EditContext.NotifyValidationStateChanged();
			};
		}
	}
}
```

## 6. 开启组件值变更时验证

关键差异：（v0.5+）为了性能考虑，默认关闭内容变更验证，在调用 `form.Validate()` 时才验证。使用 `ValidateOnChange` 属性可开启。

```razor
@using System.ComponentModel.DataAnnotations;

<Switch @bind-Value="@auto" CheckedChildren="True" UnCheckedChildren="False" />
<Form @ref="form"
      ValidateOnChange="@auto"
      Model="@model"
      LabelColSpan="8"
      WrapperColSpan="16">
    <FormItem Label="Username">
        <Input @bind-Value="@context.Username" />
    </FormItem>
    <FormItem Label="Password">
        <InputPassword @bind-Value="@context.Password" />
    </FormItem>
    <FormItem WrapperColOffset="8" WrapperColSpan="16">
        <Checkbox @bind-Value="context.RememberMe">Remember me</Checkbox>
    </FormItem>
    <FormItem WrapperColOffset="8" WrapperColSpan="16">
        <Button Type="ButtonType.Primary" OnClick="OnValidate">
            Validate
        </Button>
    </FormItem>
</Form>
@code
{
    public class Model
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }

    private Model model = new Model();

    AntDesign.Form<Model> form;

    private bool auto = false;

    public void OnValidate()
    {
        form.Validate();
    }
}
```
