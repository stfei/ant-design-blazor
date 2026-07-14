# Ant Design Blazor Slider 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 带输入框的滑块 | 和 数字输入框 组件保持同步. |
| 带标签的滑块 | 使用 `marks` 属性标注分段式滑块，使用 `value` / `defaultValue` 指定滑块位置。当 `included=false` 时，表明不同标记间为并列关系。当 `step=null` 时，Slider 的可选值仅有 `marks` 标出来的部分。 |
| 垂直 | 垂直方向的 Slider。 |
| 反向 | 设置 `reverse` 可以将滑动条置反。 |
| 事件 | 当 Slider 的值发生改变时，会触发 `onChange` 事件，并把改变后的值作为参数传入。在 `onmouseup` 时，会触发 `onAfterChange` 事件，并把当前值作为参数传入。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Slider TValue="double" @bind-Value="_value" Min="0" Max="100" />
<Slider TValue="(double, double)" @bind-Value="_range" Min="0" Max="100" />

@code {
    private double _value = 35;
    private (double, double) _range = (20, 80);
}
```

## 2. 带输入框的滑块

关键差异：和 数字输入框 组件保持同步.

```razor
<div>
    <Row>
        <Col Span="12">
        <Slider TValue="double" Min="1" Max="20" @bind-Value="@inputValue1" />
        </Col>
        <Col Span="4">
        <AntDesign.InputNumber Min="1" Max="20" Style="margin:0 16px;" @bind-Value="@inputValue1" TValue="double" />
        </Col>
    </Row>
    <Row>
        <Col Span="12">
        <Slider TValue="double" Min="0" Max="1" Step="0.01" @bind-Value="@inputValue2" />
        </Col>
        <Col Span="4">
        <AntDesign.InputNumber Min="0" Max="10" Step="0.1" Style="margin:0 16px;" @bind-Value="@inputValue2" TValue="double" />
        </Col>
    </Row>
</div>


@code
{
    private double inputValue1 = 1;

    private double inputValue2 = 0.5;
}
```

## 3. 带标签的滑块

关键差异：使用 `marks` 属性标注分段式滑块，使用 `value` / `defaultValue` 指定滑块位置。当 `included=false` 时，表明不同标记间为并列关系。当 `step=null` 时，Slider 的可选值仅有 `marks` 标出来的部分。

```razor
<div>
    <h4>included=true</h4>
    <Slider TValue="double" Marks="@nMarks" DefaultValue="37" />
    <Slider TValue="(double, double)" Marks="@nMarks" DefaultValue="(26, 37)" />

    <h4>included=false</h4>
    <Slider TValue="double" Marks="@nMarks" Included="false" DefaultValue="37" />

    <h4>marks & step</h4>
    <Slider TValue="double" Marks="@nMarks" Step="10" DefaultValue="37" />

    <h4>step=null</h4>
    <Slider TValue="double" Marks="@nMarks" Step="null" DefaultValue="37" />

</div>

@code
{
    private SliderMark[] nMarks =
     {
            new SliderMark(0, "0℃"),
            new SliderMark(26, "26℃"),
            new SliderMark(37, "37℃"),
            new SliderMark(100, (b)=>{
                b.OpenElement(0,"strong");
                b.AddContent(1,"100℃");
                b.CloseElement();
            }, "color: #f50;")
     };
}
```

## 4. 垂直

关键差异：垂直方向的 Slider。

```razor
<div>
    <div style="height: 300px; margin-left: 70px; display: inline-block;">
        <Slider TValue="double" Vertical DefaultValue="31" />
    </div>
    <div style="height: 300px; margin-left: 70px; display: inline-block;">
        <Slider TValue="(double, double)" Vertical Step="10" DefaultValue="(20, 50 )" />
    </div>
    <div style="height: 300px; margin-left: 70px; display: inline-block;">
        <Slider TValue="(double, double)" Vertical Marks="@_marks1" DefaultValue="(26, 37 )" />
    </div>
</div>

@code
{
        private SliderMark[] _marks1 =
        {
            new SliderMark(0, "0℃"),
            new SliderMark(26, "26℃"),
            new SliderMark(37, "37℃"),
            new SliderMark(100, (b)=>{
                b.OpenElement(0,"strong");
                b.AddContent(1,"100℃");
                b.CloseElement();
            }, "color: #f50;")
        };
}
```

## 5. 反向

关键差异：设置 `reverse` 可以将滑动条置反。

```razor
<div>
    <Slider TValue="double" Reverse="@reversed" DefaultValue="33" />
    <Slider TValue="(double, double)" Reverse="@reversed" DefaultValue="(20, 50)" />
    Reversed: <Switch Size="InputSize.Small" Checked="@reversed" OnChange="(e)=>OnSwitchReverse(e)" />
</div>


@code
{
    private bool reversed = true;

    private void OnSwitchReverse(bool args)
    {
        reversed = args;
    }
}
```

## 6. 事件

关键差异：当 Slider 的值发生改变时，会触发 `onChange` 事件，并把改变后的值作为参数传入。在 `onmouseup` 时，会触发 `onAfterChange` 事件，并把当前值作为参数传入。

```razor
<div>
    <Slider TValue="double" DefaultValue="30.5" OnChange="(e)=>OnChange1(e)" OnAfterChange="(e)=>OnAfterChange1(e)" />
    <Slider TValue="(double, double)" Step="10" DefaultValue="( 20.3, 50.3 )" OnChange="(e)=>OnChange2(e)" OnAfterChange="(e)=>OnAfterChange2(e)" />
</div>


@code {

    private void OnChange1(double args)
    {
    }

    private void OnAfterChange1(double args)
    {
    }

    private void OnChange2((double, double) args)
    {
    }

    private void OnAfterChange2((double, double) args)
    {
    }
}
```
