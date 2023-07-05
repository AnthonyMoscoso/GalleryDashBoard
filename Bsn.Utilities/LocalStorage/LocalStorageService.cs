using Bsn.Utilities.LocalStorage.Interfaces;
using Core.Utilities.Ensures;
using Core.Utilities.Extensions;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsn.Utilities.LocalStorage
{
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jSRuntime;
        public LocalStorageService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;

        }
        public async Task AddOrUpdateValue([NotNull] string key, [NotNull] string value)
        {
            Ensure.That(key).NotNullOrEmpty();
            Ensure.That(value).NotNullOrEmpty();
            await _jSRuntime.SetInLocalStorage(key, value);
        }


        public async Task Clear()
        {
            await _jSRuntime.ClearLocalStorage();
        }
        public async Task<TValue?> GetValue<TValue>([NotNull] string key)
        {
            Ensure.That(key).NotNullOrEmpty();
            string value = await _jSRuntime.GetFromLocalStorage(key);
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }
            return (TValue)Convert.ChangeType(value, typeof(TValue));

        }

        public async Task Remove([NotNull] string key)
        {
            Ensure.That(key, nameof(key)).NotNullOrEmpty();
            await _jSRuntime.RemoItemLocalStorage(key);
        }
    }
}
