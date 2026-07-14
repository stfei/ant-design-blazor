# Ant Design Blazor Space 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 分隔符 | 相邻组件分隔符。 |
| 垂直间距 | 相邻组件垂直间距。 可以设置 `width: 100%` 独占一行。 |
| 自动换行 | 自动换行。 |
| 间距大小 | 间距预设大、中、小三种大小。 通过设置 `Size` 为 `SpaceSize.Large` `SpaceSize.Middle` 分别把间距设为大、中间距。若不设置 `Size`，则间距为小。 |
| 对齐 | 设置对齐模式。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Space Size="SpaceSize.Middle">
    <SpaceItem><Button Type="ButtonType.Primary">保存</Button></SpaceItem>
    <SpaceItem><Button>取消</Button></SpaceItem>
</Space>
```

## 2. 分隔符

关键差异：相邻组件分隔符。

```razor
<Space>
    <Split>
        <Divider Type="DividerType.Vertical" />
    </Split>
    <ChildContent>
        <SpaceItem>
            <Link>Link</Link>
        </SpaceItem>
        <SpaceItem>
            <Link>Link</Link>
        </SpaceItem>
        <SpaceItem>
            <Link>Link</Link>
        </SpaceItem>
    </ChildContent>
</Space>
```

## 3. 垂直间距

关键差异：相邻组件垂直间距。 可以设置 `width: 100%` 独占一行。

```razor
<Space Direction="SpaceDirection.Vertical">
    <SpaceItem>
        <Card Title=@("Card") Style="width: 300px;">
            <p>Card content</p>
            <p>Card content</p>
        </Card>
    </SpaceItem>
    <SpaceItem>
        <Card Title=@("Card") Style="width: 300px;">
            <p>Card content</p>
            <p>Card content</p>
        </Card>
    </SpaceItem>
</Space>
```

## 4. 自动换行

关键差异：自动换行。

```razor
<Space Size=@(("8", "16")) Wrap>
    @foreach (var index in Enumerable.Range(0, 20))
    {
        <SpaceItem>
            <Button>Button</Button>
        </SpaceItem>
    }
</Space>
```

## 5. 间距大小

关键差异：间距预设大、中、小三种大小。 通过设置 `Size` 为 `SpaceSize.Large` `SpaceSize.Middle` 分别把间距设为大、中间距。若不设置 `Size`，则间距为小。

```razor
<RadioGroup @bind-Value="@size" OnChange="e=> setSize(e)" TValue="SpaceSize">
    <Radio Value="SpaceSize.Small">Small</Radio>
    <Radio Value="SpaceSize.Middle">Middle</Radio>
    <Radio Value="SpaceSize.Large">Large</Radio>
</RadioGroup>
<br />
<Space Size=@size>
    <SpaceItem>
        <Button Type="ButtonType.Primary">Primary</Button>
    </SpaceItem>
    <SpaceItem>
        <Button>Default</Button>
    </SpaceItem>
    <SpaceItem>
        <Button Type="ButtonType.Dashed">Dashed</Button>
    </SpaceItem>
    <SpaceItem>
        <Button Type="ButtonType.Link">Link</Button>
    </SpaceItem>
</Space>

@code {
    SpaceSize size = SpaceSize.Small;
    void setSize(SpaceSize value)
    {
        size = value;
        StateHasChanged();
    }
}
```

## 6. 对齐

关键差异：设置对齐模式。

```razor
<div class="space-align-container">
    <div class="space-align-block">
        <Space Align="SpaceAlign.Center">
            <SpaceItem>
                center
            </SpaceItem>
            <SpaceItem>
                <Button Type="ButtonType.Primary">Primary</Button>
            </SpaceItem>
            <SpaceItem>
                <span class="mock-block">Block</span>
            </SpaceItem>
        </Space>
    </div>
    <div class="space-align-block">
        <Space Align="SpaceAlign.Start">
            <SpaceItem>
                start
            </SpaceItem>
            <SpaceItem>
                <Button Type="ButtonType.Primary">Primary</Button>
            </SpaceItem>
            <SpaceItem>
                <span class="mock-block">Block</span>
            </SpaceItem>
        </Space>
    </div>
    <div class="space-align-block">
        <Space Align="SpaceAlign.End">
            <SpaceItem>
                end
                <Button Type="ButtonType.Primary">Primary</Button>
            </SpaceItem>
            <SpaceItem>
                <span class="mock-block">Block</span>
            </SpaceItem>
        </Space>
    </div>
    <div class="space-align-block">
        <Space Align="SpaceAlign.Baseline">
            <SpaceItem>
                baseline
                <Button Type="ButtonType.Primary">Primary</Button>
            </SpaceItem>
            <SpaceItem>
                <span class="mock-block">Block</span>
            </SpaceItem>
        </Space>
    </div>
</div>

<style>
    .space-align-container {
        display: flex;
        align-item: flex-start;
        flex-wrap: wrap;
    }

    .space-align-block {
        margin: 8px 4px;
        border: 1px solid #40a9ff;
        padding: 4px;
        flex: none;
    }

        .space-align-block .mock-block {
            display: inline-block;
            padding: 32px 8px 16px;
            background: rgba(150, 150, 150, 0.2);
        }
</style>
```
