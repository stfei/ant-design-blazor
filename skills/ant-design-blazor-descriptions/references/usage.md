# Ant Design Blazor Descriptions 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 带边框的 | 带边框和背景颜色列表。 |
| 垂直 | Vertical usage. |
| 响应式 | 通过响应式的配置可以实现在小屏幕设备上的完美呈现。 |
| 自定义样式 | 可以通过 `LabelStyle` 和 `ContentStyle` 设置每个 Item 的样式。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Descriptions Title="用户信息" Bordered>
    <DescriptionsItem Title="姓名">张三</DescriptionsItem>
    <DescriptionsItem Title="电话">13800000000</DescriptionsItem>
    <DescriptionsItem Title="状态">已启用</DescriptionsItem>
</Descriptions>
```

## 2. 带边框的

关键差异：带边框和背景颜色列表。

```razor
<Descriptions Title="User Info" Bordered>
    <DescriptionsItem Title="Product">Cloud Database</DescriptionsItem>
    <DescriptionsItem Title="Billing Mode">Prepaid</DescriptionsItem>
    <DescriptionsItem Title="Automatic Renewal">YES</DescriptionsItem>
    <DescriptionsItem Title="Order Time">
        2018-04-24 18:00:00
    </DescriptionsItem>
    <DescriptionsItem Title="Usage Time" Span="2">
        2018-04-24 18:00:00 To 2019-04-24 18:00:00
    </DescriptionsItem>
    <DescriptionsItem Title="Status" Span="3">
        <Badge Status="BadgeStatus.Processing" Text="Running"></Badge>
    </DescriptionsItem>
    <DescriptionsItem Title="Negotiated Amount">$80.00</DescriptionsItem>
    <DescriptionsItem Title="Discount">$20.00</DescriptionsItem>
    <DescriptionsItem Title="Official Receipts">$60.00</DescriptionsItem>
    <DescriptionsItem Title="Config Info">
        Data disk type: MongoDB
        <br />
        Database version: 3.4
        <br />
        Package: dds.mongo.mid
        <br />
        Storage space: 10 GB
        <br />
        Replication_factor:3
        <br />
        Region: East China 1<br />
    </DescriptionsItem>
</Descriptions>
```

## 3. 垂直

关键差异：Vertical usage.

```razor
<Descriptions Title="User Info" Layout="DescriptionsLayout.Vertical">
    <DescriptionsItem Title="UserName">Zhou Maomao</DescriptionsItem>
    <DescriptionsItem Title="Telephone">18100000000</DescriptionsItem>
    <DescriptionsItem Title="Live">Hangzhou, Zhejiang</DescriptionsItem>
    <DescriptionsItem Title="Address" Span="2">
        No. 18, Wantang Road, Xihu District, Hangzhou, Zhejiang, China
    </DescriptionsItem>
    <DescriptionsItem Title="Remark">Empty</DescriptionsItem>
</Descriptions>
```

## 4. 响应式

关键差异：通过响应式的配置可以实现在小屏幕设备上的完美呈现。

```razor
<Descriptions Title="Responsive Descriptions" Bordered Column="@column">
    <DescriptionsItem Title="Product">
        Cloud Database
    </DescriptionsItem>
    <DescriptionsItem Title="Billing">Prepaid</DescriptionsItem>
    <DescriptionsItem Title="time">18:00:00</DescriptionsItem>
    <DescriptionsItem Title="Amount">$80.00</DescriptionsItem>
    <DescriptionsItem Title="Discount">$20.00</DescriptionsItem>
    <DescriptionsItem Title="Official">$60.00</DescriptionsItem>
    <DescriptionsItem Title="Config Info">
        Data disk type: MongoDB
        <br />
        Database version: 3.4
        <br />
        Package: dds.mongo.mid
        <br />
        Storage space: 10 GB
        <br />
        Replication_factor:3
        <br />
        Region: East China 1
        <br />
    </DescriptionsItem>
</Descriptions>

@code{

    private Dictionary<string, int> column = new Dictionary<string, int> {
            { "Xxl", 3 },
            { "Xl", 3 },
            { "Lg", 2 },
            { "Md", 2 },
            { "Sm", 1 },
            { "Xs", 1 }
        };

}
```

## 5. 自定义样式

关键差异：可以通过 `LabelStyle` 和 `ContentStyle` 设置每个 Item 的样式。

```razor
<Descriptions Title="User Info">
    <DescriptionsItem Title="UserName" LabelStyle="color: red;" ContentStyle="font-weight: bold;">Zhou Maomao</DescriptionsItem>
    <DescriptionsItem Title="Address" ContentStyle="font-weight: bold;">
        No. 18, Wantang Road, Xihu District, Hangzhou, Zhejiang, China
    </DescriptionsItem>
</Descriptions>
```
