using System.Collections.Generic;
using Codetasy.Extensions;

namespace Codetasy.Cli
{
    public class CliDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key] 
        { 
            get 
            {
                return this.GetValueOrDefault(key);
            } 
        }
    }
}
