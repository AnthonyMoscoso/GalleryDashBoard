
using Bsn.Utilities.Constants;
using DashboardGallery.Shared.Services.JsFunctions.Index;
using Microsoft.JSInterop;
using System.Data.Common;

namespace DashboardGallery.Shared.Services.JsFunctions
{
    public class JsIndexFunctions : IJsIndexFunctions
    {
        private IJSRuntime? _jSRuntime;
        private IJSObjectReference? _jsModule;

        public async Task AddClassInBody(string className)
        {
            await _jsModule!.InvokeVoidAsync(JsMethods.addClassToBody, className);
        }

        public async Task<JsIndexFunctions> Build(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
            _jsModule = await _jSRuntime!.InvokeAsync<IJSObjectReference>(Constant.Import, JsFiles.Index);
            return this;
        }

        public async Task ChangeOrder(string idColumn)
        {
            await _jsModule!.InvokeVoidAsync(JsMethods.ChangeOrder, idColumn);
        }

        public async Task RemoveClassInBody(string className)
        {
            await _jsModule!.InvokeVoidAsync(JsMethods.removeClassFromBody, className);
        }

        public async Task ResetTableDetails()
        {
            await _jsModule!.InvokeVoidAsync(JsMethods.ResetTableDetails);
        }

        public async Task SetTrigger(string idButton, string idDetail, string idSelectableTr)
        {
            object?[] args = new object[] { idButton, idDetail, idSelectableTr };
            await _jsModule!.InvokeVoidAsync(JsMethods.SetTrigger, args);
        }
    }
}
