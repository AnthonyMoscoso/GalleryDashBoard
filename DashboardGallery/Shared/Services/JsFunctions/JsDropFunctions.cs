using Bsn.Utilities.Constants;
using DashboardGallery.Shared.Services.JsFunctions.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace DashboardGallery.Shared.Services.JsFunctions
{
    public class JsDropFunctions : IJsDropFunctions
    {
        private IJSRuntime? _jsRuntime;
        private IJSObjectReference? _jsModule;
        private IJSObjectReference? _dropZoneInstance;

        public async Task<JsDropFunctions> Build(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
            _jsModule = await _jsRuntime!.InvokeAsync<IJSObjectReference>(Constant.Import, JsFiles.DropZone);
            return this;
        }
        public async Task<bool> CheckFileInputAllowed()
        {
            bool allowedFile = await _dropZoneInstance!.InvokeAsync<bool>(JsMethods.checkFileInputAllowed);
            return allowedFile;
        }

        public async Task Dispose()
        {
            await _dropZoneInstance!.InvokeVoidAsync(JsMethods.dispose);
            await _dropZoneInstance!.DisposeAsync();
            await _jsModule!.DisposeAsync();
        }

        public async Task ImportFile(string idInput)
        {
            await _jsModule!.InvokeVoidAsync(JsMethods.ImportFile, idInput);
        }

        public async Task<IJSObjectReference> InitializeFileDropZone(ElementReference dropZoneElement, ElementReference? element, string[] formatAlloweds)
        {
            _dropZoneInstance = await _jsModule!.InvokeAsync<IJSObjectReference>(JsMethods.initializeFileDropZone, dropZoneElement, element, formatAlloweds);
            return _dropZoneInstance;
        }
    }
}
