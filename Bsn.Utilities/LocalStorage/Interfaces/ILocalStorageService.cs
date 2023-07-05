using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsn.Utilities.LocalStorage.Interfaces
{
    public interface ILocalStorageService
    {
        Task<TValue?> GetValue<TValue>([NotNull] string key);
        Task Remove([NotNull]string key);
        Task AddOrUpdateValue([NotNull]string key,[NotNull] string value);
        Task Clear();
    }
}
