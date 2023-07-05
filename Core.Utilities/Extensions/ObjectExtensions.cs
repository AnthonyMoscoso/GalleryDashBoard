using Core.Utilities.Ensures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bsn.Utilities
{
    public static class ObjectExtensions
    {
        public static string SerializeInJson<TItem>(this TItem item)
        {
            return JsonSerializer.Serialize(item);
        }
        
    }
}
