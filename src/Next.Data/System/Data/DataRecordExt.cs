using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

// ReSharper disable once CheckNamespace
namespace System.Data
{
    public static class DataRecordExt
    {
        private static Exception FieldValueTypeError(string name, object value, Type type) => new InvalidOperationException($"`{name}` field value: `{value}` is not {type.Name}.");

        private static TValue Get<TValue>(this IDataRecord self, string name)
            where TValue : struct
        {
            var value = self[name];

            return value is TValue v ? v : throw FieldValueTypeError(name, value, typeof(TValue));
        }

        #region String

        public static string GetString(this IDataRecord self, string name)
        {
            var value = self[name];

            switch (value)
            {
                case DBNull _: return null;
                case string s: return s;
                default: throw FieldValueTypeError(name, value, typeof(string));
            }
        }

        #endregion

        #region Boolean

        public static bool GetBoolean(this IDataRecord self, string name) => self.Get<bool>(name);

        public static bool? GetBooleanOrNull(this IDataRecord self, string name) => self.GetNullable<bool>(name);

        #endregion

        #region Byte

        public static byte GetByte(this IDataRecord self, string name) => self.Get<byte>(name);

        public static byte? GetByteOrNull(this IDataRecord self, string name) => self.GetNullable<byte>(name);

        #endregion

        #region Int16

        public static short GetInt16(this IDataRecord self, string name) => self.Get<short>(name);

        public static short? GetInt16OrNull(this IDataRecord self, string name) => self.GetNullable<short>(name);

        #endregion

        #region Int32

        public static int GetInt32(this IDataRecord self, string name) => self.Get<int>(name);

        public static int? GetInt32OrNull(this IDataRecord self, string name) => self.GetNullable<int>(name);

        #endregion

        #region Int64

        public static long GetInt64(this IDataRecord self, string name) => self.Get<long>(name);

        public static long? GetInt64OrNull(this IDataRecord self, string name) => self.GetNullable<long>(name);

        #endregion

        #region Decimal

        public static decimal GetDecimal(this IDataRecord self, string name) => self.Get<decimal>(name);

        public static decimal? GetDecimalOrNull(this IDataRecord self, string name) => self.GetNullable<decimal>(name);

        #endregion

        #region DateTime

        public static DateTime GetDateTime(this IDataRecord self, string name) => self.Get<DateTime>(name);

        public static DateTime? GetDateTimeOrNull(this IDataRecord self, string name) => self.GetNullable<DateTime>(name);

        #endregion

        #region Bytes

        public static byte[] GetBytes(this IDataRecord self, string name)
        {
            var value = self[name];

            switch (value)
            {
                case DBNull _: return null;
                case byte[] bytes: return bytes;
                default: throw FieldValueTypeError(name, value, typeof(byte[]));
            }
        }

        #endregion

        #region Timestamp

        private static ulong TimestampAsUInt64(byte[] bytes) => BitConverter.ToUInt64(bytes.Reverse().ToArray(), 0);

        public static ulong ReadTimestampAsUInt64(this IDataRecord self, string name)
        {
            var value = self[name];

            return value is byte[] bytes ? TimestampAsUInt64(bytes) : throw FieldValueTypeError(name, value, typeof(byte[]));
        }

        public static ulong? ReadTimestampAsUInt64OrNull(this IDataRecord self, string name)
        {
            var value = self[name];

            switch (value)
            {
                case DBNull _: return null;
                case byte[] bytes: return TimestampAsUInt64(bytes);
                default: throw FieldValueTypeError(name, value, typeof(byte[]));
            }
        }

        #endregion

        #region Enum

        public static TEnum AsEnumOrFailIfNotDefined<TEnum>(this IDataRecord self,
            string name,
            Func<object, Exception> failWith)
            where TEnum : struct
        {
            var value = self[name];
            return EnumExt.ToEnumOrFail<TEnum>(value, () => failWith(value));
        }

        public static TEnum? AsEnumOrNullOrFailIfNotDefined<TEnum>(this IDataRecord self,
            string name,
            Func<object, Exception> failWith)
            where TEnum : struct
        {
            var value = self[name];

            return Convert.IsDBNull(value)
                ? (TEnum?)null
                : EnumExt.ToEnumOrFail<TEnum>(value, () => failWith(value));
        }

        public static TEnum AsEnum<TEnum>(this IDataRecord self, string name)
            where TEnum : struct =>
            self.AsEnumOrFailIfNotDefined<TEnum>(name, v => FieldValueTypeError(name, v, typeof(TEnum)));

        public static TEnum? AsEnumOrNull<TEnum>(this IDataRecord self, string name)
            where TEnum : struct =>
            self.AsEnumOrNullOrFailIfNotDefined<TEnum>(name, v => FieldValueTypeError(name, v, typeof(TEnum)));

        #endregion

        #region Xml

        public static TResult ReadXmlAs<TResult>(this IDataRecord self, string name)
            where TResult : class => ReadXmlAsImpl<TResult>(self, name, new DataContractSerializer(typeof(TResult)));

        public static TResult ReadXmlAs<TResult>(this IDataRecord self, string name, IEnumerable<Type> knownTypes)
            where TResult : class => ReadXmlAsImpl<TResult>(self, name, new DataContractSerializer(typeof(TResult), knownTypes));

        public static TResult ReadXmlAs<TResult>(this IDataRecord self, string name,
            DataContractSerializerSettings serializerSettings)
            where TResult : class
            => ReadXmlAsImpl<TResult>(self, name, new DataContractSerializer(typeof(TResult), serializerSettings));

        private static TResult ReadXmlAsImpl<TResult>(IDataRecord record, string name, XmlObjectSerializer serializer)
            where TResult : class
        {
            var value = record.GetString(name);

            if (value == null)
            {
                return null;
            }

            using (var reader = new XmlTextReader(new StringReader(value)))
            {
                return (TResult)serializer.ReadObject(reader);
            }
        }

        #endregion

        public static TValue? GetNullable<TValue>(this IDataRecord self, string name)
            where TValue : struct
        {
            var value = self[name];

            switch (value)
            {
                case DBNull _: return null;
                case TValue v: return v;
                default: throw FieldValueTypeError(name, value, typeof(TValue));
            }
        }
    }
}
