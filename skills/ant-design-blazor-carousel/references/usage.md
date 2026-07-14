# Ant Design Blazor Carousel 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 自动切换 | 定时切换下一张。 |
| 渐显 | 切换效果为渐显。 |
| 位置 | 位置有 4 个方向。 |
| 自定义按钮 | 用户提供用于导航的自定义按钮 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Carousel Autoplay="TimeSpan.FromSeconds(3)">
    <CarouselSlick><div>第一页</div></CarouselSlick>
    <CarouselSlick><div>第二页</div></CarouselSlick>
    <CarouselSlick><div>第三页</div></CarouselSlick>
</Carousel>
```

## 2. 自动切换

关键差异：定时切换下一张。

```razor
<div>
    <Carousel Autoplay="TimeSpan.FromSeconds(2)">
        <CarouselSlick>
            <h3>1</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>2</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>3</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>4</h3>
        </CarouselSlick>
    </Carousel>
</div>
```

## 3. 渐显

关键差异：切换效果为渐显。

```razor
<div>
    <Carousel Effect="CarouselEffect.Fade">
        <CarouselSlick>
            <h3>1</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>2</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>3</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>4</h3>
        </CarouselSlick>
    </Carousel>
</div>
```

## 4. 位置

关键差异：位置有 4 个方向。

```razor
<div>
    <RadioGroup @bind-Value="_position" Style="margin-bottom: 8px;">
        <Radio RadioButton Value="CarouselDotPosition.Top">Top</Radio>
        <Radio RadioButton Value="CarouselDotPosition.Bottom">Bottom</Radio>
        <Radio RadioButton Value="CarouselDotPosition.Left">Left</Radio>
        <Radio RadioButton Value="CarouselDotPosition.Right">Right</Radio>
    </RadioGroup>
    <Carousel DotPosition="_position">
        <CarouselSlick>
            <h3>1</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>2</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>3</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>4</h3>
        </CarouselSlick>
    </Carousel>
</div>

@code{
    private CarouselDotPosition _position = CarouselDotPosition.Bottom;
}
```

## 5. 自定义按钮

关键差异：用户提供用于导航的自定义按钮

```razor
<div>
    <Carousel @ref="_carousel">
        <CarouselSlick>
            <h3>1</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>2</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>3</h3>
        </CarouselSlick>
        <CarouselSlick>
            <h3>4</h3>
        </CarouselSlick>
    </Carousel>
</div>
<div class="button-container">
    <Button OnClick="@( _=>_carousel.Previous())" Size="ButtonSize.Small">Prev</Button>
    <div>
        @foreach (int i in Enumerable.Range(0, 4))
        {
            <Button OnClick="@( _=>_carousel.GoTo(i))" Size="ButtonSize.Small">@(i+1)</Button>
        }
    </div>
    <Button OnClick="@( _=>_carousel.Next())" Size="ButtonSize.Small">Next</Button>

</div>

<style>
    .button-container {
        display: flex;
        justify-content: space-between;
        padding-top:4px;
    }

    .ant-carousel .slick-slide {
        text-align: center;
        height: 160px;
        line-height: 160px;
        background: #364d79;
        overflow: hidden;
    }

    .ant-carousel .slick-slide h3 {
        color: #fff;
    }
</style>

@code{
    Carousel _carousel;
}
```
