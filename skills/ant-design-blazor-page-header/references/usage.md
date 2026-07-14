# Ant Design Blazor PageHeader 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 多种形态的 PageHeader | 使用操作区，并自定义子节点，适合使用在需要展示一些复杂的信息，帮助用户快速了解这个页面的信息和操作。 |
| 组合示例 | 使用了 PageHeader 提供的所有能力。 |
| 白底模式 | 默认 PageHeader 是透明底色的。在某些情况下，PageHeader 需要自己的背景颜色。 |
| 带面包屑页头 | 带面包屑页头，适合层级比较深的页面，让用户可以快速导航。 |
| 响应式 | 在不同大小的屏幕下，应该有不同的表现 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<PageHeader Title="订单详情"
            Subtitle="SO-1001"
            OnBack="GoBack">
    <PageHeaderTags><Tag Color="TagColor.Blue">处理中</Tag></PageHeaderTags>
</PageHeader>

@code {
    private void GoBack() { }
}
```

## 2. 多种形态的 PageHeader

关键差异：使用操作区，并自定义子节点，适合使用在需要展示一些复杂的信息，帮助用户快速了解这个页面的信息和操作。

```razor
<PageHeader BackIcon="true" Class="site-page-header">
    <PageHeaderTitle>Title</PageHeaderTitle>
    <PageHeaderSubtitle>This is a subtitle</PageHeaderSubtitle>
    <PageHeaderExtra>
        <Button>Operation</Button>
        <Button>Operation</Button>
        <Button Type="ButtonType.Primary">Primary</Button>
    </PageHeaderExtra>
    <PageHeaderContent>
        <Descriptions Size="DescriptionsSize.Small" Column="3">
            <DescriptionsItem Title="Created" Span="1">Lili Qu</DescriptionsItem>
            <DescriptionsItem Title="Association" Span="1"><a>421421</a></DescriptionsItem>
            <DescriptionsItem Title="Creation Time" Span="1">2017-01-10</DescriptionsItem>
            <DescriptionsItem Title="Effective Time" Span="1">2017-10-10</DescriptionsItem>
            <DescriptionsItem Title="Remarks" Span="2">
                Gonghu Road, Xihu District, Hangzhou, Zhejiang, China
            </DescriptionsItem>
        </Descriptions>
    </PageHeaderContent>
</PageHeader>
<br />
<PageHeader BackIcon="true">
    <PageHeaderTitle>Title</PageHeaderTitle>
    <PageHeaderSubtitle>This is a subtitle</PageHeaderSubtitle>
    <PageHeaderTags>
        <Tag Color="TagColor.Blue">Running</Tag>
    </PageHeaderTags>
    <PageHeaderExtra>
        <Button>Operation</Button>
        <Button>Operation</Button>
        <Button Type="ButtonType.Primary">Primary</Button>
    </PageHeaderExtra>
    <PageHeaderContent>
        <Row>
            <Statistic Title="Status" Value=@("Pending") />
            <Statistic Title="Price" Value="568.08" Prefix="$" Style="margin: 0 32px" />
            <Statistic Title="Balance" Value="3345.08" Prefix="$" />
        </Row>
    </PageHeaderContent>
</PageHeader>
```

## 3. 组合示例

关键差异：使用了 PageHeader 提供的所有能力。

```razor
<PageHeader Class="site-page-header">
    <PageHeaderBreadcrumb>
        <Breadcrumb>
            <BreadcrumbItem>项目</BreadcrumbItem>
            <BreadcrumbItem>详情</BreadcrumbItem>
        </Breadcrumb>
    </PageHeaderBreadcrumb>
    <PageHeaderAvatar>
        <Avatar Icon="@IconType.Outline.User" />
    </PageHeaderAvatar>
    <PageHeaderTitle>项目标题</PageHeaderTitle>
    <PageHeaderSubtitle>项目说明</PageHeaderSubtitle>
    <PageHeaderTags>
        <Tag Color="TagColor.Blue">进行中</Tag>
    </PageHeaderTags>
    <PageHeaderExtra>
        <Button>取消</Button>
        <Button Type="ButtonType.Primary">保存</Button>
        <Dropdown Placement="Placement.BottomRight">
            <Overlay>
                <Menu>
                    <MenuItem>复制</MenuItem>
                    <MenuItem Danger>删除</MenuItem>
                </Menu>
            </Overlay>
            <ChildContent>
                <Button><Icon Type="@IconType.Outline.Ellipsis" /></Button>
            </ChildContent>
        </Dropdown>
    </PageHeaderExtra>
    <PageHeaderContent>
        <Paragraph>这里放页面摘要、统计信息或其他补充内容。</Paragraph>
    </PageHeaderContent>
</PageHeader>
```

## 4. 白底模式

关键差异：默认 PageHeader 是透明底色的。在某些情况下，PageHeader 需要自己的背景颜色。

```razor
<div class="site-page-header-ghost-wrapper">
    <PageHeader BackIcon="true" Ghost="false">
        <PageHeaderTitle>Title</PageHeaderTitle>
        <PageHeaderSubtitle>This is a subtitle</PageHeaderSubtitle>
        <PageHeaderExtra>
            <Button>Operation</Button>
            <Button>Operation</Button>
            <Button Type="ButtonType.Primary">Primary</Button>
        </PageHeaderExtra>
        <PageHeaderContent>
            <Descriptions Size="DescriptionsSize.Small" Column="3">
                <DescriptionsItem Title="Created" Span="1">Lili Qu</DescriptionsItem>
                <DescriptionsItem Title="Association" Span="1"><a>421421</a></DescriptionsItem>
                <DescriptionsItem Title="Creation Time" Span="1">2017-01-10</DescriptionsItem>
                <DescriptionsItem Title="Effective Time" Span="1">2017-10-10</DescriptionsItem>
                <DescriptionsItem Title="Remarks" Span="2">
                    Gonghu Road, Xihu District, Hangzhou, Zhejiang, China
                </DescriptionsItem>
            </Descriptions>
        </PageHeaderContent>
    </PageHeader>
</div>
```

## 5. 带面包屑页头

关键差异：带面包屑页头，适合层级比较深的页面，让用户可以快速导航。

```razor
<PageHeader Class="site-page-header" Title="Title" Subtitle="This is a subtitle">
    <PageHeaderBreadcrumb>
        <Breadcrumb>
            <BreadcrumbItem>First-level Menu</BreadcrumbItem>
            <BreadcrumbItem>
                <a>Second-level Menu</a>
            </BreadcrumbItem>
            <BreadcrumbItem>Third-level Menu</BreadcrumbItem>
        </Breadcrumb>
    </PageHeaderBreadcrumb>
</PageHeader>
```

## 6. 响应式

关键差异：在不同大小的屏幕下，应该有不同的表现

```razor
<PageHeader BackIcon="true" Class="site-page-header">
    <PageHeaderTitle>Title</PageHeaderTitle>
    <PageHeaderSubtitle>This is a subtitle</PageHeaderSubtitle>
    <PageHeaderExtra>
        <Button>Operation</Button>
        <Button>Operation</Button>
        <Button Type="ButtonType.Primary">Primary</Button>
    </PageHeaderExtra>
    <PageHeaderContent>
        <div class="pageheader-content">
            <div class="pageheader-main">
                <Descriptions Size="DescriptionsSize.Small" Column="2">
                    <DescriptionsItem Title="Created" Span="1">Lili Qu</DescriptionsItem>
                    <DescriptionsItem Title="Association" Span="1"><a>421421</a></DescriptionsItem>
                    <DescriptionsItem Title="Creation Time" Span="1">2017-01-10</DescriptionsItem>
                    <DescriptionsItem Title="Effective Time" Span="1">2017-10-10</DescriptionsItem>
                    <DescriptionsItem Title="Remarks" Span="2">
                        Gonghu Road, Xihu District, Hangzhou, Zhejiang, China
                    </DescriptionsItem>
                </Descriptions>
            </div>
            <div class="pageheader-extra">
                <div>
                    <Statistic Title="Status" Value="@("Pending")" />
                    <Statistic Title="Price" Value="568.08" Prefix="$" Style="margin: 0 32px" />
                </div>
            </div>
        </div>
    </PageHeaderContent>
    <PageHeaderFooter>
        <Tabs DefaultActiveKey="1">
            <TabPane Key="1">
                <TabTemplate>Details</TabTemplate>
            </TabPane>
            <TabPane Key="2">
                <TabTemplate>Rule</TabTemplate>
            </TabPane>
        </Tabs>
    </PageHeaderFooter>
</PageHeader>

<style>
    .pageheader-content {
        display: flex;
    }

    .pageheader-extra div {
        display: flex;
        width: max-content;
        justify-content: flex-end;
    }

    @@media (max-width: 576px) {
        .pageheader-content {
            display: block;
        }

        .pageheader-main {
            width: 100%;
            margin-bottom: 12px;
        }

        .pageheader-extra {
            width: 100%;
            margin-left: 0;
            text-align: left;
        }
    }
</style>
```
