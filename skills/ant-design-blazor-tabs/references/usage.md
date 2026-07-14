# Ant Design Blazor Tabs 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 新增和关闭页签 | 只有卡片样式的页签支持新增和关闭选项。使用 `closable={false}` 禁止关闭。 |
| 位置 | 有四个位置，`tabPosition="left\|right\|top\|bottom"`。 |
| 附加内容 | 可以在页签两边添加附加操作。 |
| 禁用 | 禁用某一项。 |
| 居中 | 标签居中展示。 |
| 嵌套 | 默认选中第一项。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Tabs ActiveKey="_activeKey" OnChange="key => _activeKey = key">
    <TabPane Key="base" Tab="基本信息">基本信息内容</TabPane>
    <TabPane Key="security" Tab="安全设置">安全设置内容</TabPane>
</Tabs>

@code {
    private string _activeKey = "base";
}
```

## 2. 新增和关闭页签

关键差异：只有卡片样式的页签支持新增和关闭选项。使用 `closable={false}` 禁止关闭。

```razor
<Tabs DefaultActiveKey="1" Type="TabType.EditableCard" OnAddClick="OnAddClick" OnClose="OnTabClose" @bind-ActiveKey="activeKey">
    @foreach (var pane in panes)
    {
        <TabPane Key="@pane.Key" Tab="@pane.Title" Closable="@pane.Closable">
            @pane.Content
        </TabPane>
    }
</Tabs>

<Tabs DefaultActiveKey="1" Type="TabType.Line">
    <TabPane Key="0" Tab="0"></TabPane>
    @foreach (var tab in _tabs)
    {
        <TabPane Key="@tab.ToString()" Tab="@tab.ToString()">
            @tab.ToString()
        </TabPane>
    }
</Tabs>
<Button OnClick="AddNewTab">Add new</Button>

@code {
    private string activeKey;
    private int newTabIndex;

    List<int> _tabs = [];

    public record Pane(string Title, string Content, string Key, bool Closable = true);

    List<Pane> panes = new List<Pane>()
    {
        new("Tab 1", "Content of Tab Pane 1", "1"),
        new("Tab 2", "Content of Tab Pane 2", "2"),
        new("Tab 3", "Content of Tab Pane 3", "3", false),
    };

    private void OnAddClick()
    {
        var key = panes.Count + 1;
        activeKey = $"newTab{newTabIndex++}";
        panes.Add(new Pane($"Tab {key}", $"Content of Tab Pane {key}", activeKey));
    }

    void OnTabClose(string key)
    {
        Console.WriteLine($"tab close:{key}");
    }

    private void AddNewTab(MouseEventArgs obj)
    {
        _tabs.Add(_tabs.Count + 1);
    }

}
```

## 3. 位置

关键差异：有四个位置，`tabPosition="left\|right\|top\|bottom"`。

```razor
<div>
    <RadioGroup @bind-Value="position" Style="margin-bottom: 16px;">
        <Radio RadioButton Value="TabPosition.Top">Top</Radio>
        <Radio RadioButton Value="TabPosition.Left">Left</Radio>
        <Radio RadioButton Value="TabPosition.Right">Right</Radio>
        <Radio RadioButton Value="TabPosition.Bottom">Bottom</Radio>
    </RadioGroup>
    <Tabs DefaultActiveKey="1" TabPosition="@position">
        <TabPane Key="1" Tab="Tab 1">
            Content of Tab Pane 1
        </TabPane>
        <TabPane Key="2" Tab="Tab 2">
            Content of Tab Pane 2
        </TabPane>
        <TabPane Key="3" Tab="Tab 3">
            Content of Tab Pane 3
        </TabPane>
    </Tabs>
</div>

@code {
    private TabPosition position = TabPosition.Top;
}
```

## 4. 附加内容

关键差异：可以在页签两边添加附加操作。

```razor
<Tabs>
	<TabBarExtraContent>
		<Button>Extra Action</Button>
	</TabBarExtraContent>
	<ChildContent>
		<TabPane Key="1" Tab="Tab 1">
			Content of Tab Pane 1
		</TabPane>
		<TabPane Key="2" Tab="Tab 2">
			Content of Tab Pane 2
		</TabPane>
		<TabPane Key="3" Tab="Tab 3">
			Content of Tab Pane 3
		</TabPane>
	</ChildContent>
</Tabs>
<br />
<br />
<br />
<div>You can also specify its direction or both side</div>
<Divider />
<CheckboxGroup Options=@(new[]{"left","right"}) @bind-Value="position" />
<br />
<br />
<Tabs TabBarExtraContentLeft="leftExtra" TabBarExtraContentRight="rightExtra" >
	<TabPane Tab="Tab 1" Key="1">
		Content of tab 1
	</TabPane>
	<TabPane Tab="Tab 2" Key="2">
		Content of tab 2
	</TabPane>
	<TabPane Tab="Tab 3" Key="3">
		Content of tab 3
	</TabPane>
</Tabs>

@code {
	string[] position =new[] { "left", "right" };

	RenderFragment leftExtra => position.Contains("left")? @<Button class="tabs-extra-demo-button">Left Extra Action</Button> :null;
	RenderFragment rightExtra => position.Contains("right")? @<Button>Right Extra Action</Button> :null;
}

<style>
.tabs-extra-demo-button {
  margin-right: 16px;
}

.ant-row-rtl .tabs-extra-demo-button {
  margin-right: 0;
  margin-left: 16px;
}
</style>
```

## 5. 禁用

关键差异：禁用某一项。

```razor
<Tabs DefaultActiveKey="1">
    <TabPane Key="1" Tab="Tab 1">
        Tab 1
    </TabPane>
    <TabPane Key="2" Tab="Tab 2" Disabled>
        Tab 2
    </TabPane>
    <TabPane Key="3" Tab="Tab 3">
        Tab 3
    </TabPane>
</Tabs>
```

## 6. 居中

关键差异：标签居中展示。

```razor
<Tabs Centered>
	<TabPane Tab="Tab 1" Key="1">
		Content of Tab Pane 1
	</TabPane>
	<TabPane Tab="Tab 2" Key="2">
		Content of Tab Pane 2
	</TabPane>
	<TabPane Tab="Tab 3" Key="3">
		Content of Tab Pane 3
	</TabPane>
</Tabs>
```

## 7. 嵌套

关键差异：默认选中第一项。

```razor
<h3>Nest</h3>

Waitting for implementing Select.

@code {

}
```
