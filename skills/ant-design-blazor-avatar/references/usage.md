# Ant Design Blazor Avatar 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 类型 | 支持三种类型：图片、Icon 以及字符，其中 Icon 和字符型可以自定义图标颜色及背景色。 |
| Avatar Group | 头像组合展现。 |
| 带徽标的头像 | 通常用于消息提示。 |
| 自动调整字符大小 | 对于字符型的头像，当字符串较长时，字体大小可以根据头像宽度自动调整。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Avatar Icon="@IconType.Outline.User" />
<Avatar Shape="AvatarShape.Square" Size="AvatarSize.Large">AB</Avatar>
```

## 2. 类型

关键差异：支持三种类型：图片、Icon 以及字符，其中 Icon 和字符型可以自定义图标颜色及背景色。

```razor
<div>
    <Avatar Icon="user" />
    <Avatar>U</Avatar>
    <Avatar>USER</Avatar>
    <Avatar Src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png" />
    <Avatar Style="color: #f56a00; background-color: #fde3cf; ">U</Avatar>
    <Avatar Style="background-color: #87d068" Icon="user" />
</div>
```

## 3. Avatar Group

关键差异：头像组合展现。

```razor
<AvatarGroup>
    <Avatar Src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png" />
    <Avatar Style="background-color: #f56a00">K</Avatar>
    <Tooltip Title="Ant User" Placement="Placement.Top">
        <Unbound>
            <Avatar Style="background-color: #87d068;" Icon="user" RefBack="@context"/>
        </Unbound>
    </Tooltip>
    <Avatar Style="background-color: #1890ff;" Icon="ant-design" />
</AvatarGroup>
<Divider />
<AvatarGroup MaxCount="2" MaxStyle="color: #f56a00; background-color:#fde3cf;">
    <Avatar Src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png" />
    <Avatar Style="background-color: #f56a00">K</Avatar>
    <Tooltip Title="Ant User" Placement="Placement.Top" >
        <Unbound>
            <Avatar Style="background-color: #87d068;" Icon="user" RefBack="@context"/>
        </Unbound>
    </Tooltip>
    <Avatar Style="background-color: #1890ff;" Icon="ant-design" />
</AvatarGroup>
```

## 4. 带徽标的头像

关键差异：通常用于消息提示。

```razor
<div>
    <span class="avatar-item">
        <Badge Count="1">
            <Avatar Shape="AvatarShape.Square" Icon="@IconType.Outline.User" />
        </Badge>
    </span>
    <span>
        <Badge Dot>
            <Avatar Shape="AvatarShape.Square" Icon="@IconType.Outline.User" />
        </Badge>
    </span>
</div>
<style>
    /* tile uploaded pictures */
    .avatar-item {
        margin-right: 24px;
    }

    [class*='-col-rtl'] .avatar-item {
        margin-right: 0;
        margin-left: 24px;
    }
</style>
```

## 5. 自动调整字符大小

关键差异：对于字符型的头像，当字符串较长时，字体大小可以根据头像宽度自动调整。

```razor
<div>
    <Avatar Style="@($"background-color: {color}; vertical-align: middle;")" Size="AvatarSize.Large">
        @user
    </Avatar>
    <Button
        Size="ButtonSize.Small"
        Style="margin:0 16px; vertical-align: middle;"
        OnClick="_=>changeUser()"
    >
        Change
    </Button>
</div>

@code
{
    private static string[] userList = {"U", "Lucy", "Tom", "Edward"};
    private static string[] colorList = {"#f56a00", "#7265e6", "#ffbf00", "#00a2ae"};

    private string user { get; set; } = userList[0];
    private string color { get; set; } = colorList[0];

    private void changeUser()
    {
        var index = Array.IndexOf(userList, user);
        user = index < userList.Length - 1 ? userList[index + 1] : userList[0];
        color = index < colorList.Length - 1 ? colorList[index + 1] : colorList[0];
    }
}
```
