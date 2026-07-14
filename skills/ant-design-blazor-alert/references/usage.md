# Ant Design Blazor Alert 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 含有辅助性文字介绍 | 含有辅助性文字介绍的警告提示。 |
| 可关闭的警告提示 | 显示关闭按钮，点击可关闭警告提示。 |
| 顶部公告 | 页面顶部通告形式，默认有图标且 `Type` 为 `'warning'`。 |
| 图标 | 可口的图标让信息类型更加醒目。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Alert Type="AlertType.Success"
       Message="保存成功"
       Description="数据已经更新。"
       ShowIcon="true"
       Closable />
```

## 2. 含有辅助性文字介绍

关键差异：含有辅助性文字介绍的警告提示。

```razor
<Alert Message="Success Text"
          Description="Success Description Success Description Success Description"
          Type="AlertType.Success" />

<Alert Message="Info Text"
          Type="AlertType.Info">
    Info Description Info Description Info Description Info Description
</Alert>

<Alert Message="Warning Text"
          Description="Warning Description Warning Description Warning Description Warning Description"
          Type="AlertType.Warning" />

<Alert Message="Error Text"
          Description="Error Description Error Description Error Description Error Description"
          Type="AlertType.Error" />
```

## 3. 可关闭的警告提示

关键差异：显示关闭按钮，点击可关闭警告提示。

```razor
<Alert Type="AlertType.Warning"
          Message="Warning Text Warning Text Warning Text Warning Text Warning Text Warning Text Warning Text"
          Closable
          OnClose="LogSomething" />

<Alert Type="AlertType.Error"
          Message="Error Text"
          Description="Error Description Error Description Error Description Error Description Error Description Error Description"
          Closable
          OnClose="LogSomething" />

@code{
    private void LogSomething()
    {
        Console.WriteLine("Logging Something...");
    }
}
```

## 4. 顶部公告

关键差异：页面顶部通告形式，默认有图标且 `Type` 为 `'warning'`。

```razor
<Alert Type="AlertType.Warning"
       Message="Warning Text"
       Banner />

<Alert Type="AlertType.Warning"
       Message="Very long warning text warning text text text text text text text"
       Banner
       Closable />

<Alert Type="AlertType.Warning"
       Message="Warning Text Without Icon"
       Banner
       ShowIcon="false" />

<Alert Type="AlertType.Error"
       Message="Error Text"
       Banner />
```

## 5. 图标

关键差异：可口的图标让信息类型更加醒目。

```razor
<Alert Type="AlertType.Success"
       Message="Success Tips"
       ShowIcon="true" />

<Alert Type="AlertType.Info"
       Message="Informational Notes"
       ShowIcon="true" />

<Alert Type="AlertType.Warning"
       Message="Warning"
       ShowIcon="true"
       Closable />

<Alert Type="AlertType.Error"
       Message="Error"
       ShowIcon="true" />

<Alert Type="AlertType.Success"
       Message="Success Tips"
       Description="Detailed description and advice about successful copywriting."
       ShowIcon="true" />

<Alert Type="AlertType.Info"
       Message="Informational Notes"
       Description="Additional description and information about copywriting."
       ShowIcon="true" />

<Alert Type="AlertType.Warning"
       Message="Warning"
       Description="This is a warning notice about copywriting."
       ShowIcon="true"
       Closable />

<Alert Type="AlertType.Error"
       Message="Error"
       Description="This is an error message about copywriting."
       ShowIcon="true" />
```
