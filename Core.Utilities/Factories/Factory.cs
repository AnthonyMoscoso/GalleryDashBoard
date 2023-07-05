using Core.Utilities.Ensures;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Factories
{
    public static class Factory
    {
        public static TItem CreateFrom<TItem>(TItem item)
        {
            Type itemType = typeof(TItem);
            Ensure.That(item,nameof(item)).IsNotNull();
            TItem newItem = (TItem)Activator.CreateInstance(itemType)!;
            PropertyInfo[] properties = itemType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    object? value = property.GetValue(item);
                    property.SetValue(newItem, value);
                }
            }
            return newItem;
        }
    }
}
