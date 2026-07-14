# Ant Design Blazor Skeleton 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 动画效果 | 显示动画效果。 |
| 包含子组件 | 加载占位图包含子组件。 |
| 复杂的组合 | 更复杂的组合。 |
| 骨架按钮、头像和输入框。 | 骨架按钮、头像和输入框。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
@if (_loading)
{
    <Skeleton Active ParagraphRows="3" />
}
else
{
    <p>真实内容</p>
}

@code {
    private bool _loading = true;
}
```

## 2. 动画效果

关键差异：显示动画效果。

```razor
<Skeleton Active="true"></Skeleton>
```

## 3. 包含子组件

关键差异：加载占位图包含子组件。

```razor
<div class="article">
    <Skeleton Loading="@_loading">
        <h4>Ant Design, a design language</h4>
        <p>
            We supply a series of design principles, practical patterns and high quality design resources (Sketch and Axure), to help people
            create their product prototypes beautifully and efficiently.
        </p>
    </Skeleton>
    <Button @onclick="showSkeleton" Disabled="@_loading">
        Show Skeleton
    </Button>
</div>

<style>
    .article h4 {
        margin-bottom: 16px;
    }

    .article button {
        margin-top: 16px;
    }
</style>

@code{
    private bool _loading = false;

    private async Task showSkeleton()
    {
        this._loading = true;
        await Task.Delay(3000);
        this._loading = false;
    }
}
```

## 4. 复杂的组合

关键差异：更复杂的组合。

```razor
<Skeleton Avatar="true" ParagraphRows="4"></Skeleton>
```

## 5. 骨架按钮、头像和输入框。

关键差异：骨架按钮、头像和输入框。

```razor
<Row Align="RowAlign.Middle" Gutter="8">
    <Col Span="5">
    ButtonActive:
    <Switch @bind-Value="_buttonActive"></Switch>
    </Col>
    <Col Span="9">
    ButtonSize:
    <RadioGroup @bind-Value="_buttonSize">
        <Radio Value="SkeletonElementSize.Default">Default</Radio>
        <Radio Value="SkeletonElementSize.Large">Large</Radio>
        <Radio Value="SkeletonElementSize.Small">Small</Radio>
    </RadioGroup>
    </Col>
    <Col Span="9">
    ButtonShape:
    <RadioGroup @bind-Value="_buttonShape">
        <Radio Value="SkeletonElementShape.Default">Default</Radio>
        <Radio Value="SkeletonElementShape.Circle">Circle</Radio>
        <Radio Value="SkeletonElementShape.Round">Round</Radio>
    </RadioGroup>
    </Col>
</Row>
<br />
<SkeletonElement Type="SkeletonElementType.Button" Active="_buttonActive" Size="_buttonSize" Shape="_buttonShape"></SkeletonElement>
<br />
<br />
<Row Align="RowAlign.Middle" Gutter="8">
    <Col Span="5">
    AvatarActive:
    <Switch @bind-Value="_avatarActive"></Switch>
    </Col>
    <Col Span="9">
    AvatarSize:
    <RadioGroup @bind-Value="_avatarSize">
        <Radio Value="SkeletonElementSize.Default">Default</Radio>
        <Radio Value="SkeletonElementSize.Large">Large</Radio>
        <Radio Value="SkeletonElementSize.Small">Small</Radio>
    </RadioGroup>
    </Col>
    <Col Span="9">
    AvatarShape:
    <RadioGroup @bind-Value="_avatarShape">
        <Radio Value="AvatarShape.Circle">Circle</Radio>
        <Radio Value="AvatarShape.Square">Square</Radio>
    </RadioGroup>
    </Col>
</Row>
<br />
<SkeletonElement Type="SkeletonElementType.Avatar" Active="_avatarActive" Size="_avatarSize" Shape="_avatarShape"></SkeletonElement>
<br />
<br />
<Row Align="RowAlign.Middle" Gutter="8">
    <Col Span="5">
    InputActive:
    <Switch @bind-Value="_inputActive"></Switch>
    </Col>
    <Col Span="9">
    InputSize:
    <RadioGroup @bind-Value="_inputSize">
        <Radio Value="SkeletonElementSize.Default">Default</Radio>
        <Radio Value="SkeletonElementSize.Large">Large</Radio>
        <Radio Value="SkeletonElementSize.Small">Small</Radio>
    </RadioGroup>
    </Col>
</Row>
<br />
<SkeletonElement Type="SkeletonElementType.Input" Active="_inputActive" Size="_inputSize" style="width:300px"></SkeletonElement>

@code{
    bool _buttonActive = false;
    bool _avatarActive = false;
    bool _inputActive = false;
    SkeletonElementSize _buttonSize = SkeletonElementSize.Default;
    SkeletonElementSize _avatarSize = SkeletonElementSize.Default;
    SkeletonElementSize _inputSize = SkeletonElementSize.Default;
    SkeletonElementShape _buttonShape = SkeletonElementShape.Default;
    SkeletonElementShape _avatarShape = SkeletonElementShape.Circle;
}
```
