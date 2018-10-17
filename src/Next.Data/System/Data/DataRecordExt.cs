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
        #region String

        public static string GetString(this IDataRecord self, string name)
        {
            var value = self[name];

            if (Convert.IsDBNull(value))
            {
                return null;
            }

            return (string)value;
        }

        #endregion

        #region Boolean

        public static bool GetBoolean(this IDataRecord self, string name) => (bool)self[name];

        public static bool? GetBooleanOrNull(this IDataRecord self, string name) => self.GetNullable<bool>(name);

        #endregion

        #region Byte

        public static byte GetByte(this IDataRecord self, string name) => (byte)self[name];

        public static byte? GetByteOrNull(this IDataRecord self, string name) => self.GetNullable<byte>(name);

        #endregion

        #region Int16

        public static short GetInt16(this IDataRecord self, string name) => (short)self[name];

        public static short? GetInt16OrNull(this IDataRecord self, string name) => self.GetNullable<short>(name);

        #endregion

        #region Int32

        public static int GetInt32(this IDataRecord self, string name) => (int)self[name];

        public static int? GetInt32OrNull(this IDataRecord self, string name) => self.GetNullable<int>(name);

        #endregion

        #region Int64

        public static long GetInt64(this IDataRecord self, string name) => (long)self[name];

        public static long? GetInt64OrNull(this IDataRecord self, string name) => self.GetNullable<long>(name);

        #endregion

        #region Decimal

        public static decimal GetDecimal(this IDataRecord self, string name) => (decimal)self[name];

        public static decimal? GetDecimalOrNull(this IDataRecord self, string name) => self.GetNullable<decimal>(name);

        #endregion

        #region DateTime

        public static DateTime GetDateTime(this IDataRecord self, string name) => (DateTime)self[name];

        public static DateTime? GetDateTimeOrNull(this IDataRecord self, string name) => self.GetNullable<DateTime>(name);

        #endregion

        #region Bytes

        public static byte[] GetBytes(this IDataRecord self, string name)
        {
            var value = self[name];

            if (Convert.IsDBNull(value))
            {
                return null;
            }

            return (byte[])value;
        }

        #endregion

        #region Timestamp

        public static ulong ReadTimestampAsUInt64(this IDataRecord self, string name) => BitConverter.ToUInt64(((byte[])self[name]).Reverse().ToArray(), 0);

        public static ulong? ReadTimestampAsUInt64OrNull(this IDataRecord self, string name)
        {
            var value = self[name];

            if (Convert.IsDBNull(value))
            {
                return null;
            }

            return BitConverter.ToUInt64(((byte[])value).Reverse().ToArray(), 0);
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

            if (Convert.IsDBNull(value))
            {
                return null;
            }

            return (TValue)value;
        }
    }
}
