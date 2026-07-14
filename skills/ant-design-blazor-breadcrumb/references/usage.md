# Ant Design Blazor Breadcrumb 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 分隔符 | 使用 `Separator=">"` 可以自定义分隔符。 |
| 带下拉菜单的面包屑 | 面包屑支持下拉菜单。 |
| 带有图标的 | 图标放在文字前面。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Breadcrumb>
    <BreadcrumbItem Href="/">首页</BreadcrumbItem>
    <BreadcrumbItem Href="/orders">订单</BreadcrumbItem>
    <BreadcrumbItem>详情</BreadcrumbItem>
</Breadcrumb>
```

## 2. 分隔符

关键差异：使用 `Separator=">"` 可以自定义分隔符。

```razor
<Breadcrumb Separator=">">
	<BreadcrumbItem>Home</BreadcrumbItem>
	<BreadcrumbItem Href="">Application Center</BreadcrumbItem>
	<BreadcrumbItem Href="">Application List</BreadcrumbItem>
	<BreadcrumbItem>An Application</BreadcrumbItem>
</Breadcrumb>
```

## 3. 带下拉菜单的面包屑

关键差异：面包屑支持下拉菜单。

```razor
<Breadcrumb>
	<BreadcrumbItem>
		Ant Design Blazor
	</BreadcrumbItem>
	<BreadcrumbItem>
		<a>Component</a>
	</BreadcrumbItem>
	<BreadcrumbItem>
		<ChildContent>
			<a>General</a>
		</ChildContent>
		<Overlay>
			<Menu>
				<MenuItem>
					<a>General</a>
				</MenuItem>
				<MenuItem>
					<a>Layout</a>
				</MenuItem>
				<MenuItem>
					<a>Navigation</a>
				</MenuItem>
			</Menu>
		</Overlay>
	</BreadcrumbItem>
	<BreadcrumbItem>
		Button
	</BreadcrumbItem>
</Breadcrumb>
```

## 4. 带有图标的

关键差异：图标放在文字前面。

```razor
<Breadcrumb>
	<BreadcrumbItem Href="">
		<Icon Type="@IconType.Outline.Home" />
	</BreadcrumbItem>
	<BreadcrumbItem Href="">
		<Icon Type="@IconType.Outline.User" />
		<span>Application List</span>
	</BreadcrumbItem>
	<BreadcrumbItem>
		Application
	</BreadcrumbItem>
</Breadcrumb>
```
