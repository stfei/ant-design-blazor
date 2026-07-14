# Ant Design Blazor Upload 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 拖拽上传 | 把文件拖入指定区域，完成上传，同样支持点击上传。 设置 `multiple` 后，在 `IE10+` 可以一次上传多个文件。 |
| 手动上传 | 选择文件后不上传,执行开始按钮再上传。 >（注意：使用原生InputFile组件时，由于不调用js的方法，所有不需要采用这个方案） |
| 已上传的文件列表 | 使用 `defaultFileList` 设置已上传的内容。 |
| 粘贴上传 | 你可以将文件复制到剪贴板，然后在输入框中粘贴来上传。 |
| 文件夹上传 | 支持上传一个文件夹里的所有文件。 |
| 用户头像 | 点击上传用户头像，并使用 beforeUpload 限制用户上传的图片格式和大小。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Upload Action="/api/files"
        Name="file"
        Accept=".png,.jpg"
        OnSingleCompleted="HandleCompleted">
    <Button Icon="upload">选择文件</Button>
</Upload>

@code {
    private void HandleCompleted(UploadInfo info)
    {
        if (info.File.State == UploadState.Success)
        {
            Console.WriteLine("上传成功");
        }
    }
}
```

## 2. 拖拽上传

关键差异：把文件拖入指定区域，完成上传，同样支持点击上传。 设置 `multiple` 后，在 `IE10+` 可以一次上传多个文件。

```razor
<Upload Action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
        Name="files"
        Drag
        Multiple
        OnSingleCompleted="OnSingleCompleted">
    <p class="ant-upload-drag-icon">
        <Icon Type="@IconType.Outline.Inbox" />
    </p>
    <p class="ant-upload-text">Click or drag file to this area to upload</p>
    <p class="ant-upload-hint">
        Support for a single or bulk upload. Strictly prohibit from uploading company data or other
        band files
    </p>
</Upload>

@code {

    void OnSingleCompleted(UploadInfo fileinfo)
    {
        if (fileinfo.File.State == UploadState.Success)
        {
            var result = fileinfo.File.GetResponse<ResponseModel>();
            fileinfo.File.Url = result.url;
        }
    }

    public class ResponseModel
    {
        public string name { get; set; }

        public string status { get; set; }

        public string url { get; set; }

        public string thumbUrl { get; set; }
    }
}
```

## 3. 手动上传

关键差异：选择文件后不上传,执行开始按钮再上传。 >（注意：使用原生InputFile组件时，由于不调用js的方法，所有不需要采用这个方案）

```razor
<Upload @ref="_upload"
        Action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
        Name="files"
        Defer
        BatchUpload
        FileListChanged="FileListChanged"
		ListType="UploadListType.Picture"
        OnSingleCompleted="OnSingleCompleted">
    <Button Icon="upload">
        <span>Select File</span>
    </Button>
</Upload>

<br />

<Button Type="ButtonType.Primary" Disabled="!canUpload" OnClick="HandleUpload">Start Upload</Button>
@code {
    Upload _upload;
    bool canUpload = false;

    void OnSingleCompleted(UploadInfo fileinfo)
    {
        if (fileinfo.File.State == UploadState.Success)
        {
            var result = fileinfo.File.GetResponse<ResponseModel>();
            fileinfo.File.Url = result.url;
        }
    }

    void FileListChanged(List<UploadFileItem> files)
    {
        if (files.Count > 0)
            canUpload = true;
        else
            canUpload = false;
        StateHasChanged();
    }
    async Task HandleUpload()
    {
        await _upload.StartUpload();
    }

    public class ResponseModel
    {
        public string name { get; set; }

        public string status { get; set; }

        public string url { get; set; }

        public string thumbUrl { get; set; }
    }

}
```

## 4. 已上传的文件列表

关键差异：使用 `defaultFileList` 设置已上传的内容。

```razor
@inject IMessageService _message

<Upload @attributes="attrs"
        OnSingleCompleted="OnSingleCompleted">
    <Button Icon="upload">
        <span>Upload</span>
    </Button>
</Upload>

@code
{

    Upload upload;

    Dictionary<string, object> attrs = new Dictionary<string, object>
{
        {"Action", "https://www.mocky.io/v2/5cc8019d300000980a055e76" },
        {"Name", "files" },
        {"DefaultFileList", new List<UploadFileItem>
            {
                new UploadFileItem
                {
                    Id = "1",
                    FileName = "1.jpg",
                    Url = "https://www.baidu.com/1.jpg",
                    State = UploadState.Success
                },
                new UploadFileItem
                {
                    Id = "2",
                    FileName = "2.jpg",
                    Response = "网络错误",
                    State = UploadState.Fail
                }
            }
        }
    };

    void OnSingleCompleted(UploadInfo fileinfo)
    {
        if (fileinfo.File.State == UploadState.Success)
        {
            var result = fileinfo.File.GetResponse<ResponseModel>();
            fileinfo.File.Url = result.url;
        }
    }

    public class ResponseModel
    {
        public string name { get; set; }

        public string status { get; set; }

        public string url { get; set; }

        public string thumbUrl { get; set; }
    }

}
```

## 5. 粘贴上传

关键差异：你可以将文件复制到剪贴板，然后在输入框中粘贴来上传。

```razor
@inject IMessageService _message
@inject IJSRuntime JS

<Divider>文本框粘贴事件</Divider>
<Upload @ref="upload"
        Action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
        Name="files"
        Defer="true"
        @bind-FileList="fileList"
        ListType="UploadListType.Picture"
        Trigger="UploadTrigger.Paste">
    <TextArea Placeholder="Paste files here (Ctrl+V)" />
</Upload>

<Divider>页面粘贴事件</Divider>
<Upload Action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
        Name="files2"
        Pastable
        @bind-FileList="fileList2"
        Defer>
    <Button Icon="upload">
        <span>Page Paste or click to upload</span>
    </Button>
</Upload>

<Button Type="ButtonType.Primary" OnClick="HandleUpload" Disabled="!CanUpload">Start Upload</Button>

@code {

    Upload upload;

    List<UploadFileItem> fileList = [];
    List<UploadFileItem> fileList2 = [];
    bool CanUpload => fileList?.Any(x => x.State == UploadState.Waiting) ?? false;

    async Task HandleUpload()
    {
        await upload.StartUpload();
    }
}
```

## 6. 文件夹上传

关键差异：支持上传一个文件夹里的所有文件。

```razor
<Upload Action="https://www.mocky.io/v2/5cc8019d300000980a055e76" Directory OnSingleCompleted="OnSingleCompleted">
    <Button Icon="upload">
        <span>Upload Directory</span>
    </Button>
</Upload>

@code{
    void OnSingleCompleted(UploadInfo fileinfo)
    {
        if (fileinfo.File.State == UploadState.Success)
        {
            var result = fileinfo.File.GetResponse<ResponseModel>();
            fileinfo.File.Url = result.url;
        }
    }

    public class ResponseModel
    {
        public string name { get; set; }

        public string status { get; set; }

        public string url { get; set; }

        public string thumbUrl { get; set; }
    }

}
```

## 7. 用户头像

关键差异：点击上传用户头像，并使用 beforeUpload 限制用户上传的图片格式和大小。

```razor
@inject IMessageService _message

    <Upload Action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
            Name="avatar"
            Class="avatar-uploader"
            ListType="UploadListType.PictureCard"
            ShowUploadList="false"
            BeforeUpload="BeforeUpload"
            OnChange="HandleChange">
        @if (!string.IsNullOrWhiteSpace(imageUrl))
        {
            <img src="@imageUrl" alt="avatar" style="width: 100%" />
        }
        else
        {
            <div>
                <Icon Spin="loading" Type="@(loading ? IconType.Outline.Loading : IconType.Outline.Plus)" />
                <div className="ant-upload-text">Upload</div>
            </div>
        }
    </Upload>

@code
{
    bool loading = false;

    string imageUrl;

    bool BeforeUpload(UploadFileItem file)
    {
        var isJpgOrPng = file.Type == "image/jpeg" || file.Type == "image/png";
        if (!isJpgOrPng)
        {
            _message.Error("You can only upload JPG/PNG file!");
        }
        var isLt2M = file.Size / 1024 / 1024 < 2;
        if (!isLt2M)
        {
            _message.Error("Image must smaller than 2MB!");
        }
        return isJpgOrPng && isLt2M;
    }

    void HandleChange(UploadInfo fileinfo)
    {
        loading = fileinfo.File.State == UploadState.Uploading;

        if (fileinfo.File.State == UploadState.Success)
        {
            imageUrl = fileinfo.File.ObjectURL;
        }
        InvokeAsync(StateHasChanged);
    }

    public class ResponseModel
    {
        public string name { get; set; }

        public string status { get; set; }

        public string url { get; set; }

        public string thumbUrl { get; set; }
    }
}
<style>
    .avatar-uploader > .ant-upload {
        width: 128px;
        height: 128px;
    }
</style>
```
