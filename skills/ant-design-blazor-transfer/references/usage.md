# Ant Design Blazor Transfer 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 带搜索框 | 带搜索框的穿梭框，可以自定义搜索函数。 |
| 自定义渲染行数据 | 自定义渲染每一个 Transfer Item，可用于渲染复杂数据。 |
| 高级用法 | 穿梭框高级用法，可配置操作文案，可定制宽高，可对底部进行自定义渲染。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Transfer DataSource="_items"
          TargetKeys="_targetKeys"
          Titles="@(new[] { "可选", "已选" })"
          OnChange="HandleChange" />

@code {
    private readonly List<TransferItem> _items = new()
    {
        new() { Key = "1", Title = "管理员" },
        new() { Key = "2", Title = "审计员" }
    };
    private string[] _targetKeys = new[] { "2" };

    private void HandleChange(TransferChangeArgs args)
    {
        _targetKeys = args.TargetKeys;
    }
}
```

## 2. 带搜索框

关键差异：带搜索框的穿梭框，可以自定义搜索函数。

```razor
<div>
    <Transfer DataSource="dataSource"
              TargetKeys="targetKeys.ToArray()"
              ShowSearch="true"
              SelectedKeys="selectedKeys"
              Titles="titles"
              OnChange="OnChange"
              OnSearch="OnSearch"
              OnScroll="OnScroll"
              OnSelectChange="OnSelectChange"></Transfer>
</div>

@code {
    private List<TransferItem> dataSource = new List<TransferItem>();
    private List<string> targetKeys = new List<string>();
    private string[] selectedKeys = Array.Empty<string>();
    private string[] titles = { "Source", "Target" };
    private bool Disabled = false;

    protected override void OnInitialized()
    {
        var random = new Random();
        for (int i = 0; i < 20; i++)
        {
            var data = new TransferItem
            {
                Key = i.ToString(),
                Title = $"Content{i + 1}",
                Description = $"description of content{i + 1}",
                ["Chosen"] = random.Next(0, 100) * 2 > 100
            };

            if (data["Chosen"] is bool _bool && _bool)
            {
                targetKeys.Add(data.Key);
            }

            dataSource.Add(data);
        }
        base.OnInitialized();
    }

    private OneOf.OneOf<string, RenderFragment> Render(TransferItem item)
    {
        //return new RenderFragment(buildTree);
        return $"{item.Title} - {item.Description}";
    }

    private void OnChange(TransferChangeArgs e)
    {
        Console.WriteLine(e.Direction);
        Console.WriteLine($"MoveKeys:{string.Join(',', e.MoveKeys)}");
        Console.WriteLine($"TargetKeys:{string.Join(',', e.TargetKeys)}");
    }

    private void OnSearch(TransferSearchArgs e)
    {
        Console.WriteLine(e.Direction);
        Console.WriteLine(e.Value);
    }

    private void OnScroll(TransferScrollArgs e)
    {
        Console.WriteLine(e.Direction);
    }

    private void OnSelectChange(TransferSelectChangeArgs e)
    {
        Console.WriteLine($"SourceSelectedKeys:{string.Join(',', e.SourceSelectedKeys)}");
        Console.WriteLine($"TargetSelectedKeys:{string.Join(',', e.TargetSelectedKeys)}");
    }
}
```

## 3. 自定义渲染行数据

关键差异：自定义渲染每一个 Transfer Item，可用于渲染复杂数据。

```razor
<div>
    <Transfer DataSource="dataSource"
              Disabled="Disabled"
              TargetKeys="targetKeys.ToArray()"
              SelectedKeys="selectedKeys"
              Titles="titles"
              OnChange="OnChange"
              OnScroll="OnScroll"
              OnSelectChange="OnSelectChange"
              Render="Render"
              Style="width:300px;height:300px;"></Transfer>
</div>

@code {
    private List<TransferItem> dataSource = new List<TransferItem>();
    private List<string> targetKeys = new List<string>();
    private string[] selectedKeys = Array.Empty<string>();
    private string[] titles = { "Source", "Target" };
    private bool Disabled = false;

    protected override void OnInitialized()
    {
        var random = new Random();
        for (int i = 0; i < 20; i++)
        {
            var data = new TransferItem
            {
                Key = i.ToString(),
                Title = $"Content{i + 1}",
                Description = $"description of content{i + 1}",
                ["Chosen"] = random.Next(0, 100) * 2 > 100
            };

            if (data["Chosen"] is bool _bool && _bool)
            {
                targetKeys.Add(data.Key);
            }

            dataSource.Add(data);
        }
        base.OnInitialized();
    }


    private OneOf.OneOf<string, RenderFragment> Render(TransferItem item)
    {
        //return new RenderFragment(buildTree);
        return $"{item.Title} - {item.Description}";
    }

    private void OnChange(TransferChangeArgs e)
    {
        Console.WriteLine(e.Direction);
        Console.WriteLine($"MoveKeys:{string.Join(',', e.MoveKeys)}");
        Console.WriteLine($"TargetKeys:{string.Join(',', e.TargetKeys)}");
    }

    private void OnScroll(TransferScrollArgs e)
    {
        Console.WriteLine(e.Direction);
    }

    private void OnSelectChange(TransferSelectChangeArgs e)
    {
        Console.WriteLine($"SourceSelectedKeys:{string.Join(',', e.SourceSelectedKeys)}");
        Console.WriteLine($"TargetSelectedKeys:{string.Join(',', e.TargetSelectedKeys)}");
    }
}
```

## 4. 高级用法

关键差异：穿梭框高级用法，可配置操作文案，可定制宽高，可对底部进行自定义渲染。

```razor
<div>
    <Transfer DataSource="dataSource"
              Disabled="Disabled"
              TargetKeys="targetKeys.ToArray()"
              SelectedKeys="selectedKeys"
              Titles="titles"
              Operations="operations"
              OnChange="OnChange"
              OnScroll="OnScroll"
              OnSelectChange="OnSelectChange"
              Render="Render"
              Style="width:300px;height:300px;">
        <FooterTemplate>
            <Button Type="ButtonType.Default" Size="ButtonSize.Small" Style="float: right; margin: 5px;">reload</Button>
        </FooterTemplate>

    </Transfer>
</div>

@code {
    private List<TransferItem> dataSource = new List<TransferItem>();
    private List<string> targetKeys = new List<string>();
    private string[] selectedKeys = Array.Empty<string>();
    private string[] titles = { "Source", "Target" };
    private string[] operations = { "to right", "to left" };
    private bool Disabled = false;

    protected override void OnInitialized()
    {
        var random = new Random();
        for (int i = 0; i < 20; i++)
        {
            var data = new TransferItem
            {
                Key = i.ToString(),
                Title = $"Content{i + 1}",
                Description = $"description of content{i + 1}",
                ["Chosen"] = random.Next(0, 100) * 2 > 100
            };

            if (data["Chosen"] is bool _bool && _bool)
            {
                targetKeys.Add(data.Key);
            }

            dataSource.Add(data);
        }
        base.OnInitialized();
    }

    private OneOf.OneOf<string, RenderFragment> Render(TransferItem item)
    {
        //return new RenderFragment(buildTree);
        return $"{item.Title} - {item.Description}";
    }

    private void OnChange(TransferChangeArgs e)
    {
        Console.WriteLine(e.Direction);
        Console.WriteLine($"MoveKeys:{string.Join(',', e.MoveKeys)}");
        Console.WriteLine($"TargetKeys:{string.Join(',', e.TargetKeys)}");
    }

    private void OnScroll(TransferScrollArgs e)
    {
        Console.WriteLine(e.Direction);
    }

    private void OnSelectChange(TransferSelectChangeArgs e)
    {
        Console.WriteLine($"SourceSelectedKeys:{string.Join(',', e.SourceSelectedKeys)}");
        Console.WriteLine($"TargetSelectedKeys:{string.Join(',', e.TargetSelectedKeys)}");
    }
}
```
