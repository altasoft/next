// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
    /// <summary>
    /// IDictionary extension methods
    /// </summary>
    public static class DictionaryExt
    {
        public static Tuple<bool, TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
        {
            TValue value;

            return Tuple.Create(self.TryGetValue(key, out value), value);
        }
    }
}
