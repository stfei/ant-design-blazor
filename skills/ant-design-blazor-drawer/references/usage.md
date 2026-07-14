# Ant Design Blazor Drawer 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 多层抽屉 | 在抽屉内打开新的抽屉，用以解决多分支任务的复杂状况。 |
| 自定义位置 | 自定义位置，点击触发按钮抽屉从相应的位置滑出，点击遮罩区关闭 |
| OffsetX(Y) | 设置X或Y方向偏移。 |
| 抽屉表单 | 在抽屉中使用表单。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Button Type="ButtonType.Primary" OnClick="() => _visible = true">打开抽屉</Button>

<Drawer Title="@("订单详情")" @bind-Visible="_visible" Placement="DrawerPlacement.Right">
    <p>抽屉内容</p>
</Drawer>

@code {
    private bool _visible;
}
```

## 2. 多层抽屉

关键差异：在抽屉内打开新的抽屉，用以解决多分支任务的复杂状况。

```razor
<div>
    <Button Type="ButtonType.Primary" OnClick="_=>open()">Open</Button>

    <Drawer Width="@wdFirstLayer" Closable="true" Visible="visible1" Title='("Multi-level drawer")' OnClose="_=>close()">
        <Button Type="ButtonType.Primary" OnClick="_=>ShowDrawer()">Two-level Drawer</Button>
        <Drawer Width="260" Closable="true" Visible="visible2" Title='("two-level drawer")' OnClose="_=>CloseDrawer()">
            <Button Type="ButtonType.Primary">This is two-level drawer</Button>
        </Drawer>

    </Drawer>
</div>

   @code{

       bool visible1 = false;
       bool visible2 = false;
        string wdFirstLayer = "520";

        void open()
        {
            this.visible1 = true;
        }

        void ShowDrawer()
        {

            this.visible2 = true;
            wdFirstLayer = $"{int.Parse(wdFirstLayer) + 260}";
        }

        void close()
        {
            this.visible1 = false;
        }

        void CloseDrawer()
        {
            wdFirstLayer = $"{int.Parse(wdFirstLayer) - 260}";
            this.visible2 = false;
        }

}
```

## 3. 自定义位置

关键差异：自定义位置，点击触发按钮抽屉从相应的位置滑出，点击遮罩区关闭

```razor
<div>
    <RadioGroup @bind-Value="placement">
        <Radio Value="DrawerPlacement.Top">Top</Radio>
        <Radio Value="DrawerPlacement.Right">Right</Radio>
        <Radio Value="DrawerPlacement.Bottom">Bottom</Radio>
        <Radio Value="DrawerPlacement.Left">Left</Radio>
    </RadioGroup>

    <Button Type="ButtonType.Primary" OnClick="open">Open</Button>
    <Drawer Visible="@visible" Placement="@placement" Title='("Basic Drawer")' OnClose="close">
        <p>Some contents...</p>
        <p>Some contents...</p>
        <p>Some contents...</p>
    </Drawer>
</div>

@code{

    DrawerPlacement placement = DrawerPlacement.Right;

    bool visible = false;

    void open()
    {
        this.visible = true;
    }

    void close()
    {
        this.visible = false;
    }
}
```

## 4. OffsetX(Y)

关键差异：设置X或Y方向偏移。

```razor
<div>
    <RadioGroup @bind-Value="placement">
        <Radio Value="DrawerPlacement.Top">Top</Radio>
        <Radio Value="DrawerPlacement.Right">Right</Radio>
        <Radio Value="DrawerPlacement.Bottom">Bottom</Radio>
        <Radio Value="DrawerPlacement.Left">Left</Radio>
    </RadioGroup>

    <Button Type="ButtonType.Primary" OnClick="open">Open</Button>
    <Drawer OffsetX="200" OffsetY="100" Visible="@visible" Placement="placement" Title='("Basic Drawer")' OnClose="close">
        <p>Some contents...</p>
        <p>Some contents...</p>
        <p>Some contents...</p>
    </Drawer>
</div>

@code{

    DrawerPlacement placement = DrawerPlacement.Right;

    bool visible = false;

    void open()
    {
        this.visible = true;
    }

    void close()
    {
        this.visible = false;
    }
}
```

## 5. 抽屉表单

关键差异：在抽屉中使用表单。

```razor
<div>
    <Button Icon="@IconType.Outline.Plus" Type="ButtonType.Primary" @onclick="_=>open()">New account</Button>
    <Drawer Closable="true" Width="720" Visible="visible" Title='("Submit from in Drawer")' OnClose="_=>close()">
        <Template style="height:90%">
            <Row Gutter="16">
                <AntDesign.Col Span="12">
                    <Text>Name</Text>
                    <Input Placeholder="Please enter user name" TValue="string"></Input>
                </AntDesign.Col>
                <AntDesign.Col Span="12">
                    <Text>Url</Text>
                    <AntDesign.Input Placeholder="please enter url" TValue="string">
                        <AddOnBefore>Http://</AddOnBefore>
                        <AddOnAfter>.Com</AddOnAfter>
                    </AntDesign.Input>
                </AntDesign.Col>
            </Row>
            <br />
            <Row Gutter="16">
                <AntDesign.Col Span="12">
                    <Text>Owner</Text>
                    <Input Placeholder="Please select a owner" TValue="string" />
                </AntDesign.Col>
                <AntDesign.Col Span="12">
                    <Text>Type</Text>
                    <Input Placeholder="please enter url" TValue="string" />
                </AntDesign.Col>
            </Row>
            <br />

            <Row>
                <AntDesign.Col Span="24">
                    <Text>Description</Text>
                    <TextArea Placeholder="Please enter your description">

                    </TextArea>
                </AntDesign.Col>
            </Row>
            <br />
            <Row>
                <AntDesign.Col Span="18">

                </AntDesign.Col>
                <AntDesign.Col Span="6">
                    <Button Type="ButtonType.Default">Cancel</Button>
                    <Button Type="ButtonType.Primary">Submit</Button>
                </AntDesign.Col>
            </Row>
        </Template>
    </Drawer>
</div>


@code{
    bool visible = false;

    void open()
    {
        this.visible = true;
    }

    void close()
    {
        this.visible = false;
    }
}
```
