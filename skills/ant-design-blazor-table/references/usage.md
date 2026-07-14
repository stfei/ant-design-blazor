# Ant Design Blazor Table 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 固定表头 | 设置 `ScrollY` 限制表体高度；为列指定 `Width`，避免表头与内容错位。 |
| 固定列 | 设置 `ScrollX`，再用列的 `Fixed` 与 `Width` 固定左右列。 |
| 多列排序 | `Column` 支持 `SorterMultiple` 字段以配置多列排序优先级。通过 `SorterCompare` 配置排序逻辑，你可以通过不设置该函数只启动多列排序的交互形式。 |
| 单元格自动省略 | 设置列的 `Ellipsis`，让超出宽度的单元格内容自动省略。 |
| 动态数据 | 从动态的 Json 数据生成表格 |
| DataTable 支持 | 对于使用 ADO.NET 查询数据库的场景，也可以将 `DataTable` 作为数据源。 |
| 虚拟化 | 设置 `EnableVirtualization` 与 `ScrollY` 启用虚拟化，在大数据量下提高性能（需要.NET 5或更高版本框架）。 启用 `RemoteDataSource` 即变为按需加载。 |
| 自定义排序 | 通过 `SorterCompare` 提供字段比较函数，覆盖默认排序规则。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Table TItem="UserRow" DataSource="_rows">
    <PropertyColumn Property="x => x.Name" Title="姓名" />
    <PropertyColumn Property="x => x.Age" Title="年龄" Sortable />
    <ActionColumn Title="操作">
        <Button Type="ButtonType.Link" OnClick="() => Edit(context)">编辑</Button>
    </ActionColumn>
</Table>

@code {
    private readonly UserRow[] _rows = new[] { new UserRow("张三", 28), new UserRow("李四", 35) };
    private void Edit(UserRow row) { }
    private sealed record UserRow(string Name, int Age);
}
```

## 2. 固定表头

关键差异：设置 `ScrollY` 限制表体高度；为列指定 `Width`，避免表头与内容错位。

```razor
<Table DataSource="data" ScrollY="240px" PageSize="50">
    <Selection />
    <PropertyColumn Property="c=>c.Name" Width="150" />
    <PropertyColumn Property="c=>c.Age"  Width="150" />
    <PropertyColumn Property="c=>c.Address" />
</Table>

@code {
    class Column
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }

    Column[] data = Enumerable.Range(0, 100).Select(i => new Column()
    {
        Name = $"Edward King {i}",
        Age = 32,
        Address = $"Edward King {i}"
    }).ToArray();

}
```

## 3. 固定列

关键差异：设置 `ScrollX`，再用列的 `Fixed` 与 `Width` 固定左右列。

```razor
@using System.ComponentModel
<Table DataSource="data" PageSize="50" ScrollX="1300">
    <PropertyColumn Property="c=>c.Name" Width="100" Fixed="ColumnFixPlacement.Left" />
    <PropertyColumn Property="c=>c.Age" Width="100" Fixed="ColumnFixPlacement.Left" />
    <PropertyColumn Title="Column 1" Property="c=>c.Address" />
    <PropertyColumn Title="Column 2" Property="c=>c.Address" />
    <PropertyColumn Title="Column 3" Property="c=>c.Address" />
    <PropertyColumn Title="Column 4" Property="c=>c.Address" />
    <PropertyColumn Title="Column 5" Property="c=>c.Address" />
    <PropertyColumn Title="Column 6" Property="c=>c.Address" />
    <PropertyColumn Title="Column 7" Property="c=>c.Address" />
    <PropertyColumn Title="Column 8" Property="c=>c.Address" />
    <ActionColumn Title="Action" Width="100" Fixed="ColumnFixPlacement.Right">
        <a>action</a>
    </ActionColumn>
</Table>

@code {
    class Column
    {
        [DisplayName("Full Name")]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }

    Column[] data = new Column[] {
        new()
        {
            Name = "John Brown",
            Age = 32,
            Address = "New York Park"
        },
        new()
        {
            Name = "Jim Green",
            Age = 40,
            Address = "London Park"
        },
    };

}
```

## 4. 多列排序

关键差异：`Column` 支持 `SorterMultiple` 字段以配置多列排序优先级。通过 `SorterCompare` 配置排序逻辑，你可以通过不设置该函数只启动多列排序的交互形式。

```razor
<Table DataSource="data" OnChange="OnChange" TItem="Data">
    <Column Title="Name" DataIndex="name" TData="string" />
    <Column Title="Chinese Score" DataIndex="chinese" TData="int" SorterMultiple="3" SorterCompare="(a,b)=>a-b" />
    <Column Title="Math Score" DataIndex="math" TData="int" SorterMultiple="2" SorterCompare="(a,b)=>a-b" />
    <Column Title="English Score" DataIndex="english" TData="int" SorterMultiple="1" SorterCompare="(a,b)=>a-b" />
</Table>

@using AntDesign.TableModels;
@using System.Text.Json;
@code {

    public record Data(string Name, int Chinese, int Math, int English);

    public Data[] data =
    {
        new("John Brown",98,60,70),
        new("Jim Green",98,66,89),
        new("Joe Black",98,90,70),
        new("Jim Red",88,99,89),
    };

    void OnChange(QueryModel<Data> query)
    {
        Console.WriteLine(JsonSerializer.Serialize(query));
    }
}
```

## 5. 单元格自动省略

关键差异：设置 `Eolumn.Ellipsis` 可以让单元格内容根据宽度自动省略。 > 列头缩略暂不支持和排序筛选一起使用。

```razor
<Table DataSource="data">
    <PropertyColumn Property="c=>c.Name" Width="150">
        <a>@context.Name</a>
    </PropertyColumn>
    <PropertyColumn Property="c=>c.Age" Width="80" />
    <PropertyColumn Property="c=>c.Address" Ellipsis />
    <PropertyColumn Property="c=>c.Address" Ellipsis Title="Long Column Long Column Long Column" />
    <PropertyColumn Property="c=>c.Address" EllipsisShowTitle="false" Title="Long Column Long Column" >
        <Tooltip Placement="Placement.TopLeft" Title="@context.Address" Style="display: inline;">
            @context.Address
        </Tooltip>
    </PropertyColumn>
    <PropertyColumn Property="c=>c.Address" Ellipsis Title="Long Column" />
</Table>

@code {
    Column[] data = new Column[] {
        new()
        {
            Name = "John Brown",
            Age = 32,
            Address = "New York No. 1 Lake Park, New York No. 1 Lake Park"
        },
        new()
        {
            Name = "Jim Green",
            Age = 42,
            Address = "London No. 2 Lake Park, London No. 2 Lake Park"
        },
        new()
        {
            Name = "Joe Black",
            Age = 32,
            Address = "Sidney No. 1 Lake Park, Sidney No. 1 Lake Park"
        },
    };

    class Column
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }

}
```

## 6. 动态数据

关键差异：从动态的 Json 数据生成表格

```razor
@using System.ComponentModel
@using AntDesign.Core
@using AntDesign.TableModels
@using System.Text.Json.Serialization

<Table TItem="Dictionary<string, object>" OnChange="HanleChange" DataSource="@data" Loading="data==null" ScrollX="1500"
    PageSize="5" Size="TableSize.Small">
    @foreach (var key in data?.FirstOrDefault()?.Keys.Take(10) ?? new string[0])
    {
        <PropertyColumn Filterable Sortable Property=@(c=>c[key]) Title="@key" />
    }
</Table>

@inject HttpClient httpClient;
@code {

    List<Dictionary<string, object>> data;

    string githubUrl = "https://api.github.com/repos/ant-design-blazor/ant-design-blazor/contributors?per_page=200";

    protected async Task HanleChange(QueryModel<Dictionary<string, object>> query)
    {
        try
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DictionaryConverter());

            data = await httpClient.GetFromJsonAsync<List<Dictionary<string, object>>>(githubUrl, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
```

## 7. DataTable 支持

关键差异：对于使用 ADO.NET 查询数据库的场景，也可以将 `DataTable` 作为数据源。

```razor
@using System.Data;

<Table DataSource="data">
    @foreach (DataColumn column in _dataTable.Columns)
    {
        <PropertyColumn
            Property="c=>c.Field<object>(column.ColumnName)"
            Title="@column.ColumnName"
            Sortable
            Filterable />
    }
</Table>

@code {
    DataTable _dataTable = new DataTable();

    IEnumerable<DataRow> data => _dataTable?.AsEnumerable();

    protected override void OnInitialized()
    {
        _dataTable.Columns.Add("Name");
        _dataTable.Columns.Add("Age", typeof(int));
        _dataTable.Columns.Add("Address");
        _dataTable.Columns.Add("CreateAt", typeof(DateTime));

        _dataTable.Rows.Add("John Brown", 32, "New York No. 1 Lake Park", DateTime.Now);
        _dataTable.Rows.Add("Jim Green", 42, "London No. 1 Lake Park", DateTime.Now);
        _dataTable.Rows.Add(null, 32, "Sidney No. 1 Lake Park", DateTime.Now);

        base.OnInitialized();
    }
}
```

## 8. 虚拟化

关键差异：设置 `EnableVirtualization` 与 `ScrollY` 启用虚拟化，在大数据量下提高性能（需要.NET 5或更高版本框架）。 启用 `RemoteDataSource` 即变为按需加载。

```razor
<Divider Orientation="DividerOrientation.Left">Load all data</Divider>

<Space>
    <SpaceItem>
        <Button OnClick="LoadData">Load data</Button>
    </SpaceItem>
</Space>

<Table TItem="Data"
       DataSource="data1"
       ScrollY="300px"
       EnableVirtualization
       HidePagination
       Bordered>
    <Selection />
    <PropertyColumn Property="c=>c.Name" Width="400" />
    <PropertyColumn Property="c=>c.Age" Width="150" />
    <PropertyColumn Property="c=>c.Address" />
</Table>

<Divider Orientation="DividerOrientation.Left">Load on demand</Divider>

<Table TItem="Data"
       DataSource="data2"
       ScrollY="300px"
       EnableVirtualization
       OnChange="OnChange"
       Total="total"
       RemoteDataSource
       Bordered
       RowKey="x=>x.Name">
    <Selection />
    <PropertyColumn Property="c=>c.Name" Width="400" />
    <PropertyColumn Property="c=>c.Age" Width="150" />
    <PropertyColumn Property="c=>c.Address" />
</Table>

@code {
    class Data
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }

    Data[] data1 = Array.Empty<Data>();
    Data[] data2 = Array.Empty<Data>();

    int total = 0;

    void LoadData()
    {
        data1 = Enumerable.Range(0, 10000).Select(i => new Data()
        {
            Name = $"Edward King {i}",
            Age = 32,
            Address = $"Edward King {i}'s address"
        }).ToArray();
    }


    async Task OnChange(TableModels.QueryModel queryModel)
    {
        await Task.Delay(1000);
        data2 = Enumerable.Range(queryModel.StartIndex, queryModel.PageSize).Select(i => new Data
            {
                Name = $"Edward King {i}",
                Age = 32,
                Address = $"Edward King {i}'s address"
            }).ToArray();

        total = 10000;
    }
}
```

## 9. 自定义排序

关键差异：通过 `SorterCompare` 提供字段比较函数，覆盖默认排序规则。

```razor
<Table @ref="table" TItem="TestData" DataSource="@testData">
    <PropertyColumn Property="c=>c.Id" Sortable/>
    <PropertyColumn Title="Default sort" Property="c=>c.DayOfWeek" Sortable />
    <PropertyColumn Title="Custom comparer" Property="c=>c.DayOfWeek" Sortable SorterCompare="@((v1, v2) => v1 - v2)"/>
    <PropertyColumn Title="Custom comparer(order by DayName)" Property="c=>c.DayOfWeek" Sortable SorterCompare="@((v1, v2) => string.CompareOrdinal(DayName[v1], DayName[v2]))" />
    <PropertyColumn Title="DayName" Field="DayName[context.DayOfWeek]"/>
</Table>

@code {

    ITable table;

    TestData[] testData;

    public class TestData
    {
        public int Id { get; set; }

        public int DayOfWeek { get; set; }
    }

    public string[] DayName = {"None", "Monday", "Tuesday", "Wednesdays", "Thursday", "Friday", "Saturday", "Sunday"};

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        testData = GetForecastAsync();
        base.OnInitialized();
    }

    public TestData[] GetForecastAsync()
    {
        var rng = new Random();
        return Enumerable.Range(0, 5).Select(index => new TestData
        {
            Id = index,
            DayOfWeek = rng.Next(1, 8),
        }).ToArray();
    }

}
```
