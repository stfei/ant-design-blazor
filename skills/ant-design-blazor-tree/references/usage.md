# Ant Design Blazor Tree 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 目录 | 内置的目录树，`multiple` 模式支持 `ctrl(Windows)` / `command(Mac)` 复选。 |
| 自定义图标 | 可以针对不同的节点定制图标。 |
| 异步数据加载 | 点击展开节点，动态加载数据。 |
| 拖动示例 | 将节点拖拽到其他节点内部或前后。 |
| Node Checkable | 使用`Checkable="false"`来标记节点不可勾选。 > 注意：也可以使用`CheckableExpression`来动态设置节点是否可勾选。 |
| 连接线 | 节点之间带连接线的树，常用于文件目录结构展示。使用 `showLine` 开启，可以用 `switcherIcon` 修改默认图标。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Tree TItem="string"
      Checkable
      DefaultExpandAll
      OnSelect="HandleSelect">
    <TreeNode Key="root" Title="根节点">
        <TreeNode Key="child-1" Title="子节点一" />
        <TreeNode Key="child-2" Title="子节点二" />
    </TreeNode>
</Tree>

@code {
    private void HandleSelect(TreeEventArgs<string> args)
    {
        Console.WriteLine(args.Node.Key);
    }
}
```

## 2. 目录

关键差异：内置的目录树，`multiple` 模式支持 `ctrl(Windows)` / `command(Mac)` 复选。

```razor
<DirectoryTree TItem="string"
			   Multiple
			   DefaultExpandAll
			   OnSelect="OnSelect"
			   OnExpand="OnExpand">
	<TreeNode Title="parent 0" Key="0-0">
		<TreeNode Title="leaf 0-0" Key="0-0-0" />
		<TreeNode Title="leaf 0-1" Key="0-0-1" />
	</TreeNode>
	<TreeNode Title="parent 1" Key="0-1">
		<TreeNode Title="leaf 1-0" Key="0-1-0" />
		<TreeNode Title="leaf 1-1" Key="0-1-1" />
	</TreeNode>
</DirectoryTree>

@code {
	void OnSelect()
	{

	}

	void OnExpand()
	{

	}
}
```

## 3. 自定义图标

关键差异：可以针对不同的节点定制图标。

```razor
<Tree TItem="string"
	  ShowIcon
	  DefaultExpandAll
	  DefaultSelectedKeys="@(new[]{"0-0-0"})">
	<SwitcherIconTemplate>
		@switcherIcon
	</SwitcherIconTemplate>
	<Nodes>
		<TreeNode Title="parent 1" Key="0-0" Icon="@IconType.Outline.Smile">
			<TreeNode Title="leaf" Key="0-0-0" Icon="@IconType.Outline.Meh" />
		</TreeNode>
		<TreeNode Title="leaf" Key="0-0-1">
			<IconTemplate Context="node">
				@if (node.Selected)
				{
					<Icon Type="@IconType.Fill.Frown"/>
				}
				else
				{
					<Icon Type="@IconType.Outline.Frown" />
				}
			</IconTemplate>
		</TreeNode>
	</Nodes>
</Tree>

@code {
	RenderFragment switcherIcon =@<Icon Type="@IconType.Outline.Down" />;
}
```

## 4. 异步数据加载

关键差异：点击展开节点，动态加载数据。

```razor
<div>
    <Tree TItem="Data"
          DataSource="_datas" @ref="_tree"
          TitleExpression="x => x.DataItem.Title"
          ChildrenExpression="x => x.DataItem.Childs"
          IsLeafExpression="x => x.TreeLevel > 2"
          OnNodeLoadDelayAsync="OnNodeLoadDelayAsync"></Tree>
</div>

<Button OnClick="ExpandAll">Expand All</Button>
<Button OnClick="ExpandAllChild">Expand Node "C" And Expand All Child Nodes</Button>

@code {
    public record Data(string Title)
    {
        public List<Data> Childs { get; set; } = new List<Data>();
    }

    List<Data> _datas = new() { new("A"), new("B"), new("C") };

    Tree<Data> _tree;

    private void ExpandAll()
    {
        _tree.ExpandAll();
    }

    private void ExpandAllChild()
    {
        _tree.ExpandAll(n => n.Title == "C");
    }

    public async Task OnNodeLoadDelayAsync(TreeEventArgs<Data> args)
    {
        if (args.Node.TreeLevel < 3)
        {
            await Task.Delay(2000);//模拟异步执行

            var dataItem = ((Data)args.Node.DataItem);
            dataItem.Childs.Clear();
            dataItem.Childs.AddRange(new List<Data>() { new Data($"{dataItem.Title}-1"), new Data($"{dataItem.Title}-2") });
        }
    }
}
```

## 5. 拖动示例

关键差异：将节点拖拽到其他节点内部或前后。

```razor
<Tree TItem="Node"
      DataSource="nodes"
      Draggable
      DefaultExpandAll
      TitleExpression="x => x.DataItem.Title"
      ChildrenExpression="x => x.DataItem.Children"
      KeyExpression="x => x.DataItem.Key"
      OnDrop="HandleDrop" />

@code {
    private readonly List<Node> nodes = new()
    {
        new("0", "父节点")
        {
            Children = new() { new("0-0", "子节点") }
        },
        new("1", "另一个节点")
    };

    private void HandleDrop(TreeEventArgs<Node> args)
    {
        // 根据 args 中的拖拽节点和目标节点更新业务树数据。
    }

    private sealed record Node(string Key, string Title)
    {
        public List<Node> Children { get; init; } = new();
    }
}
```

## 6. Node Checkable

关键差异：使用`Checkable="false"`来标记节点不可勾选。 > 注意：也可以使用`CheckableExpression`来动态设置节点是否可勾选。

```razor
<Tree TItem="string"
      Checkable
      CheckStrictly
      ExpandOnClickNode
      @bind-CheckedKeys="_checkedKeys"
      DefaultExpandAll>
	<TreeNode Title="parent 1" Key="0-0" Checkable="false">
        <TreeNode Title="parent 1-0" Key="0-0-0" Checkable="false">
			<TreeNode Title="leaf" Key="0-0-0-0" ></TreeNode>
            <TreeNode Title="leaf" Key="0-0-0-1" ></TreeNode>
            <TreeNode Title="leaf" Key="0-0-0-2" ></TreeNode>
		</TreeNode>
        <TreeNode Title="parent 1-1" Key="0-0-1" Checkable="false">
			<TreeNode Key="0-0-1-0" Title="sss">
				<TitleTemplate>
					<span style="color: #1890ff; ">sss</span>
				</TitleTemplate>
			</TreeNode>
		</TreeNode>
	</TreeNode>
</Tree>

@code
{
    string[] _checkedKeys = { "0-0-0-0", "0-0-0-1" };
}
```

## 7. 连接线

关键差异：节点之间带连接线的树，常用于文件目录结构展示。使用 `showLine` 开启，可以用 `switcherIcon` 修改默认图标。

```razor
<div>
	<div style="margin-bottom: 16px">
		showLine: <Switch @bind-Checked="_showLine" />
		<br />
		<br />
		showIcon: <Switch @bind-Checked="_showIcon" />
		<br />
		<br />
		showLeafIcon: <Switch @bind-Checked="_showLeafIcon" />
	</div>

	<Tree TItem="string"
          ShowLine="@_showLine"
		  ShowIcon="@_showIcon"
		  ShowLeafIcon="@_showLeafIcon"
		  DefaultExpandedKeys="@(new[]{"0-0-0"})"
		  OnSelect="OnSelect"
		 >
		<TreeNode Title="parent 1" Key="0-0" Icon="carry-out">
			<TreeNode Title="parent 1-0" Key="0-0-0" Icon="carry-out">
				<TreeNode Title="leaf" Key="0-0-0-0" Icon="carry-out" />
				<TreeNode Key="0-0-0-1" Icon="carry-out">
					<TitleTemplate>
						<div>
							<div>multiple line title</div>
							<div>multiple line title</div>
						</div>
					</TitleTemplate>
				</TreeNode>
				<TreeNode Title="leaf" Key="0-0-0-2" Icon="carry-out" />
			</TreeNode>
			<TreeNode Title="parent 1-1" Key="0-0-1" Icon="carry-out">
				<TreeNode Title="left" Key="0-0-1-0" Icon="carry-out" />
			</TreeNode>
			<TreeNode Title="parent 1-2" Key="0-0-2" Icon="carry-out">
				<TreeNode Title="leaf" Key="0-0-2-0" Icon="carry-out" />
				<TreeNode Title="leaf" Key="0-0-2-1" Icon="carry-out" SwitcherIcon="form" />
			</TreeNode>
		</TreeNode>
		<TreeNode Title="parent 2" Key="0-1" Icon="carry-out">
			<TreeNode Title="parent 2-0" Key="0-1-0" Icon="carry-out">
				<TreeNode Title="leaf" Key="0-1-0-0" Icon="carry-out" />
				<TreeNode Title="leaf" Key="0-1-0-1" Icon="carry-out" />
			</TreeNode>
		</TreeNode>
	</Tree>
</div>

@code {
	bool _showLine = true;
	bool _showIcon = false;
	bool _showLeafIcon = true;

	void OnSelect(TreeEventArgs<string> e)
	{
		Console.WriteLine(JsonSerializer.Serialize(e.Node.DataItem));
	}
}
```
