using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class Flags
    {
        public static IEnumerable<int> Decode(int value) =>
            Enumerable.Range(0, value).Select(v => 1 << v).TakeWhile(v => v <= value).Where(v => (v & value) == v);

        public static IEnumerable<byte> Decode(byte value) =>
            Enumerable.Range(0, value).Select(v => (byte)(1 << v)).TakeWhile(v => v <= value).Where(v => (v & value) == v);

        public static IEnumerable<int> DecodeBitPositions(this int self)
        {
            var pos = -1;
            var flag = 0;

            while ((flag = (1 << ++pos)) <= self)
            {
                if ((flag & self) == flag)
                    yield return pos;
            }
        }

        public static IEnumerable<int> DecodeBitPositions(this byte self)
        {
            var pos = -1;
            var flag = 0;

            while ((flag = (1 << ++pos)) <= self)
            {
                if ((flag & self) == flag)
                    yield return pos;
            }
        }
    }
}
