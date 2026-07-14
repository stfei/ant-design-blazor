# Ant Design Blazor Layout 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 侧边布局 | 使用嵌套 `Layout` 与 `Sider` 构造侧栏加内容区，并通过 `Collapsible` 支持收起。 |
| 响应式布局 | Layout.Sider 支持响应式布局。 > 说明：配置 `breakpoint` 属性即生效，视窗宽度小于 `breakpoint` 时 Sider 缩小为 `collapsedWidth` 宽度，若将 `collapsedWidth` 设置为零，会出现特殊 trigger。 |
| 固定头部 | 一般用于固定顶部导航，方便页面切换。 |
| 自定义触发器 | 要使用自定义触发器，可以设置 `trigger={null}` 来隐藏默认设定。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Layout Style="min-height: 100vh">
    <Sider Width="220">侧边导航</Sider>
    <Layout>
        <Header>页头</Header>
        <Content>页面内容</Content>
        <Footer>页脚</Footer>
    </Layout>
</Layout>
```

## 2. 侧边布局

关键差异：使用嵌套 `Layout` 与 `Sider` 构造侧栏加内容区，并通过 `Collapsible` 支持收起。

```razor
<Layout Style="min-height: 100vh; ">
    <Sider Collapsible>
        <div class="logo" />
        <Menu Theme="MenuTheme.Dark" DefaultSelectedKeys=@(new[]{"1"}) Mode="MenuMode.Inline">
            <MenuItem Key="1">
                <Icon Type="@IconType.Outline.PieChart" />
                <span>Option 1</span>
            </MenuItem>
            <MenuItem Key="2">
                <Icon Type="@IconType.Outline.Desktop" />
                <span>Option 2</span>
            </MenuItem>
            @{
                RenderFragment sub1Title =
                    @<span>
                        <Icon Type="@IconType.Outline.User" />
                        <span>User</span>
                    </span>;
            }
            <SubMenu Key="sub1" TitleTemplate=sub1Title>
                <MenuItem Key="3">Tom</MenuItem>
                <MenuItem Key="4">Bill</MenuItem>
                <MenuItem Key="5">Alex</MenuItem>
            </SubMenu>
            @{
                RenderFragment sub2Title =
                    @<span>
                        <Icon Type="@IconType.Outline.Team" />
                        <span>Team</span>
                    </span>;
            }
            <SubMenu Key="sub2" TitleTemplate=sub2Title>
                <MenuItem Key="6">Team 1</MenuItem>
                <MenuItem Key="8">Team 2</MenuItem>
            </SubMenu>
            <MenuItem Key="9">
                <Icon Type="@IconType.Outline.File" />
            </MenuItem>
        </Menu>
    </Sider>
    <Layout Class="site-layout" >
        <Header Class="site-layout-background" Style="padding: 0;" ></Header>
        <Content Style="margin:0 16px;" >
            <Breadcrumb Style="margin:16px 0;">
                <BreadcrumbItem>User</BreadcrumbItem>
                <BreadcrumbItem>Bill</BreadcrumbItem>
            </Breadcrumb>
            <div class="site-layout-background" style="padding: 24px; min-height: 360px">
                Bill is a cat.
            </div>
        </Content>
        <Footer Style="text-align:center">Ant Design ©2018 Created by Ant UED</Footer>
    </Layout>
</Layout>


<style>
    #components-layout-demo-side .logo {
        height: 32px;
        background: rgba(255, 255, 255, 0.2);
        margin: 16px;
    }

    .site-layout .site-layout-background {
        background: #fff;
    }
</style>
```

## 3. 响应式布局

关键差异：Layout.Sider 支持响应式布局。 > 说明：配置 `breakpoint` 属性即生效，视窗宽度小于 `breakpoint` 时 Sider 缩小为 `collapsedWidth` 宽度，若将 `collapsedWidth` 设置为零，会出现特殊 trigger。

```razor
<Layout>
    <Sider Breakpoint="BreakpointType.Lg"
           CollapsedWidth="64"
           @bind-Collapsed=@collapsed
         >
        <div class="logo" />
        <Menu Theme="MenuTheme.Dark" Mode="MenuMode.Inline" DefaultSelectedKeys=@(new[]{"4"})>
            <MenuItem Key="1">
                <Icon Type="@IconType.Outline.User" />
                <span class="nav-text">nav 1</span>
            </MenuItem>
            <MenuItem Key="2">
                <Icon Type="@IconType.Outline.VideoCamera" />
                <span class="nav-text">nav 2</span>
            </MenuItem>
            <MenuItem Key="3">
                <Icon Type="@IconType.Outline.Upload" />
                <span class="nav-text">nav 3</span>
            </MenuItem>
            <MenuItem Key="4">
                <Icon Type="@IconType.Outline.User" />
                <span class="nav-text">nav 4</span>
            </MenuItem>
        </Menu>
    </Sider>
    <Layout>
        <Header Class="site-layout-sub-header-background" Style="padding: 0;" ></Header>
        <Content Style=" margin: 24px 16px 0;">
            <div class="site-layout-background" style="padding: 24px; min-height: 360px">
                content
            </div>
        </Content>
        <Footer Style="text-align: center;">Ant Design ©2018 Created by Ant UED</Footer>
    </Layout>
</Layout>

@code{
    bool collapsed;
}

<style>
    #components-layout-demo-responsive .logo {
        height: 32px;
        background: rgba(255, 255, 255, 0.2);
        margin: 16px;
    }

    .site-layout-sub-header-background {
        background: #fff;
    }

    .site-layout-background {
        background: #fff;
    }
</style>
```

## 4. 固定头部

关键差异：一般用于固定顶部导航，方便页面切换。

```razor
<Layout>
    <Header Style="position: fixed; z-index: 1; width: 100%; ">
        <div class="logo" />
        <Menu Theme="MenuTheme.Dark" Mode="MenuMode.Horizontal" DefaultSelectedKeys=@(new[]{"2"})>
            <MenuItem Key="1">nav 1</MenuItem>
            <MenuItem Key="2">nav 2</MenuItem>
            <MenuItem Key="3">nav 3</MenuItem>
        </Menu>
    </Header>
    <Content Class="site-layout" Style="padding:0 50px; margin-top: 64px; ">
        <Breadcrumb Style=" margin: 16px 0;">
            <BreadcrumbItem>Home</BreadcrumbItem>
            <BreadcrumbItem>List</BreadcrumbItem>
            <BreadcrumbItem>App</BreadcrumbItem>
        </Breadcrumb>
        <div class="site-layout-background" style="padding: 24px; min-height: 380px ">
            Content
        </div>
    </Content>
    <Footer Style="text-align: center;">Ant Design ©2018 Created by Ant UED</Footer>
</Layout>

<style>
    #components-layout-demo-fixed .logo {
        width: 120px;
        height: 31px;
        background: rgba(255, 255, 255, 0.2);
        margin: 16px 24px 16px 0;
        float: left;
    }

    .site-layout .site-layout-background {
        background: #fff;
    }
</style>
```

## 5. 自定义触发器

关键差异：要使用自定义触发器，可以设置 `trigger={null}` 来隐藏默认设定。

```razor
<Layout>
    <Sider @bind-Collapsed=@collapsed NoTrigger OnCollapse="OnCollapse">
        <div class="logo" />
        <Menu Theme="MenuTheme.Dark" Mode="MenuMode.Inline" DefaultSelectedKeys=@(new[]{"1"})>
            <MenuItem Key="1">
                <Icon Type="@IconType.Outline.User" />
                <span>nav 1</span>
            </MenuItem>
            <MenuItem Key="2">
                <Icon Type="@IconType.Outline.VideoCamera" />
                <span>nav 2</span>
            </MenuItem>
            <MenuItem Key="3">
                <Icon Type="@IconType.Outline.Upload" />
                <span>nav 3</span>
            </MenuItem>
        </Menu>
    </Sider>
    <Layout Class="site-layout">
        <Header Class="site-layout-background" Style="padding: 0;">
            @if (collapsed)
            {
                <Icon Type="@IconType.Outline.MenuUnfold" Class="trigger" OnClick="toggle" />
            }
            else
            {
                <Icon Type="@IconType.Outline.MenuFold" Class="trigger" OnClick="toggle" />
            }
        </Header>
        <Content Class="site-layout-background" Style="margin: 24px 16px;padding: 24px;min-height: 280px;">
            Content
        </Content>
    </Layout>
</Layout>

<style>
    #components-layout-demo-custom-trigger .trigger {
        font-size: 18px;
        line-height: 64px;
        padding: 0 24px;
        cursor: pointer;
        transition: color 0.3s;
    }

        #components-layout-demo-custom-trigger .trigger:hover {
            color: #1890ff;
        }

    #components-layout-demo-custom-trigger .logo {
        height: 32px;
        background: rgba(255, 255, 255, 0.2);
        margin: 16px;
    }

    .site-layout .site-layout-background {
        background: #fff;
    }
</style>


@code{
    bool collapsed;

    void toggle()
    {
        collapsed = !collapsed;
    }

    void OnCollapse(bool isCollapsed)
    {
        Console.WriteLine($"Collapsed: {isCollapsed}");
    }

}
```
