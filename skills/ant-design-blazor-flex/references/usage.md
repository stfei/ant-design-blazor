# Ant Design Blazor Flex 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 对齐方式 | 设置对齐方式。 |
| 设置间隙 | 使用 `Gap` 设置元素之间的间距，预设了 `FlexGap.Small`、`FlexGap.Middle`、`FlexGap.Large`` 三种尺寸，也可以自定义间距。 |
| 自动换行 | 自动换行。 |
| 组合使用 | 嵌套使用，可以实现更复杂的布局。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Flex Justify="FlexJustify.SpaceBetween"
      Align="FlexAlign.Center"
      Gap="FlexGap.Middle">
    <span>左侧内容</span>
    <Button Type="ButtonType.Primary">操作</Button>
</Flex>
```

## 2. 对齐方式

关键差异：设置对齐方式。

```razor
<Flex Gap="FlexGap.Middle" Align="FlexAlign.Start" Direction="FlexDirection.Vertical">
    <p>Select justify :</p>
    <Segmented TValue="string" Labels="justifyOptions" @bind-Value="justify" />
    <p>Select align :</p>
    <Segmented Labels="alignOptions" @bind-Value="alignItems" />
    <Flex style="@boxStyle" Justify="@justify" Align="@alignItems">
        <Button Type="ButtonType.Primary">Primary</Button>
        <Button Type="ButtonType.Primary">Primary</Button>
        <Button Type="ButtonType.Primary">Primary</Button>
        <Button Type="ButtonType.Primary">Primary</Button>
    </Flex>
</Flex>

@code {
    string boxStyle = "width:100%;height:120px;border-radius:6px;border:1px solid #40a9ff;";

    string[] justifyOptions = [
         "flex-start",
        "center",
        "flex-end",
        "space-between",
        "space-around",
        "space-evenly",
    ];

    string[] alignOptions = [
        "flex-start",
        "center",
        "flex-end"
    ];

    string justify;
    string alignItems;

}
```

## 3. 设置间隙

关键差异：使用 `Gap` 设置元素之间的间距，预设了 `FlexGap.Small`、`FlexGap.Middle`、`FlexGap.Large`` 三种尺寸，也可以自定义间距。

```razor
<Flex Gap="FlexGap.Middle" Direction="FlexDirection.Vertical">
    <span><Switch @bind-Checked="custom" /> Custom</span>
    @if (custom)
    {
        <Slider @bind-Value="customGapSize" />

        <Flex Gap="@($"{customGapSize}")">
            <Button Type="ButtonType.Primary">Primary</Button>
            <Button>Default</Button>
            <Button Type="ButtonType.Dashed">Dashed</Button>
            <Button Type="ButtonType.Link">Link</Button>
        </Flex>
    }
    else
    {
        <RadioGroup @bind-Value="gapSize" Options="gapSizeOptions"></RadioGroup>

        <Flex Gap="@gapSize">
            <Button Type="ButtonType.Primary">Primary</Button>
            <Button>Default</Button>
            <Button Type="ButtonType.Dashed">Dashed</Button>
            <Button Type="ButtonType.Link">Link</Button>
        </Flex>
    }

</Flex>

@code {
    RadioOption<FlexGap>[] gapSizeOptions = new RadioOption<FlexGap>[]
    {
        new(){ Value = FlexGap.Small, Label="Small", },
        new(){ Value = FlexGap.Middle, Label="Middle", },
        new(){ Value = FlexGap.Large, Label="Large", },
    };

    FlexGap gapSize;

    bool custom;
    double customGapSize;
}
```

## 4. 自动换行

关键差异：自动换行。

```razor
<Flex Wrap="FlexWrap.Wrap" Gap="FlexGap.Small">
    @foreach(var i in Enumerable.Range(0, 24))
    {
        <Button @key="i" Type="ButtonType.Primary">
            Button
        </Button>
    }
</Flex>
```

## 5. 组合使用

关键差异：嵌套使用，可以实现更复杂的布局。

```razor
<Card Hoverable Style="width: 620px;" BodyStyle="padding: 0; overflow: hidden;">
     <Flex Justify="FlexJustify.SpaceBetween">
         <img alt="avatar"
              src="https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png"
              style="display:block;width:273px;" />
         <Flex Direction="FlexDirection.Vertical" Align="FlexAlign.FlexEnd" Justify="FlexJustify.SpaceBetween" Style="padding: 32px;">
             <Title Level="3">
                 “antd is an enterprise-class UI design language and React UI library.”
             </Title>
             <a href="http://localhost:5000/zh-CN/docs/getting-started" target="_blank">
                 <Button Type="ButtonType.Primary">
                     Get Start
                 </Button>
             </a>
         </Flex>
     </Flex>
 </Card>
```
