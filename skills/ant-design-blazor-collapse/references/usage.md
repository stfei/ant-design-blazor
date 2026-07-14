# Ant Design Blazor Collapse 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 手风琴 | 手风琴，每次只打开一个 tab 。 |
| 简洁风格 | 一套没有边框的简洁样式。 |
| 可控制展开面板 | 通过 `@bind-ActiveKeys` 可以从外部控制展开的面板。你可以通过按钮来控制面板的展开/折叠状态。 |
| 额外节点 | 你可以通过 `Extra` 来指定面板右上角的额外内容。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Collapse DefaultActiveKey="@(new[] { "1" })" Accordion>
    <Panel Key="1" Header="基本信息">基本信息内容</Panel>
    <Panel Key="2" Header="高级设置">高级设置内容</Panel>
</Collapse>
```

## 2. 手风琴

关键差异：手风琴，每次只打开一个 tab 。

```razor
<Collapse DefaultActiveKey="@(new[]{"1"})" Accordion>
    <Panel Header="This is panel header 1" Key="1">
        <p>@text</p>
    </Panel>
    <Panel Header="This is panel header 2" Key="2">
        <p>@text</p>
    </Panel>
    <Panel Header="This is panel header 3" Key="3">
        <p>@text</p>
    </Panel>
</Collapse>

@code{

    string text = @"
A dog is a type of domesticated animal.
Known for its loyalty and faithfulness,
it can be found as a welcome guest in many households across the world.
";

}
```

## 3. 简洁风格

关键差异：一套没有边框的简洁样式。

```razor
<Collapse DefaultActiveKey="@(new[]{"1"})" Bordered="false">
    <Panel Header="This is panel header 1" Key="1">
        <p>@text</p>
    </Panel>
    <Panel Header="This is panel header 2" Key="2">
        <p>@text</p>
    </Panel>
    <Panel Header="This is panel header 3" Key="3">
        <p>@text</p>
    </Panel>
</Collapse>

@code{

    string text = @"
A dog is a type of domesticated animal.
Known for its loyalty and faithfulness,
it can be found as a welcome guest in many households across the world.
";

}
```

## 4. 可控制展开面板

关键差异：通过 `@bind-ActiveKeys` 可以从外部控制展开的面板。你可以通过按钮来控制面板的展开/折叠状态。

```razor
<Collapse @bind-ActiveKeys="@_activeKeys">
    <Panel Key="1" Header="This is panel header 1">
        <p>A dog is a type of domesticated animal. Known for its loyalty and faithfulness, it can be found as a welcome guest in many households across the world.</p>
    </Panel>
    <Panel Key="2" Header="This is panel header 2">
        <p>A cat is a type of domesticated animal. Known for its independent and aloof nature, it is a common pet in many households.</p>
    </Panel>
    <Panel Key="3" Header="This is panel header 3">
        <p>A bird is a type of wild or domesticated animal. Known for its ability to fly and sing, it brings joy to many people.</p>
    </Panel>
</Collapse>
<br />
<Space>
    <SpaceItem>
        <Button Type="@ButtonType.Primary" OnClick="@(() => TogglePanel("1"))">Toggle Panel 1</Button>
    </SpaceItem>
    <SpaceItem>
        <Button Type="@ButtonType.Primary" OnClick="@(() => TogglePanel("2"))">Toggle Panel 2</Button>
    </SpaceItem>
    <SpaceItem>
        <Button Type="@ButtonType.Primary" OnClick="@(() => TogglePanel("3"))">Toggle Panel 3</Button>
    </SpaceItem>
</Space>

@code {
    private string[] _activeKeys = Array.Empty<string>();

    private void TogglePanel(string key)
    {
        if (_activeKeys.Contains(key))
        {
            _activeKeys = _activeKeys.Where(k => k != key).ToArray();
        }
        else
        {
            _activeKeys = _activeKeys.Append(key).ToArray();
        }
    }
}
```

## 5. 额外节点

关键差异：你可以通过 `Extra` 来指定面板右上角的额外内容。

```razor
<div>
    <Collapse DefaultActiveKey="@(new[] { "1" })"
              OnChange="Callback"
              ExpandIconPosition="@expandIconPosition"
              ExpandIcon="@IconType.Outline.CaretRight">
        <Panel Header="This is panel header 1" Key="1" ExtraTemplate="@extra">
            <div>@text</div>
        </Panel>
        <Panel Header="This is panel header 2" Key="2" ExtraTemplate="@extra">
            <div>@text</div>
        </Panel>
        <Panel Header="This is panel header 3" Key="3">
            <ExtraTemplate>
                <div @onclick:stopPropagation><Icon Type="@IconType.Outline.Snippets" /></div>
            </ExtraTemplate>
            <ChildContent>
                <div>@text</div>
            </ChildContent>
        </Panel>
    </Collapse>
</div>
@code{

    string text = @"
A dog is a type of domesticated animal.
Known for its loyalty and faithfulness,
it can be found as a welcome guest in many households across the world.
";

    RenderFragment extra =@<div @onclick:stopPropagation><Icon Type="@IconType.Outline.Setting" /></div>;

    CollapseExpandIconPosition expandIconPosition = CollapseExpandIconPosition.Left;

    void Callback(string[] keys)
    {
        Console.WriteLine(string.Join(',',keys));
    }
}
```
