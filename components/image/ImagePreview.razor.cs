// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using AntDesign.JsInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace AntDesign
{
    public partial class ImagePreview : System.IDisposable
    {
        [Parameter]
        public ImageRef ImageRef { get; set; }

        [Inject]
        protected IJSRuntime Js { get; set; }

        [Inject]
        private IDomEventListener DomEventListener { get; set; }

        private ElementReference _previewImg;
        private double _zoomOutTimes = 1;
        private int _rotateTimes;
        private bool _visible = true;
        private string _left = "50%";
        private string _top = "50%";
        private string _switchAnimationClass;


        private async Task HandleClose()
        {
            _visible = false;
            StateHasChanged();
            // Blocking DOM removal
            await Task.Delay(200);

            ImageRef.Close();
        }

        private void HandleZoomIn()
        {
            _zoomOutTimes++;
        }

        private void HandleZoomOut()
        {
            if (_zoomOutTimes > 1)
            {
                _zoomOutTimes--;
            }
        }

        private void HandleRotateRight()
        {
            _rotateTimes++;
        }

        private void HandleRotateLeft()
        {
            _rotateTimes--;
        }

        private void SwitchTo(int index)
        {
            if (index < 0 || index >= ImageRef.ImageCount || index == ImageRef.CurrentIndex)
            {
                return;
            }

            var direction = index > ImageRef.CurrentIndex ? "left" : "right";
            _zoomOutTimes = 1;
            _switchAnimationClass = $"ant-image-preview-img-wrapper-switch-{direction}-{index % 2}";
            ImageRef.SwitchTo(index);
        }

        private async Task HandleKeyDown(KeyboardEventArgs keyboardEventArgs)
        {
            if (keyboardEventArgs.Key == "ArrowLeft")
            {
                SwitchTo(ImageRef.CurrentIndex - 1);
            }
            else if (keyboardEventArgs.Key == "ArrowRight")
            {
                SwitchTo(ImageRef.CurrentIndex + 1);
            }
            else
            {
                return;
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task WeelHandZoom(WheelEventArgs wheelEventArgs)
        {
            _left = await Js.InvokeAsync<string>(JSInteropConstants.GetStyle, _previewImg, "left");
            _top = await Js.InvokeAsync<string>(JSInteropConstants.GetStyle, _previewImg, "top");

            if (wheelEventArgs.DeltaY < 0)
            {
                _zoomOutTimes += 0.1;
            }
            else if (_zoomOutTimes > 0.5)
            {
                _zoomOutTimes -= 0.1;
            }
        }

        private DialogOptions GetDialogOptions()
        {
            return new DialogOptions()
            {
                PrefixCls = "ant-image-preview",
                Closable = false,
                Footer = null,
                MaskClosable = true,
                CreateByService = true,
                OnCancel = async (e) =>
                {
                    await HandleClose();
                }
            };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (ImageRef.ImageCount > 1)
                {
                    DomEventListener.AddShared<KeyboardEventArgs>("document", "keydown", HandleKeyDown);
                }
                await Js.InvokeVoidAsync(JSInteropConstants.ImgDragAndDrop, _previewImg);
            }
        }

        public void Dispose()
        {
            DomEventListener?.Dispose();
        }
    }
}
