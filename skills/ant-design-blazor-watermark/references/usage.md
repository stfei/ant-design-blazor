# Ant Design Blazor Watermark 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 多行水印 | 通过 `content` 设置 字符串数组 指定多行文字水印内容。 |
| 图片水印 | 通过 `image` 指定图片地址。为保证图片高清且不被拉伸，请设置 width 和 height, 并上传至少两倍的宽高的 logo 图片地址。 |
| 滚动文字 | 实现无限滚动文字效果。 |
| 自定义配置 | 通过自定义参数配置预览水印效果。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Watermark Content="内部资料" Rotate="-22">
    <div style="height: 320px; padding: 24px">
        需要添加水印的内容
    </div>
</Watermark>
```

## 2. 多行水印

关键差异：通过 `content` 设置 字符串数组 指定多行文字水印内容。

```razor
<Watermark Contents="@(new[]{"Ant Design", "Happy Working"})">
    <div style="height: 500px" />
  </Watermark>
```

## 3. 图片水印

关键差异：通过 `image` 指定图片地址。为保证图片高清且不被拉伸，请设置 width 和 height, 并上传至少两倍的宽高的 logo 图片地址。

```razor
<Watermark Height="30"
           Width="130"
           Image="https://mdn.alipayobjects.com/huamei_7uahnr/afts/img/A*lkAoRbywo0oAAAAAAAAAAAAADrJ8AQ/origina">
    <div style="height: 500px" />
</Watermark>
```

## 4. 滚动文字

关键差异：实现无限滚动文字效果。

```razor
<Watermark Content="Ant Design" Scrolling>
    <div style="height: 500px" />
</Watermark>
```

## 5. 自定义配置

关键差异：通过自定义参数配置预览水印效果。

```razor
<div>
    <Watermark Content="Ant Design" FontColor="rgba(0, 0, 0, 0.15)" FontSize="16" ZIndex="11" Rotate="-22" Gap="(100,100)">

        <Paragraph>
            The light-speed iteration of the digital world makes products more complex. However,
            human consciousness and attention resources are limited. Facing this design
            contradiction, the pursuit of natural interaction will be the consistent direction of
            Ant Design.
        </Paragraph>
        <Paragraph>
            Natural user cognition: According to cognitive psychology, about 80% of external
            information is obtained through visual channels. The most important visual elements in
            the interface design, including layout, colors, illustrations, icons, etc., should fully
            absorb the laws of nature, thereby reducing the user&apos;s cognitive cost and bringing
            authentic and smooth feelings. In some scenarios, opportunely adding other sensory
            channels such as hearing, touch can create a richer and more natural product experience.
        </Paragraph>
        <Paragraph>
            Natural user behavior: In the interaction with the system, the designer should fully
            understand the relationship between users, system roles, and task objectives, and also
            contextually organize system functions and services. At the same time, a series of
            methods such as behavior analysis, artificial intelligence and sensors could be applied
            to assist users to make effective decisions and reduce extra operations of users, to
            save users&apos; mental and physical resources and make human-computer interaction more
            natural.
        </Paragraph>
        <img style="
            z-index: 10;
            width: 100%;
            max-width: 800px;
            position: relative;
          "
             src="https://gw.alipayobjects.com/mdn/rms_08e378/afts/img/A*zx7LTI_ECSAAAAAAAAAAAABkARQnAQ"
             alt="示例图片" />
    </Watermark>

</div>

@code {

            }
```
