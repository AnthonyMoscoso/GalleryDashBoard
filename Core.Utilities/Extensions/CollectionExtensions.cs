using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public static class ICollectionExtensions
    {
        public static void Insert<T>(this ICollection<T> collection, int index, T item)
        {
            if (collection is IList<T> list)
            {
                list.Insert(index, item);
            }
            else
            {
                if (index != 0)
                {
                    throw new NotSupportedException("La colección no admite la inserción en una posición específica.");
                }

                collection.Add(item);
            }
        }
    }
}
