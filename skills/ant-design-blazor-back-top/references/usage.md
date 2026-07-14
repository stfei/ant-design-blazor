# Ant Design Blazor BackTop 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 自定义样式 | 可以自定义回到顶部按钮的样式，限制宽高：`40px * 40px`。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<div style="min-height: 1200px">
    向下滚动后显示返回顶部按钮
</div>
<BackTop />
```

## 2. 自定义样式

关键差异：可以自定义回到顶部按钮的样式，限制宽高：`40px * 40px`。

```razor
<div style="height:600vh;padding:8px;">
    <div>Scroll to bottom</div>
    <div>Scroll to bottom</div>
    <div>Scroll to bottom</div>
    <div>Scroll to bottom</div>
    <div>Scroll to bottom</div>
    <div>Scroll to bottom</div>
    <div>Scroll to bottom</div>
    <BackTop Style="margin-bottom:60px" OnClick="OnClick">
        <div class="ant-back-top-inner">UP</div>
    </BackTop>
</div>

<style>

    .ant-back-top-inner {
        height: 40px;
        width: 40px;
        line-height: 40px;
        border-radius: 4px;
        background-color: #1088e9;
        color: #fff;
        text-align: center;
        font-size: 20px;
    }
</style>

@code{
    private void OnClick()
    {
        Console.WriteLine("Top");
    }

}
```
