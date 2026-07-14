# Ant Design Blazor Result 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| Success | 成功的结果。 |
| Error | 复杂的错误反馈。 |
| 403 | 你没有此页面的访问权限。 |
| 自定义 icon | 自定义 icon。 |
| Warning | 警告类型的结果。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Result Status="ResultStatus.Success"
        Title="提交成功"
        SubTitle="系统已收到你的申请。">
    <Extra>
        <Button Type="ButtonType.Primary">返回列表</Button>
    </Extra>
</Result>
```

## 2. Success

关键差异：成功的结果。

```razor
<Result Status="ResultStatus.Success"
        Title="Successfully Purchased Cloud Server ECS!"
        SubTitle="Order number: 2017182818828182881 Cloud server configuration takes 1-5 minutes, please wait.">
    <Extra>
        <Button Type="ButtonType.Primary">Go Console</Button>
        <Button>Buy Again</Button>
    </Extra>

</Result>
```

## 3. Error

关键差异：复杂的错误反馈。

```razor
<Result
    Status="ResultStatus.Error"
    Title="Submission Failed"
    SubTitle="Please check and modify the following information before resubmitting."
    Extra=extra>
    <div class="desc">
        <Paragraph>
            <Text Strong Style="font-size: 16px;">
                The content you submitted has the following error:
            </Text>
        </Paragraph>
        <Paragraph>
            <Icon Type="@IconType.Outline.CloseCircle" Class="site-result-demo-error-icon" /> Your account has been frozen
            <a>Thaw immediately &gt;</a>
        </Paragraph>
        <Paragraph>
            <Icon Type="@IconType.Outline.CloseCircle" Class="site-result-demo-error-icon" /> Your account is not yet
            eligible to apply <a>Apply Unlock &gt;</a>
        </Paragraph>
    </div>
</Result>

@code {
    RenderFragment extra =
        @<Template>
            <Button Type="ButtonType.Primary">Go Console</Button>
            <Button>Buy Again</Button>
        </Template>
        ;
}
```

## 4. 403

关键差异：你没有此页面的访问权限。

```razor
<Result Status="ResultStatus.Http403"
        Title="403"
        SubTitle="Sorry, you are not authorized to access this page."
        Extra="extra" />

@code
{
    RenderFragment extra = @<Button Type="ButtonType.Primary">Back Home</Button>;
}
```

## 5. 自定义 icon

关键差异：自定义 icon。

```razor
<Result Icon="smile-outline"
        Title="Great, we have done all the operations!"
        Extra="extra">
</Result>
<Divider></Divider>
<Result IsShowIcon="false"
        Title="Great, we can hide the icon!"
        Extra="extra">
</Result>

@code
{
    RenderFragment extra =@<Button Type="ButtonType.Primary">Next</Button>;
}
```

## 6. Warning

关键差异：警告类型的结果。

```razor
<Result Status="ResultStatus.Warning"
        Title="There are some problems with your operation."
        Extra=extra />
@code
{
    RenderFragment extra = @<Button Type="ButtonType.Primary">Go Console</Button>;
}
```
