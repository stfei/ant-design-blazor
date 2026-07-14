# Ant Design Blazor Cascader 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 选择即改变 | 这种交互允许只选中父级选项。 |
| 可以自定义显示 | 切换按钮和结果分开。 |
| 移入展开 | 通过移入展开下级菜单，点击完成选择。 |
| 搜索 | 可以直接搜索选项并选择。 > `Cascader[showSearch]` 暂不支持服务端搜索，更多信息见 [#5547](https://github.com/ant-design/ant-design/issues/5547) |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Cascader Options="_options" @bind-Value="_value" />

@code {
    private string _value = string.Empty;
    private readonly List<CascaderNode> _options = new()
    {
        new()
        {
            Value = "zhejiang",
            Label = "浙江",
            Children = new[] { new CascaderNode { Value = "hangzhou", Label = "杭州" } }
        }
    };
}
```

## 2. 选择即改变

关键差异：这种交互允许只选中父级选项。

```razor
<div>
  <Cascader Options="@options" @bind-Value="value" ChangeOnSelect="true" SelectedNodesChanged="OnChange"></Cascader>
</div>

@code {
  string value = "112";
  void OnChange(CascaderNode[] selectedNodes)
  {
    Console.WriteLine($"value: {value} selected: {string.Join(",", selectedNodes.Select(x => x.Value))}");
  }

  List<CascaderNode> options = new List<CascaderNode>()
  {
      new()
      {
          Value = "zhejiang",
          Label = "Zhejiang",
          Children = new CascaderNode[]
          {
              new()
              {
                  Value = "hangzhou",
                  Label = "Hangzhou",
                  Children = new CascaderNode[]
                  {
                      new()
                      {
                          Value = "xihu",
                          Label = "West Lake",
                      },
                  }
              },
          },
      },
      new()
      {
          Value = "jiangsu",
          Label = "Jiangsu",
          Children = new CascaderNode[]
          {
              new()
              {
                  Value = "nanjing",
                  Label = "Nanjing",
                  Children = new CascaderNode[]
                  {
                      new()
                      {
                          Value = "zhonghuamen",
                          Label = "Zhong Hua Men",
                      },
                  }
              },
          },
      }
  };

}
```

## 3. 可以自定义显示

关键差异：切换按钮和结果分开。

```razor
<span>
  @text
  &nbsp;
  <Cascader Options="@options" SelectedNodesChanged="OnChange">
    <a>Change city</a>
  </Cascader>
</span>

@code {
    string text = "Unselect";

    void OnChange(CascaderNode[] selectedNodes)
    {
        text = string.Join(", ", selectedNodes.Select(x => x.Value));
    }

    List<CascaderNode> options = new List<CascaderNode>()
    {
      new()
      {
          Value = "zhejiang",
          Label = "Zhejiang",
          Children = new CascaderNode[]
          {
              new()
              {
                  Value = "hangzhou",
                  Label = "Hangzhou",
                  Children = new CascaderNode[]
                  {
                      new()
                      {
                          Value = "xihu",
                          Label = "West Lake",
                      },
                  }
              },
          },
      },
      new()
      {
          Value = "jiangsu",
          Label = "Jiangsu",
          Children = new CascaderNode[]
          {
              new()
              {
                  Value = "nanjing",
                  Label = "Nanjing",
                  Children = new CascaderNode[]
                  {
                      new()
                      {
                          Value = "zhonghuamen",
                          Label = "Zhong Hua Men",
                      },
                  }
              },
          },
      }
    };
}
```

## 4. 移入展开

关键差异：通过移入展开下级菜单，点击完成选择。

```razor
<div>
    <Cascader Options="@options" ExpandTrigger="hover"></Cascader>
</div>

@code {
  List<CascaderNode> options = new List<CascaderNode>()
  {
      new()
      {
          Value = "zhejiang",
          Label = "Zhejiang",
          Children = new CascaderNode[]
          {
              new()
              {
                  Value = "hangzhou",
                  Label = "Hangzhou",
                  Children = new CascaderNode[]
                  {
                      new()
                      {
                          Value = "xihu",
                          Label = "West Lake",
                      },
                  }
              },
          },
      },
      new()
      {
          Value = "jiangsu",
          Label = "Jiangsu",
          Children = new CascaderNode[]
          {
              new()
              {
                  Value = "nanjing",
                  Label = "Nanjing",
                  Children = new CascaderNode[]
                  {
                      new()
                      {
                          Value = "zhonghuamen",
                          Label = "Zhong Hua Men",
                      },
                  }
              },
          },
      }
  };
}
```

## 5. 搜索

关键差异：可以直接搜索选项并选择。 > `Cascader[showSearch]` 暂不支持服务端搜索，更多信息见 [#5547](https://github.com/ant-design/ant-design/issues/5547)

```razor
<Cascader Options="@options" Placeholder="Please select" ShowSearch />

@code {
  List<CascaderNode> options = new List<CascaderNode>()
  {
      new()
      {
          Value = "zhejiang",
          Label = "Zhejiang",
          Children = new CascaderNode[]
          {
              new()
              {
                  Value = "hangzhou",
                  Label = "Hangzhou",
                  Children = new CascaderNode[]
                  {
                      new()
                      {
                          Value = "xihu",
                          Label = "West Lake",
                      },
                      new()
                      {
                          Value = "xiasha",
                          Label = "Xia Sha",
                          Disabled = true,
                      },
                  }
              },
          },
      },
      new()
      {
          Value = "jiangsu",
          Label = "Jiangsu",
          Children = new CascaderNode[]
          {
              new()
              {
                  Value = "nanjing",
                  Label = "Nanjing",
                  Children = new CascaderNode[]
                  {
                      new()
                      {
                          Value = "zhonghuamen",
                          Label = "Zhong Hua Men",
                      },
                  }
              },
          },
      }
  };
}
```
