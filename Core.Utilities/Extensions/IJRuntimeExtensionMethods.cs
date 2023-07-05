using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public static class IJRuntimeExtensionMethods
    {
        private static readonly string LOCAL_STORAGE = "localStorage";
        private static readonly string GetItem = "getItem";
        private static readonly string SetItem = "setItem";
        private static readonly string RemoveItem = "removeItem";
        public static ValueTask<string> SetInLocalStorage(this IJSRuntime js, string key, string content)
        {
            return js.InvokeAsync<string>($"{LOCAL_STORAGE}.{SetItem}", key, content);
        }

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
        {
            return js.InvokeAsync<string>($"{LOCAL_STORAGE}.{GetItem}", key);
        }


        public static ValueTask<string> RemoItemLocalStorage(this IJSRuntime js, string key)
        {
            return js.InvokeAsync<string>($"{LOCAL_STORAGE}.{RemoveItem}", key);
        }

        public static async Task ClearLocalStorage(this IJSRuntime js)
        {
            await js.InvokeVoidAsync($"{LOCAL_STORAGE}.clear");
        }
    }
}
