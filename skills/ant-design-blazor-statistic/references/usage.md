# Ant Design Blazor Statistic 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 倒计时 | 倒计时组件。 |
| 单位 | 通过前缀和后缀添加单位。 |
| 在卡片中使用 | 在卡片中展示统计数值。 |
| 数字分隔符 | 指定数字分隔符。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Statistic Title="活跃用户" Value="112893" />
<Statistic Title="账户余额" Value="12345.67" Precision="2" Prefix="¥" />
```

## 2. 倒计时

关键差异：倒计时组件。

```razor
<Row Gutter="16">
    <Col Span="12">
        <CountDown Title="Countdown" Value="@deadline" OnFinish="OnFinish" />
    </Col>
    <Col Span="12">
        <CountDown Title="Million" Value="@deadline" Format="hh:mm:ss:fff" />
    </Col>
    <Col Span="24">
        <CountDown Title="Day Level" Value="@deadline" Format="dd 天  h 小时 m 分钟 s 秒" />
    </Col>
    <br/>
    <Button OnClick="OnReset">Reset Value</Button>
</Row>

@code
{
    DateTime deadline = DateTime.Now.AddMilliseconds(1000 * 60 * 60 * 24 * 2 + 1000 * 30);

    void OnFinish()
    {
        Console.WriteLine("finished!");
    }

    void OnReset()
    {
        deadline = DateTime.Now.AddMinutes(10);
    }
}
```

## 3. 单位

关键差异：通过前缀和后缀添加单位。

```razor
<Row Gutter=16>
    <Col Span=12>
    <Statistic Title="Feedback" Value="1128" PrefixTemplate="@prefix1" />
    </Col>
    <Col Span=12>
    <Statistic Title="Unmerged" Value="93" Suffix="/ 100" />
    </Col>
</Row>


@code
{
    RenderFragment prefix1 =@<Icon Type="@IconType.Outline.Like" />;
}
```

## 4. 在卡片中使用

关键差异：在卡片中展示统计数值。

```razor
<div class="site-statistic-demo-card">
    <Row Gutter="16">
        <Col Span="12">
        <Card>
            <Statistic Title="Active" Value="11.28" Precision="2" ValueStyle="color: #3f8600;" Suffix="%">
                <PrefixTemplate>
                    <span><Icon Type="@IconType.Outline.ArrowUp" /></span>
                </PrefixTemplate>
            </Statistic>
        </Card>
        </Col>
        <Col Span="12">
        <Card>
            <Statistic Title="Idle" Value="9.3" Precision="2" ValueStyle="color: #cf1322;" Suffix="%">
                <PrefixTemplate>
                    <span><Icon Type="@IconType.Outline.ArrowDown" /></span>
                </PrefixTemplate>
            </Statistic>
        </Card>
        </Col>
    </Row>
</div>

<style>
    .site-statistic-demo-card {
        background: #ececec;
        padding: 30px;
    }
</style>
```

## 5. 数字分隔符

关键差异：指定数字分隔符。

```razor
<Row>
    <Statistic Title="Number" Value="123456" GroupSeparator=" " Style="text-align: center;" />
    <Statistic Title="Decimal" Value="50.5" DecimalSeparator=" " Style="margin: 0 32px; text-align: center;" />
</Row>
```
