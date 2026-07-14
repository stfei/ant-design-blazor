# Ant Design Blazor Progress 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 进度条 | 标准的进度条。 |
| 进度圈 | 圈形的进度。 |
| 动态展示 | 会动的进度条才是好进度条。 |
| 步骤进度条 | 带步骤的进度条。 |
| 自定义进度条渐变色 | `linear-gradient` 的封装。推荐只传两种颜色。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Progress Percent="65" Status="ProgressStatus.Active" />
<Progress Type="ProgressType.Circle" Percent="80" />
```

## 2. 进度条

关键差异：标准的进度条。

```razor
<div>
    <Progress Percent="30" />
    <Progress Percent="50" Status="ProgressStatus.Active" />
    <Progress Percent="70" Status="ProgressStatus.Exception" />
    <Progress Percent="100" />
    <Progress Percent="50" ShowInfo="false" />
</div>
```

## 3. 进度圈

关键差异：圈形的进度。

```razor
<style>
    .ant-progress-circle-wrap,
    .ant-progress-line-wrap {
        margin-right: 8px;
        margin-bottom: 5px;
    }
</style>

<div>
    <Progress Type="ProgressType.Circle" Percent=75 />
    <Progress Type="ProgressType.Circle" Percent=70 Status="ProgressStatus.Exception" />
    <Progress Type="ProgressType.Circle" Percent=100 />
</div>
```

## 4. 动态展示

关键差异：会动的进度条才是好进度条。

```razor
<div>
    <Progress Percent=_percent/>
    <ButtonGroup>
        <Button OnClick=Decline Icon=Minus />
        <Button OnClick=Increase Icon=Plus />
    </ButtonGroup>
</div>

@code{
    private int _percent = 0;

    private void Decline()
    {
        _percent -= 10;
        if (_percent < 0)
        {
            _percent = 0;
        }
    }

    private void Increase()
    {
        _percent += 10;
        if (_percent > 100)
        {
            _percent = 100;
        }
    }
}
```

## 5. 步骤进度条

关键差异：带步骤的进度条。

```razor
<div>
    <Progress Percent=50 Steps=3 StrokeColor=_color />
    <br />
    <Progress Percent=30 Steps=5 StrokeColor=_color />
    <br />
    <Progress Percent=100 Steps=5 Size=ProgressSize.Small StrokeColor=_color />
</div>

@code{
    private string _color = "#1890ff";
}
```

## 6. 自定义进度条渐变色

关键差异：`linear-gradient` 的封装。推荐只传两种颜色。

```razor
<div>
    <Progress StrokeColor=_gradients Percent=99.9 />
    <Progress StrokeColor=_gradients Percent=99.9 Status=ProgressStatus.Active />
    <Progress Type=ProgressType.Circle StrokeColor=_gradients Percent=90 />
    <Progress Type=ProgressType.Circle StrokeColor=_gradients Percent=100 />
</div>

@code{
    private Dictionary<string, string> _gradients = new()
    {
        { "0%", "#108ee9" },
        { "100%", "#87d068" }
    };
}
```
