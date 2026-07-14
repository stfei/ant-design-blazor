# Ant Design Blazor Spin 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 卡片加载中 | 可以直接把内容内嵌到 `Spin` 中，将现有容器变为加载状态。 |
| 自定义指示符 | 使用自定义指示符。 |
| 延迟 | 延迟显示 loading 效果。当 spinning 状态在 `delay` 时间内结束，则不显示 loading 状态。 |
| 自定义描述文案 | 自定义描述文案。 |
| 各种大小 | 小的用于文本加载，默认用于卡片容器级加载，大的用于**页面级**加载。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Spin Spinning="_loading" Tip="加载中...">
    <div>需要被遮罩的内容</div>
</Spin>

@code {
    private bool _loading = true;
}
```

## 2. 卡片加载中

关键差异：可以直接把内容内嵌到 `Spin` 中，将现有容器变为加载状态。

```razor
<div>
    <Spin Spinning="loading">
        <Alert
            Message="Alert message title"
            Description="Further details about the context of this alert."
            Type="AlertType.Info" />
    </Spin>
    <div style="margin-top: 16px">
        Loading state：
        <Switch Checked="loading" OnChange="toggle" />
    </div>
</div>

@code {
    bool loading = false;

    void toggle(bool value) => loading = value;
}
```

## 3. 自定义指示符

关键差异：使用自定义指示符。

```razor
<Spin Indicator="antIcon" />
@code{
    RenderFragment antIcon = @<Icon Type="@IconType.Outline.Loading" Style="font-size: 24px" Spin />;
}
```

## 4. 延迟

关键差异：延迟显示 loading 效果。当 spinning 状态在 `delay` 时间内结束，则不显示 loading 状态。

```razor
<div>
    <Spin Spinning="loading" Delay="500">
        <Alert
            Message="Alert message title"
            Description="Further details about the context of this alert."
            Type="AlertType.Info" />
    </Spin>
    <div style="margin-top: 16px">
        Loading state：
        <Switch Checked="loading" OnChange="toggle" />
    </div>
</div>

@code {
    bool loading = false;

    void toggle(bool value) => loading = value;
}
```

## 5. 自定义描述文案

关键差异：自定义描述文案。

```razor
<Spin Tip="Loading...">
    <Alert
        Message="Alert message title"
        Description="Further details about the context of this alert."
        Type="AlertType.Info" />
</Spin>
```

## 6. 各种大小

关键差异：小的用于文本加载，默认用于卡片容器级加载，大的用于**页面级**加载。

```razor
<div>
    <Spin Size="SpinSize.Small" />
    <Spin />
    <Spin Size="SpinSize.Large" />
</div>
```
