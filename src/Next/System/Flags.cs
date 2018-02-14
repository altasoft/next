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

        public static IEnumerable<int> DecodeBitPositions(int value)
        {
            var pos = -1;
            int flag;

            while ((flag = 1 << ++pos) <= value)
            {
                if ((flag & value) == flag)
                {
                    yield return pos;
                }
            }
        }

        public static IEnumerable<int> DecodeBitPositions(byte value)
        {
            var pos = -1;
            int flag;

            while ((flag = 1 << ++pos) <= value)
            {
                if ((flag & value) == flag)
                {
                    yield return pos;
                }
            }
        }
    }
}
