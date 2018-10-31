// ReSharper disable once CheckNamespace

using System.Linq;
using System.Reflection;

namespace System
{
    public static class EnumExt
    {
        public static (bool Converted, TEnum Result) ToEnum<TEnum>(object value)
            where TEnum : struct
        {
            if (value is string s)
            {
                return (Enum.TryParse(s, true, out TEnum result), result);
            }

            var enumType = typeof(TEnum);
            var underlyingType = Enum.GetUnderlyingType(enumType);

            if (underlyingType != value.GetType())
            {
                value = Convert.ChangeType(value, underlyingType);
            }
            
            if (!Enum.IsDefined(enumType, value))
            {
                var isDefined = false;
                
                if (enumType.GetTypeInfo().GetCustomAttributes(typeof(FlagsAttribute), true).IfNotNull(v => (bool?)v.Any()) ?? false)
                {
                    isDefined = value is byte b && Flags.Decode(b).All(v => Enum.IsDefined(enumType, v))
                                || value is int i && Flags.Decode(i).All(v => Enum.IsDefined(enumType, v));
                }
      
                if (!isDefined)
                {
                    return default;
                }
            }

            return (true, (TEnum)Enum.ToObject(enumType, value));
        }

        public static TEnum ToEnumOrFail<TEnum>(object value, Func<Exception> failWith)
            where TEnum : struct
        {
            var (converted, result) = ToEnum<TEnum>(value);

            return converted ? result : throw failWith();            
        }

        public static TEnum ToEnumOrDefault<TEnum>(object value, TEnum defaultValue)
            where TEnum : struct
        {
            var (converted, result) = ToEnum<TEnum>(value);

            return converted ? result : defaultValue;
        }
    }
}
