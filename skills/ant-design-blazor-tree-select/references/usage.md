# Ant Design Blazor TreeSelect 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 多选 | 多选的树选择。 |
| 可勾选 | 使用勾选框实现多选功能。 |
| 带搜索的多项选择 | 多项选择搜索使用。 |
| 从数据直接生成 | 使用 `DataSource` 把 IEnumerable<T> 数据直接生成树结构。 |
| 自定义状态 | 使用 `status` 为 TreeSelect 添加状态，可选 `error` 或者 `warning`。 |
| 后缀图标 | 最简单的用法。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<TreeSelect TItem="string"
            TItemValue="string"
            @bind-Value="_value"
            Placeholder="选择节点"
            AllowClear
            TreeDefaultExpandAll>
    <TreeNode TItem="string" Key="department" Title="部门">
        <TreeNode TItem="string" Key="sales" Title="销售部" />
        <TreeNode TItem="string" Key="tech" Title="技术部" />
    </TreeNode>
</TreeSelect>

@code {
    private string? _value;
}
```

## 2. 多选

关键差异：多选的树选择。

```razor
<TreeSelect TItem="string"
            TItemValue="string"
			Style="width:100%;"
			@bind-Values="values"
            DropdownStyle="max-height:400px;overflow:auto;"
			Placeholder="Please select"
			AllowClear
			Multiple
			TreeDefaultExpandAll>
	<TreeNode TItem="string" Key="parent 1" Title="parent 1">
		<TreeNode TItem="string" Key="parent 1-0" Title="parent 1-0">
			<TreeNode TItem="string" Key="leaf1" Title="my leaf" />
			<TreeNode TItem="string" Key="leaf2" Title="your leaf" />
		</TreeNode>
		<TreeNode TItem="string" Key="parent 1-1" Title="parent 1-1">
			<TreeNode TItem="string" Key="sss">
				<TitleTemplate>
					<b style=" color: #08c;">sss</b>
				</TitleTemplate>
			</TreeNode>
		</TreeNode>
	</TreeNode>
</TreeSelect>

@JsonSerializer.Serialize(values);

@code {

	private IEnumerable<string> values=new[]{"leaf1","leaf2"};

}
```

## 3. 可勾选

关键差异：使用勾选框实现多选功能。

```razor
<TreeSelect
            Style="width:100%;"
            DataSource="treeData"
            @bind-Values="@values"
            Placeholder="Please select"
            AllowClear
            TreeCheckable
            CheckOnClickNode
            TreeDefaultExpandAll
            ItemValue="node=> node.Key"
            ChildrenExpression="node=>node.DataItem.Children"
            TitleExpression="node=>node.DataItem.Title"
            KeyExpression="node=>node.DataItem.Key"
            IsLeafExpression="node=>node.DataItem.Children==null">
</TreeSelect>

@JsonSerializer.Serialize(values);

@code {
    private IEnumerable<string> values = new[] { "1", "11" };

  Data[] treeData = new Data[]
  {
        new()
        {
            Title = "Node1",
            Key="1",
            Children = new Data[]
            {
                new()
                {
                    Title = "Child Node1",
                    Key="11",
                },
            }
        },
        new()
        {
            Title = "Node2",
            Key="2",
            Children = new Data[]
            {
                new()
                {
                    Title ="Child Node3",
                    Key="21"
                },
                new()
                {
                    Title ="Child Node4",
                    Key="22",
                },
                new()
                {
                    Title ="Child Node5",
                    Key="23",
                }
            }
        }
  };

  public class Data : ITreeData<Data>
  {
    public string Key { get; set; }
    public Data Value => this;
    public string Title { get; set; }
    public IEnumerable<Data> Children { get; set; }
  }
}
```

## 4. 带搜索的多项选择

关键差异：多项选择搜索使用。

```razor
<TreeSelect TItem="string" TItemValue="string"
			Style="width:100%;"
			@bind-Values="values"
			DropdownStyle="max-height:400px;overflow:auto;"
			Placeholder="Please select"
			AllowClear
			Multiple
			TreeDefaultExpandAll
			EnableSearch
			MatchedStyle="font-weight: bold">
	<TreeNode TItem="string" Key="parent 1" Title="parent 1">
		<TreeNode TItem="string" Key="parent 1-0" Title="parent 1-0">
			<TreeNode TItem="string" Key="leaf1" Title="my leaf" />
			<TreeNode TItem="string" Key="leaf2" Title="your leaf" />
		</TreeNode>
		<TreeNode TItem="string" Key="parent 1-1" Title="parent 1-1">
			<TreeNode TItem="string" Key="leaf3" Title="Leaf3"/>
		</TreeNode>
	</TreeNode>
</TreeSelect>

@JsonSerializer.Serialize(values);

@code {

	private IEnumerable<string> values=new[]{"leaf1","leaf2"};

}
```

## 5. 从数据直接生成

关键差异：使用 `DataSource` 把 IEnumerable<T> 数据直接生成树结构。

```razor
<TreeSelect
            Style="width:100%;"
            DataSource="treeData"
            @bind-Values="@values"
            Placeholder="Please select"
            ItemValue="item=>item"
            ItemLabel="item=>item.Title"
            AllowClear
            Multiple
            EnableSearch
            TreeDefaultExpandAll
            MatchedStyle="font-weight: bold"
            ChildrenExpression="node=>node.DataItem.Children"
            TitleExpression="node=>node.DataItem.Title"
            TitleTemplate="node=>node.DataItem.Title.ToRenderFragment()"
            KeyExpression="node=>node.DataItem.Key.ToString()"
            IsLeafExpression="node=>node.DataItem.Children==null">
</TreeSelect>

@JsonSerializer.Serialize(values)

@code {
    private IEnumerable<Data> values;

    Data[] treeData = new Data[]
    {
        new()
        {
            Title = "Node1",
            Key = 1,
            Children = new Data[]
            {
                new()
                {
                    Title = "Child Node1",
                    Key = 11,
                },
                new()
                {
                    Title = "Child Node2",
                    Key = 12,
                }
            }
        },
        new()
        {
            Title = "Node2",
            Key = 13,
        },
        new()
        {
            Title="Node3",
            Key=14
        }
    };

  public class Data
  {
    public int Key { get; set; }
    public string Title { get; set; }
    public IEnumerable<Data> Children { get; set; }
  }
}
```

## 6. 自定义状态

关键差异：使用 `status` 为 TreeSelect 添加状态，可选 `error` 或者 `warning`。

```razor
<Space Direction="SpaceDirection.Vertical" Style="width: 100%">
  <SpaceItem>
        <TreeSelect TItem="string" TItemValue="string" Status="error" Style="width: 100%" Placeholder="Error" />
  </SpaceItem>
  <Space>
        <TreeSelect TItem="string" TItemValue="string" Status="warning" Style="width: 100%" Multiple Placeholder="Warning multiple" />
  </Space>

</Space>

@code {

}
```

## 7. 后缀图标

关键差异：最简单的用法。

```razor
<TreeSelect TItem="string" TItemValue="string"
            EnableSearch
			Style="width:100%;"
			@bind-Value="value"
            DropdownStyle="max-height:400px;overflow:auto;"
			Placeholder="Please select"
			AllowClear
			Multiple
            SuffixIcon="icon"
			TreeDefaultExpandAll>
	<TreeNode TItem="string" Key="parent 1" Title="parent 1">
		<TreeNode TItem="string" Key="parent 1-0" Title="parent 1-0">
			<TreeNode TItem="string" Key="leaf1" Title="my leaf" />
			<TreeNode TItem="string" Key="leaf2" Title="your leaf" />
		</TreeNode>
		<TreeNode TItem="string" Key="parent 1-1" Title="parent 1-1">
			<TreeNode TItem="string" Key="sss">
				<TitleTemplate>
					<b style=" color: #08c;">sss</b>
				</TitleTemplate>
			</TreeNode>
		</TreeNode>
	</TreeNode>
</TreeSelect>

@code {

  private string value;
  private RenderFragment icon =@<Icon Type="@IconType.Outline.Smile" />;
}
```
