# Ant Design Blazor Empty 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 自定义 | 自定义图片链接、图片大小、描述、附属内容。 |
| 无描述 | 无描述展示。 |
| 选择图片 | 可以通过设置 `Simple` 属性为 `true`，选择另一种风格的图片，这里实现与 Ant Design 不同。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Empty Description="@("暂无订单")">
    <Button Type="ButtonType.Primary">新建订单</Button>
</Empty>
```

## 2. 自定义

关键差异：自定义图片链接、图片大小、描述、附属内容。

```razor
<Empty Image="https://gw.alipayobjects.com/zos/antfincdn/ZHrcdLPrvN/empty.svg"
          ImageStyle='@("height: 60px")'
        >
    <DescriptionTemplate>
        <span>Customize <a>Description</a></span>
    </DescriptionTemplate>
    <ChildContent>
        <Button Type="ButtonType.Primary">Create Now</Button>
    </ChildContent>

</Empty>
```

## 3. 无描述

关键差异：无描述展示。

```razor
<Empty Description="false" />
```

## 4. 选择图片

关键差异：可以通过设置 `Simple` 属性为 `true`，选择另一种风格的图片，这里实现与 Ant Design 不同。

```razor
<Empty Simple/>
```

