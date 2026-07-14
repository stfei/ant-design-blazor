# Ant Design Blazor Affix 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 固定状态改变的回调 | 可以获得是否固定的状态。 |
| 滚动容器 | 用 `TargetId` 设置 `Affix` 需要监听其滚动事件的元素，默认为 `window`。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Affix OffsetTop="16">
    <Button Type="ButtonType.Primary">固定操作</Button>
</Affix>
```

## 2. 固定状态改变的回调

关键差异：可以获得是否固定的状态。

```razor
<div>
<Affix OffsetTop="120" OnChange="OnAffixChange">
	<Button>
		120px to affix top
	</Button>
</Affix>
</div>

@code{
        private void OnAffixChange(bool affixed)
        {
            Console.WriteLine(affixed);
        }

}
```

## 3. 滚动容器

关键差异：用 `TargetId` 设置 `Affix` 需要监听其滚动事件的元素，默认为 `window`。

```razor
<div class="scrollable-container" id="scrollable-container">
	<div class="background">
		<Affix TargetSelector="#scrollable-container">
			<Button Type="ButtonType.Primary">
				Fixed at the top of container
			</Button>
		</Affix>
	</div>
</div>

<style>
    .scrollable-container {
        height: 100px;
        overflow-y: scroll;
    }

    .background {
        padding-top: 60px;
        height: 300px;
        background-image: url("https://zos.alipayobjects.com/rmsportal/RmjwQiJorKyobvI.jpg")
    }
</style>
```
