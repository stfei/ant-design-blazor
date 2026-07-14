# Ant Design Blazor Segmented 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 受控模式 | 受控的 Segmented。 |
| 设置图标 | 给 Segmented Item 设置 Icon。 |
| 自定义渲染 | 使用 ReactNode 自定义渲染每一个 Segmented Item。 |
| 不可用 | Segmented 不可用。 |
| 三种大小 | 我们为 `<Segmented />` 组件定义了三种尺寸（大、默认、小），高度分别为 `40px`、`32px` 和 `24px`。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Segmented TValue="string"
           Labels="@(new[] { "日", "周", "月" })"
           @bind-Value="_period" />

@code {
    private string _period = "日";
}
```

## 2. 受控模式

关键差异：受控的 Segmented。

```razor
<Segmented TValue="string" Labels=@(new[]{"Map", "Transit", "Satellite"}) Value=@_value OnChange=@setValue />

@code {
  string _value;

  void setValue(string value)
  {
    _value = value;
  }
}
```

## 3. 设置图标

关键差异：给 Segmented Item 设置 Icon。

```razor
<Segmented TValue="string">
  <SegmentedItem Label="List" Value=@("List") Icon="@IconType.Outline.Bars" />
  <SegmentedItem Label="Kanban" Value=@("Kanban") Icon="@IconType.Outline.Appstore" />
</Segmented>
```

## 4. 自定义渲染

关键差异：使用 ReactNode 自定义渲染每一个 Segmented Item。

```razor
<Segmented TValue="string">
  <SegmentedItem Value="@("user1")">
    <div style=" padding: 4px;">
      <Avatar Src="https://joeschmoe.io/api/v1/random" />
      <div>User 1</div>
    </div>
  </SegmentedItem>
  <SegmentedItem Value="@("user2")">
    <div style="padding: 4px;">
      <Avatar Style="background-color: #f56a00 ">K</Avatar>
      <div>User 2</div>
    </div>
  </SegmentedItem>
  <SegmentedItem Value="@("user3")">
    <div style="padding: 4px;">
      <Avatar style="background-color: #87d068;" Icon="user"></Avatar>
      <div>User 3</div>
    </div>
  </SegmentedItem>
</Segmented>

<br />

<Segmented TValue="string">
  <SegmentedItem Value=@("spring")>
    <div style="padding: 4px;">
      <div>Spring</div>
      <div>Jan-Mar</div>
    </div>
  </SegmentedItem>
  <SegmentedItem Value=@("summer")>
    <div style="padding: 4px;">
      <div>Summer</div>
      <div>Apr-Jun</div>
    </div>
  </SegmentedItem>
  <SegmentedItem Value=@("autumn")>
    <div style="padding: 4px;">
      <div>Autumn</div>
      <div>Jul-Sept</div>
    </div>
  </SegmentedItem>
  <SegmentedItem Value=@("winter")>
    <div style="padding: 4px;">
      <div>Winter</div>
      <div>Oct-Dec</div>
    </div>
  </SegmentedItem>
</Segmented>
```

## 5. 不可用

关键差异：Segmented 不可用。

```razor
<Segmented TValue="string" Labels="@(new []{"Map", "Transit", "Satellite"})" Disabled />
<br />
<Segmented TValue="string" Options="@options" />
<br />
<Segmented @bind-Value="_value" Disabled>
    <SegmentedItem Value="true" Label="True" />
    <SegmentedItem Value="false" Label="False" />
</Segmented>
<br />
<Segmented @bind-Value="_value" Disabled="_disabled">
    <SegmentedItem Value="true" Label="True" />
    <SegmentedItem Value="false" Label="False" />
</Segmented>
<br />
<Switch @bind-Checked="_disabled" CheckedChildren="Disabled" UnCheckedChildren="Enable"/>

@code {
    bool _disabled;
    bool _value;

    SegmentedOption<string>[] options = {
        new("Daily"),
        new("Weekly",  "Weekly", true),
        new("Monthly"),
        new("Quarterly",  "Quarterly",  true ),
        new("Yearly"),
    };
}
```

## 6. 三种大小

关键差异：我们为 `<Segmented />` 组件定义了三种尺寸（大、默认、小），高度分别为 `40px`、`32px` 和 `24px`。

```razor
<Segmented Size="SegmentedSize.Large" @bind-Value="value" Labels="@(new[]{"Daily", "Weekly", "Monthly", "Quarterly", "Yearly"})" />
<br />
<Segmented @bind-Value="value" Labels="@(new[]{"Daily", "Weekly", "Monthly", "Quarterly", "Yearly"})" />
<br />
<Segmented Size="SegmentedSize.Small" @bind-Value="value" Labels="@(new[]{"Daily", "Weekly", "Monthly", "Quarterly", "Yearly"})" />

@code {
  string value;
}
```
