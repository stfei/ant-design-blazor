# Ant Design Blazor Menu 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 顶部导航 | 水平的顶部导航菜单。 |
| 内嵌菜单 | 垂直菜单，子菜单内嵌在菜单区域。 |
| 缩起内嵌菜单 | 内嵌菜单可以被缩起/展开。 你可以在 [Layout](/components/layout/#components-layout-demo-side) 里查看侧边布局结合的完整示例。 |
| 主题 | 内建了两套主题 `light\|dark`，默认 `light`。 |
| 配合路由使用 | 自动根据路由激活菜单项，使用 `RouterLink` 属性。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Menu Mode="MenuMode.Inline" SelectedKeys="@(new[] { "dashboard" })">
    <MenuItem Key="dashboard" RouterLink="/dashboard">仪表盘</MenuItem>
    <SubMenu Key="settings" Title="设置">
        <MenuItem Key="users" RouterLink="/users">用户</MenuItem>
    </SubMenu>
</Menu>
```

## 2. 顶部导航

关键差异：水平的顶部导航菜单。

```razor
<Menu Mode=MenuMode.Horizontal>
    <MenuItem Key="mail">
        <Icon Type="@IconType.Outline.Mail" />
        Navigation One
    </MenuItem>
    <MenuItem Key="app" Disabled>
        <Icon Type="@IconType.Outline.Appstore" />
        Navigation Two
    </MenuItem>
    <SubMenu TitleTemplate=@sub1Title>
        <MenuItemGroup Title="Item 1">
            <MenuItem Key="setting:1">Option 1</MenuItem>
            <MenuItem Key="setting:2">Option 2</MenuItem>
        </MenuItemGroup>
        <MenuDivider />
        <MenuItemGroup Title="Item 2">
            <MenuItem Key="setting:3">Option 3</MenuItem>
            <SubMenu Title="Option 4">
                <MenuItem Key="setting4:1">Option 1</MenuItem>
                <MenuItem Key="setting4:2">Option 2</MenuItem>
            </SubMenu>
        </MenuItemGroup>
    </SubMenu>
    <MenuItem Key="alipay">
        <MenuLink Href="https://ant.design" Target="@MenuTarget.Blank" rel="noopener noreferrer">
            Navigation Four - Link
        </MenuLink>
    </MenuItem>
</Menu>

@code
{
    RenderFragment sub1Title =
    @<Template>
        <Icon Type="@IconType.Outline.Setting" />
        Navigation Three - Submenu
    </Template>;
}
```

## 3. 内嵌菜单

关键差异：垂直菜单，子菜单内嵌在菜单区域。

```razor
<Menu Style="width: 256px;"
      DefaultSelectedKeys=@(new[] { "1" })
      DefaultOpenKeys=@(new[] { "sub1" })
      Mode=@MenuMode.Inline>
    <SubMenu Key="sub1" TitleTemplate=@sub1Title>
        <MenuItemGroup Key="g1" Title="Item 1">
            <MenuItem Key="1">Option 1</MenuItem>
            <MenuItem Key="2">Option 2</MenuItem>
        </MenuItemGroup>
        <MenuItemGroup Key="g2" Title="Item 2">
            <MenuItem Key="3">Option 3</MenuItem>
            <MenuItem Key="4">Option 4</MenuItem>
        </MenuItemGroup>
    </SubMenu>
    <SubMenu Key="sub2" TitleTemplate=@sub2Title>
        <MenuItem Key="5">Option 5</MenuItem>
        <MenuItem Key="6">Option 6</MenuItem>
        <SubMenu Key="sub3" Title="Submenu">
            <MenuItem Key="7">Option 7</MenuItem>
            <MenuItem Key="8">Option 8</MenuItem>
        </SubMenu>
    </SubMenu>
    <SubMenu Key="sub4" TitleTemplate=@sub4Title>
        <MenuItem Key="9">Option 9</MenuItem>
        <MenuItem Key="10">Option 10</MenuItem>
        <MenuItem Key="11">Option 11</MenuItem>
        <MenuItem Key="12">Option 12</MenuItem>
    </SubMenu>
</Menu>

@code
{
    RenderFragment sub1Title =
    @<span>
        <Icon Type="@IconType.Outline.Mail" />
        <span>Navigation One</span>
    </span>;

    RenderFragment sub2Title =
    @<span>
        <Icon Type="@IconType.Outline.Appstore" />
        <span>Navigation Two</span>
    </span>;

    RenderFragment sub4Title =
    @<span>
        <Icon Type="@IconType.Outline.Setting" />
        <span>Navigation Three</span>
    </span>;
    }
```

## 4. 缩起内嵌菜单

关键差异：内嵌菜单可以被缩起/展开。 你可以在 [Layout](/components/layout/#components-layout-demo-side) 里查看侧边布局结合的完整示例。

```razor
@inject IconService iconService

<div style="width: 256px;">
	<Button Type="ButtonType.Primary" OnClick="ToggleCollapsed" Style="margin-bottom: 16px">
		@if (collapsed)
		{
			<Icon Type="@IconType.Outline.MenuUnfold" />
		}
		else
		{
			<Icon Type="@IconType.Outline.MenuFold" />
		}
	</Button>
	<Menu DefaultSelectedKeys=@(new[] { "1" })
		  DefaultOpenKeys=@(new[] { "sub1" })
		  Mode=MenuMode.Inline
		  Theme=MenuTheme.Dark
		  InlineCollapsed=collapsed>
		<MenuItem Key="1" Icon="@IconType.Outline.PieChart">
			<IconTemplate>
				<IconFont Type="icon-c-sharp-l" />
			</IconTemplate>
			<ChildContent>
				Option 1
			</ChildContent>
		</MenuItem>
        <MenuItem Key="2" Icon="@IconType.Outline.Desktop">
			Option 2
		</MenuItem>
        <MenuItem Key="3" Icon="@IconType.Outline.Container">
			Option 3
		</MenuItem>
		<SubMenu Key="sub1" TitleTemplate="@sub1Title">
			<MenuItem Key="5">Option 5</MenuItem>
			<MenuItem Key="6">Option 6</MenuItem>
			<MenuItem Key="7">Option 7</MenuItem>
			<MenuItem Key="8">Option 8</MenuItem>
		</SubMenu>
		<SubMenu Key="sub2" TitleTemplate="@sub2Title">
			<MenuItem Key="9">Option 9</MenuItem>
			<MenuItem Key="10">Option 10</MenuItem>
			<SubMenu Key="sub3" Title="Submenu">
				<MenuItem Key="11">Option 11</MenuItem>
				<MenuItem Key="12">Option 12</MenuItem>
			</SubMenu>
		</SubMenu>
	</Menu>
</div>

@code {
	bool collapsed = false;

	RenderFragment sub1Title =
	@<span>
        <Icon Type="@IconType.Outline.Mail" />
		<span>Navigation One</span>
	</span>;

	RenderFragment sub2Title =
	@<span>
        <Icon Type="@IconType.Outline.Appstore" />
		<span>Navigation Two</span>
	</span>;

	void ToggleCollapsed()
	{
		collapsed = !collapsed;
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await iconService.CreateFromIconfontCN("//at.alicdn.com/t/font_2735473_hi62ezq5579.js");
		}
	}
}
```

## 5. 主题

关键差异：内建了两套主题 `light\|dark`，默认 `light`。

```razor
<div>
    <Switch Checked="theme == MenuTheme.Dark" OnChange=changeTheme CheckedChildren=@("Dark") UnCheckedChildren=@("Light") />
    <br />
    <br />
    <Menu Theme=theme
          OnMenuItemClicked=handleClick
          Style=" width: 256px "
          DefaultOpenKeys=@(new []{"sub1"})
          SelectedKeys=@(new []{current})
          Mode="MenuMode.Inline">
        <SubMenu Key="sub1" TitleTemplate=@sub1Title>
            <MenuItem Key="1">Option 1</MenuItem>
            <MenuItem Key="2">Option 2</MenuItem>
            <MenuItem Key="3">Option 3</MenuItem>
            <MenuItem Key="4">Option 4</MenuItem>
        </SubMenu>
        <SubMenu Key="sub2" TitleTemplate=@sub2Title>
            <MenuItem Key="5">Option 5</MenuItem>
            <MenuItem Key="6">Option 6</MenuItem>
            <SubMenu Key="sub3" Title="Submenu">
                <MenuItem Key="7">Option 7</MenuItem>
                <MenuItem Key="8">Option 8</MenuItem>
            </SubMenu>
        </SubMenu>
        <SubMenu Key="sub4" TitleTemplate=@sub4Title>
            <MenuItem Key="9">Option 9</MenuItem>
            <MenuItem Key="10">Option 10</MenuItem>
            <MenuItem Key="11">Option 11</MenuItem>
            <MenuItem Key="12">Option 12</MenuItem>
        </SubMenu>
    </Menu>
</div>

@code {

    RenderFragment sub1Title =
        @<span>
        <Icon Type="@IconType.Outline.Mail" />
            <span>Navigation One</span>
        </span>;

    RenderFragment sub2Title =
        @<span>
            <Icon Type="@IconType.Outline.Appstore" />
            <span>Navigation Two</span>
        </span>;

    RenderFragment sub4Title =
        @<span>
            <Icon Type="@IconType.Outline.Setting" />
            <span>Navigation Three</span>
        </span>;

    MenuTheme theme = MenuTheme.Dark;
    string current = "1";

    void changeTheme(bool value)
    {
        this.theme = value ? MenuTheme.Dark : MenuTheme.Light;
    }

    void handleClick(MenuItem e)
    {
        current = e.Key;
    }

}
```

## 6. 配合路由使用

关键差异：自动根据路由激活菜单项，使用 `RouterLink` 属性。

```razor
<Menu Mode="MenuMode.Horizontal">
    <MenuItem RouterLink="/en-US/components/menu" RouterMatch="NavLinkMatch.All">English Menu Document</MenuItem>
    <MenuItem RouterLink="/zh-CN/components/menu" RouterMatch="NavLinkMatch.All">Chinese Menu Document</MenuItem>
</Menu>

<br />
<br />

<div style="width: 256px;">
    <Menu Mode="MenuMode.Inline">
        <SubMenu Title="Sub Menu1">
            <MenuItem RouterLink="/en-US/components/menu" RouterMatch="NavLinkMatch.All">English Menu Document</MenuItem>
        </SubMenu>
        <SubMenu Title="Sub Menu2">
            <MenuItem RouterLink="/zh-CN/components/menu" RouterMatch="NavLinkMatch.All">Chinese Menu Document</MenuItem>
        </SubMenu>
    </Menu>
</div>
```
