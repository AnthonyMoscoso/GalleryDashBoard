using Core.Utilities.Ensures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Get specific value from dictionary on TValue object
        /// </summary>
        /// <typeparam name="TValue">Type of object to convert the value founded in dictionary </typeparam>
        /// <param name="keyValuePairs">Dictionary which we must search our value</param>
        /// <param name="key">key of the value to search</param>
        /// <returns>return TValue of value converter</returns>
        public static TValue? GetValue<TValue>(this IDictionary<string, string> keyValuePairs, string key)
        {
            Ensure.That(keyValuePairs).NotNullOrEmpty();
            Ensure.That(key,nameof(key)).NotNullOrEmpty();
            if (!keyValuePairs.ContainsKey(key))
            {
                return default;
            }
            string value = keyValuePairs[key];
            return (TValue)Convert.ChangeType(value, typeof(TValue));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="keyValuePairs"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TItem? GetValue<TKey,TValue,TItem>(this IDictionary<TKey, TValue> keyValuePairs, TKey key)
        {
            Ensure.That(keyValuePairs).NotNullOrEmpty();
            Ensure.That(key, nameof(key)).IsNotNull();
            if (!keyValuePairs.ContainsKey(key))
            {
                return default;
            }
            TValue? value = keyValuePairs[key];
            if (value == null)
            {
                return default;
            }
            return (TItem)Convert.ChangeType(value, typeof(TItem));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keyValuePairs"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue? GetValue<TValue>(this IReadOnlyDictionary<string, string> keyValuePairs, string key)
        {
            Ensure.That(keyValuePairs).IsNotNull();
            Ensure.That(key, nameof(key)).NotNullOrEmpty();
            if (!keyValuePairs.ContainsKey(key))
            {
                return default;
            }
            string value = keyValuePairs[key];
            return (TValue)Convert.ChangeType(value, typeof(TValue));
        }

        public static TItem? GetValue<TKey, TValue, TItem>(this IReadOnlyDictionary<TKey, TValue> keyValuePairs, TKey key)
        {
            Ensure.That(keyValuePairs).IsNotNull();
            Ensure.That(key, nameof(key)).IsNotNull();
            if (!keyValuePairs.ContainsKey(key))
            {
                return default;
            }
            TValue? value = keyValuePairs[key];
            if (value == null)
            {
                return default;
            }
            return (TItem)Convert.ChangeType(value, typeof(TItem));
        }
    }
}
