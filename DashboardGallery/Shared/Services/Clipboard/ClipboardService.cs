using Bsn.Utilities.Constants;
using Microsoft.JSInterop;

namespace Dashiell.Front.App.Services
{
    public class ClipboardService : IClipboardService
    {
        private readonly IJSRuntime _jsInterop;
        public ClipboardService(IJSRuntime jsInterop)
        {
            _jsInterop = jsInterop;
        }
        public async Task CopyToClipboard(string text)
        {
            await _jsInterop.InvokeVoidAsync(Constant.Navigator_clipboard_writetext, text);
        }
    }
}
