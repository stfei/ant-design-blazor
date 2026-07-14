# Ant Design Blazor Divider 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 水平分割线 | 默认为水平分割线，可在中间加入文字。 |
| 垂直分割线 | 使用 `Type="DividerType.Vertical"` 设置为行内的垂直分割线。 |
| 带文字的分割线 | 分割线中带有文字，可以用 orientation 指定文字位置。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<p>第一部分</p>
<Divider Orientation="DividerOrientation.Left">详细信息</Divider>
<p>第二部分</p>
```

## 2. 水平分割线

关键差异：默认为水平分割线，可在中间加入文字。

```razor
<div>
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed nonne merninisti licere mihi ista
        probare, quae sunt a te dicta? Refert tamen, quo modo.
    </p>
    <Divider />
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed nonne merninisti licere mihi ista
        probare, quae sunt a te dicta? Refert tamen, quo modo.
    </p>
    <Divider Dashed />
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed nonne merninisti licere mihi ista
        probare, quae sunt a te dicta? Refert tamen, quo modo.
    </p>
</div>
```

## 3. 垂直分割线

关键差异：使用 `Type="DividerType.Vertical"` 设置为行内的垂直分割线。

```razor
<div>
    Text
    <Divider Type="DividerType.Vertical" />
    <a href="#">Link</a>
    <Divider Type="DividerType.Vertical" />
    <a href="#">Link</a>
</div>
```

## 4. 带文字的分割线

关键差异：分割线中带有文字，可以用 orientation 指定文字位置。

```razor
<div>
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed nonne merninisti licere mihi ista
        probare, quae sunt a te dicta? Refert tamen, quo modo.
    </p>
    <Divider>Text</Divider>
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed nonne merninisti licere mihi ista
        probare, quae sunt a te dicta? Refert tamen, quo modo.
    </p>
    <Divider Orientation="DividerOrientation.Left" Style="font-weight:bold">Left Text</Divider>
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed nonne merninisti licere mihi ista
        probare, quae sunt a te dicta? Refert tamen, quo modo.
    </p>
    <Divider Orientation="DividerOrientation.Right" Style="font-weight:bold">Right Text</Divider>
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed nonne merninisti licere mihi ista
        probare, quae sunt a te dicta? Refert tamen, quo modo.
    </p>
</div>
```
