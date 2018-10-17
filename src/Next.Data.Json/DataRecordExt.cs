using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace System.Data
{
    public static class DataRecordExt
    {
        #region Json

        public static TResult ReadJsonAs<TResult>(this IDataRecord self, string name, TypeNameHandling typeNameHandling = TypeNameHandling.Auto)
            where TResult : class => ReadJsonAs<TResult>(self, name, new JsonSerializerSettings { TypeNameHandling = typeNameHandling });

        public static TResult ReadJsonAs<TResult>(this IDataRecord self, string name, JsonSerializerSettings serializerSettings)
            where TResult : class => self.GetString(name).IfNotNull(v => JsonConvert.DeserializeObject<TResult>(v, serializerSettings));

        #endregion
    }
}
