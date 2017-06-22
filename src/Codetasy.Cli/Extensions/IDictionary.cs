using System.Collections.Generic;

namespace Codetasy.Extensions
{
    public static class IDictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key)
        {
            TValue value = default(TValue);
            try
            {
                value = dic[key];
            }
            catch
            {

            }

            return value;            
        }

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dic, KeyValuePair<TKey, TValue> keyValue) 
        {
            if (dic.ContainsKey(keyValue.Key))
            {
                dic[keyValue.Key] = keyValue.Value;
            }
            else
            {
                dic.Add(keyValue.Key, keyValue.Value);
            }
        }       
    }
}
