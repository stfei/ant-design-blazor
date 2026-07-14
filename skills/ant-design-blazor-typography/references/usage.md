# Ant Design Blazor Typography 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 标题组件 | 展示不同级别的标题。 |
| 文本组件 | 内置不同样式的文本。 |
| 可交互 | 提供可编辑和可复制等额外的交互能力。 |
| 省略号 | 多行文本省略。 |
| 后缀 | 添加后缀的省略。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Title Level="2">页面标题</Title>
<Paragraph>
    这是一段正文，其中包含 <Text Strong>重点内容</Text>、
    <Text Code>inline code</Text> 和 <Text Copyable>可复制文本</Text>。
</Paragraph>
```

## 2. 标题组件

关键差异：展示不同级别的标题。

```razor
<div>
	<Title Level="1">h1. Ant Design 标题一</Title>
	<Title Level="2">h2. Ant Design 标题二</Title>
	<Title Level="3">h3. Ant Design 标题三</Title>
	<Title Level="4">h4. Ant Design 标题四</Title>
	<Title Level="5">h5. Ant Design 标题五</Title>
</div>
```

## 3. 文本组件

关键差异：内置不同样式的文本。

```razor
<div>
    <Text>Ant Design</Text>
    <br />
    <Text Type="TextElementType.Secondary">Ant Design</Text>
    <br />
    <Text Type="TextElementType.Success">Ant Design</Text>
    <br />
    <Text Type="TextElementType.Warning">Ant Design</Text>
    <br />
    <Text Type="TextElementType.Danger">Ant Design</Text>
    <br />
    <Text Disabled>Ant Design</Text>
    <br />
    <Text Mark>Ant Design</Text>
    <br />
    <Text Code>Ant Design</Text>
    <br />
    <Text Keyboard>Ant Design</Text>
    <br />
    <Text Underline>Ant Design</Text>
    <br />
    <Text Delete>Ant Design</Text>
    <br />
    <Text Strong>Ant Design</Text>
</div>
```

## 4. 可交互

关键差异：提供可编辑和可复制等额外的交互能力。

```razor
<Paragraph Editable EditConfig="editableStr">@editableStr.Text</Paragraph>
<Paragraph Editable EditConfig="editableStrWithSuffix">@editableStrWithSuffix.Text</Paragraph>
<Paragraph Editable EditConfig="customIconStr">@customIconStr.Text</Paragraph>
<Paragraph Editable EditConfig="customEnterIconStr">@customEnterIconStr.Text</Paragraph>
<Paragraph Editable EditConfig="clickTriggerStr">@clickTriggerStr.Text</Paragraph>
<Paragraph Editable EditConfig="noEnterIconStr">@noEnterIconStr.Text</Paragraph>
<Paragraph Editable EditConfig="lengthLimitedStr">@lengthLimitedStr.Text</Paragraph>

<Title Level="1" Editable EditConfig="h1">@h1.Text</Title>
<Title Level="2" Editable EditConfig="h2">@h2.Text</Title>
<Title Level="3" Editable EditConfig="h3">@h3.Text</Title>
<Title Level="4" Editable EditConfig="h4">@h4.Text</Title>
<Title Level="5" Editable EditConfig="h5">@h5.Text</Title>
<Divider />

<Paragraph Copyable>Simple copyable text</Paragraph>
<Paragraph Copyable CopyConfig="@selfDefinedText">Copy a self defiend text</Paragraph>
<Paragraph Copyable CopyConfig="@selfDefinedCopy">在控制台中查看此文本的log</Paragraph>
<Title Copyable>可复制的标题</Title>
<Paragraph>不可复制的段落中穿插着<Text Strong Copyable>可复制的文字</Text>以及其他</Paragraph>

@code
{
    public TypographyCopyableConfig selfDefinedText = new() { Text = "Just copied from self defined text" };
    public TypographyCopyableConfig selfDefinedCopy = new() { OnCopy = () => System.Console.WriteLine("Log in console as a self defined copy behavior") };

    TypographyEditableConfig editableStr = new() { Text = "This is an editable text." };
    TypographyEditableConfig editableStrWithSuffix = new() { Text = "This is a loooooooooooooooooooooooooooooooong editable text with suffix." };
    TypographyEditableConfig customIconStr = new() { Text = "Custom Edit icon and replace tooltip text." };
    TypographyEditableConfig clickTriggerStr = new() { Text = "Text or icon as trigger - click to start editing." };
    TypographyEditableConfig customEnterIconStr = new() { Text = "Editable text with a custom enter icon in edit field." };
    TypographyEditableConfig noEnterIconStr = new() { Text = "Editable text with no enter icon in edit field." };
    TypographyEditableConfig lengthLimitedStr = new() { Text = "This is an editable text with limited length." };

    TypographyEditableConfig h1 = new() { Text = "h1. Ant Design" };
    TypographyEditableConfig h2 = new() { Text = "h2. Ant Design" };
    TypographyEditableConfig h3 = new() { Text = "h3. Ant Design" };
    TypographyEditableConfig h4 = new() { Text = "h4. Ant Design" };
    TypographyEditableConfig h5 = new() { Text = "h5. Ant Design" };
}
```

## 5. 省略号

关键差异：多行文本省略。

```razor
<div>
To do
</div>
```

## 6. 后缀

关键差异：添加后缀的省略。

```razor
<div>
To do
</div>
```
