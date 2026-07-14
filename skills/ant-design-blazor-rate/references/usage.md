# Ant Design Blazor Rate 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 半星 | 支持选中半星。 |
| 文案展现 | 给评分组件加上文案展示。 |
| 其他字符 | 可以将星星替换为其他字符，比如字母，数字，字体图标甚至中文。 |
| 只读 | 只读，无法进行鼠标交互。 |
| 清除 | 支持允许或者禁用清除。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Rate @bind-Value="_score" AllowHalf Tooltips="_tooltips" />

@code {
    private decimal _score = 4.5m;
    private readonly string[] _tooltips = new[] { "很差", "较差", "一般", "满意", "很好" };
}
```

## 2. 半星

关键差异：支持选中半星。

```razor
<Rate AllowHalf="true" DefaultValue="3.5M" />
```

## 3. 文案展现

关键差异：给评分组件加上文案展示。

```razor
<Rate @bind-Value="value" Tooltips="@desc" />
<span class="ant-rate-text">@(desc[(int)value-1])</span>

@code
{
    string[] desc = new string[] { "terrible", "bad", "normal", "good", "wonderful" };
    decimal value = 3M;
}
```

## 4. 其他字符

关键差异：可以将星星替换为其他字符，比如字母，数字，字体图标甚至中文。

```razor
<Rate Character="@Character1" AllowHalf="true" DefaultValue="3" />
<br />
<Rate Character="@Character2" AllowHalf="true" DefaultValue="3" />
<br />
<Rate Character="@Character3" AllowHalf="true" DefaultValue="3" />

@code
{
    RenderFragment<RateItemRenderContext> Character1 = (builder) =>
    @<Template>
        <Icon Type="@IconType.Fill.Heart" />
    </Template>;

    RenderFragment<RateItemRenderContext> Character2 = (builder) =>
    @<Template>
        A
    </Template>;

    RenderFragment<RateItemRenderContext> Character3 = (builder) =>
    @<Template>
        好
    </Template>;
}
```

## 5. 只读

关键差异：只读，无法进行鼠标交互。

```razor
<Rate Disabled DefaultValue="2" />
```

## 6. 清除

关键差异：支持允许或者禁用清除。

```razor
<Rate AllowClear="true" DefaultValue="2" />
<span class="ant-rate-text">AllowClear: true</span>
<br />
<Rate AllowClear="false" DefaultValue="3"></Rate>
<span class="ant-rate-text">AllowClear: false</span>
```
