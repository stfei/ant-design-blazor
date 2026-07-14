# Ant Design Blazor Timeline 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 交替展现 | 内容在时间轴两侧轮流出现。 |
| 自定义时间轴点 | 可以设置为图标或其他自定义元素。 |
| 标签 | 使用 `label` 标签单独展示时间。 |
| 最后一个及排序 | 当任务状态正在发生，还在记录过程中，可用幽灵节点来表示当前的时间节点，当 `pending` 为真值时展示幽灵节点，如果 pending 是 `React 元素`可用于定制该节点内容，同时 pendingDot 将可以用于定制其轴点。`reverse` 属性用于控制节点排序，为 false 时按正序排列，为 true 时按倒序排列。 |
| 右侧时间轴点 | 时间轴点可以在内容的右边。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Timeline>
    <TimelineItem Color="TimelineDotColor.Green">创建订单</TimelineItem>
    <TimelineItem Color="TimelineDotColor.Blue">完成付款</TimelineItem>
    <TimelineItem>等待发货</TimelineItem>
</Timeline>
```

## 2. 交替展现

关键差异：内容在时间轴两侧轮流出现。

```razor
<div>
<Timeline Mode="TimelineMode.Alternate"  >
    <TimelineItem>Create a services site 2015-09-01</TimelineItem>
    <TimelineItem Color="TimelineDotColor.Green">Solve initial network problems 2015-09-01</TimelineItem>
    <TimelineItem Dot="dotTemplate">
        Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa
        quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.
    </TimelineItem>
    <TimelineItem Color="TimelineDotColor.Red">Network problems being solved 2015-09-01</TimelineItem>
    <TimelineItem>Create a services site 2015-09-01</TimelineItem>
    <TimelineItem Dot="dotTemplate">Technical testing 2015-09-01</TimelineItem>
</Timeline>
</div>

@code {
    RenderFragment dotTemplate =
    @<Template>
        <Icon Type="@IconType.Outline.ClockCircle" Style="font-size: 16px;" />
    </Template>;
 }
```

## 3. 自定义时间轴点

关键差异：可以设置为图标或其他自定义元素。

```razor
<div>
    <Timeline>
        <TimelineItem>Create a services site 2015-09-01</TimelineItem>
        <TimelineItem>Solve initial network problems 2015-09-01</TimelineItem>
        <TimelineItem Dot="dotTemplate" Color="TimelineDotColor.Red">
        Technical testing 2015-09-01
        </TimelineItem>
       <TimelineItem>Network problems being solved 2015-09-01</TimelineItem>
    </Timeline>
</div>

@code {
    RenderFragment dotTemplate =
        @<Template>
            <Icon Type="@IconType.Outline.ClockCircle" Style="font-size: 16px;" />
        </Template>;
}
```

## 4. 标签

关键差异：使用 `label` 标签单独展示时间。

```razor
<div>
    <RadioGroup @bind-Value="@mode" Style="margin-bottom:20px">
        <Radio Value="TimelineMode.Left">Left</Radio>
        <Radio Value="TimelineMode.Right">Right</Radio>
        <Radio Value="TimelineMode.Alternate">Alternate</Radio>
    </RadioGroup>
    <Timeline Mode="@mode">
        <TimelineItem Label="2015-09-01">Create a services</TimelineItem>
        <TimelineItem Label="2015-09-01 09:12:11">Solve initial network problems</TimelineItem>
        <TimelineItem>Technical testing</TimelineItem>
        <TimelineItem Label="2015-09-01 09:12:11">Network problems being solved</TimelineItem>
    </Timeline>
</div>

@code{
    private TimelineMode mode = TimelineMode.Left;
}
```

## 5. 最后一个及排序

关键差异：当任务状态正在发生，还在记录过程中，可用幽灵节点来表示当前的时间节点，当 `pending` 为真值时展示幽灵节点，如果 pending 是 `React 元素`可用于定制该节点内容，同时 pendingDot 将可以用于定制其轴点。`reverse` 属性用于控制节点排序，为 false 时按正序排列，为 true 时按倒序排列。

```razor
<div>
    <Timeline Pending="pending" Reverse=@bReverse>
        <TimelineItem>Create a services site 2015-09-01</TimelineItem>
        <TimelineItem>Solve initial network problems 2015-09-01</TimelineItem>
        <TimelineItem>Technical testing 2015-09-01</TimelineItem>
    </Timeline>
    <Button Type="ButtonType.Primary" Style="margin-top: 16px " OnClick="HandleClick">
        Toggle Reverse
    </Button>
</div>
@code{
    private bool bReverse { get; set; } = false;
    RenderFragment pending = @<Template>
                                                        Recording...
                                                    </Template>;
    void HandleClick()
    {
     bReverse = !bReverse;
    }
}
```

## 6. 右侧时间轴点

关键差异：时间轴点可以在内容的右边。

```razor
<div>
    <Timeline Mode="TimelineMode.Right">
        <TimelineItem>Create a services site 2015-09-01</TimelineItem>
        <TimelineItem>Solve initial network problems 2015-09-01</TimelineItem>
        <TimelineItem Dot="dotTemplate" Color="TimelineDotColor.Red">
            Technical testing 2015-09-01
        </TimelineItem>
        <TimelineItem Color="TimelineDotColor.Red">Network problems being solved 2015-09-01</TimelineItem>
    </Timeline>,
</div>

@code{
    RenderFragment dotTemplate =
    @<Template>
        <Icon Type="@IconType.Outline.ClockCircle" Style="font-size: 16px;" />
    </Template>;
}
```
