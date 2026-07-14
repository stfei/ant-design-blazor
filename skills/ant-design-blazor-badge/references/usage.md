# Ant Design Blazor Badge 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 状态点 | 用于表示状态的小圆点。 |
| 讨嫌的小红点 | 没有具体的数字。 |
| 缎带 | 使用缎带型的徽标。 |
| 封顶数字 | 超过 `overflowCount` 的会显示为 `${overflowCount}+`，默认的 `overflowCount` 为 `99`。 |
| 多彩徽标 | 我们添加了多种预设色彩的徽标样式，用作不同场景使用。如果预设值不能满足你的需求，可以设置为具体的色值。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Badge Count="5">
    <Button>待办事项</Button>
</Badge>

<Badge Status="BadgeStatus.Success" Text="服务正常" />
```

## 2. 状态点

关键差异：用于表示状态的小圆点。

```razor
<div>
    <Badge Status="BadgeStatus.Success" />
    <Badge Status="BadgeStatus.Error" />
    <Badge Status="BadgeStatus.Default" />
    <Badge Status="BadgeStatus.Processing" />
    <Badge Status="BadgeStatus.Warning" />
    <br />
    <Badge Status="BadgeStatus.Success" Text="Success" />
    <br />
    <Badge Status="BadgeStatus.Error" Text="Error" />
    <br />
    <Badge Status="BadgeStatus.Default" Text="Default" />
    <br />
    <Badge Status="BadgeStatus.Processing" Text="Processing" />
    <br />
    <Badge Status="BadgeStatus.Warning" Text="Warning" />
</div>
```

## 3. 讨嫌的小红点

关键差异：没有具体的数字。

```razor
<div>
    <Badge Dot>
        <Icon Type="@IconType.Outline.Notification"/>
    </Badge>
    <Badge Count="0" Dot>
        <Icon Type="@IconType.Outline.Notification" />
    </Badge>
    <Badge Dot>
        <a href="#">Link something</a>
    </Badge>
    <Badge Dot Status="BadgeStatus.Warning">
        <a href="#" class="head-example" />
    </Badge>
</div>
```

## 4. 缎带

关键差异：使用缎带型的徽标。

```razor
<BadgeRibbon Text="Hippies">
    <Card Title="Pushes open the window" Size="CardSize.Small">
        and raises the spyglass.
    </Card>
</BadgeRibbon>
<br />
<BadgeRibbon Text="Hippies" Color="BadgeColor.Pink">
    <Card Title="Pushes open the window" Size="CardSize.Small">
        and raises the spyglass.
    </Card>
</BadgeRibbon>
<br />
<BadgeRibbon Text="Hippies" Color="BadgeColor.Red">
    <Card Title="Pushes open the window" Size="CardSize.Small">
        and raises the spyglass.
    </Card>
</BadgeRibbon>
<br />
<BadgeRibbon Color="@("#832")">
    <TextTemplate>
        <Icon Type="@IconType.Outline.Windows" /> Hippies
    </TextTemplate>
    <ChildContent>
        <Card Title="Pushes open the window" Size="CardSize.Small">
            and raises the spyglass.
        </Card>
    </ChildContent>
</BadgeRibbon>
```

## 5. 封顶数字

关键差异：超过 `overflowCount` 的会显示为 `${overflowCount}+`，默认的 `overflowCount` 为 `99`。

```razor
<div>
    <Badge Count="99">
        <a href="#" class="head-example" />
    </Badge>
    <Badge Count="100">
        <a href="#" class="head-example" />
    </Badge>
    <Badge Count="99" OverflowCount="10">
        <a href="#" class="head-example" />
    </Badge>
    <Badge Count="1000" OverflowCount="999">
        <a href="#" class="head-example" />
    </Badge>
</div>
```

## 6. 多彩徽标

关键差异：我们添加了多种预设色彩的徽标样式，用作不同场景使用。如果预设值不能满足你的需求，可以设置为具体的色值。

```razor
@using System

<div>
    <Divider Orientation="DividerOrientation.Left">Presets</Divider>
    <div>
        @foreach (var color in Enum.GetValues(typeof(BadgeColor)))
        {
            <div key="@color">
                <Badge Color="@((BadgeColor)color)" Text="@(Enum.GetName(typeof(BadgeColor), color))" />
            </div>
        }
    </div>
    <Divider Orientation="DividerOrientation.Left">Custom</Divider>
    <div>
        <Badge Color="@("#f50")" Text="#f50" />
        <br />
        <Badge Color="@("#2db7f5")" Text="#2db7f5" />
        <br />
        <Badge Color="@("#87d068")" Text="#87d068" />
        <br />
        <Badge Color="@("#108ee9")" Text="#108ee9" />
        <br />
        <Badge Color="@("HotPink")" Text="HotPink" />
        <br />
        <Badge Color="@("DarkRed")" Text="DarkRed" />
        <br />
        <Badge Color="@("rgb(143, 201, 146)")" Text="rgb(143, 201, 146)" />
        <br />
        <Badge Color="@("rgb(105, 58, 236)")" Text="rgb(105, 58, 236)" />
    </div>
</div>

<style>
    .ant-tag {
        margin-bottom: 8px;
    }
</style>
```
