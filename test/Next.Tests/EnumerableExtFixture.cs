using System.Linq;
using Xunit;

namespace Next.Tests
{
    public sealed class EnumerableExtFixture
    {
        [Fact]
        public void EnumerableFollow()
        {
            var sequnce = new[] { 1, 2, 3 }
                .Follow(4);

            Assert.Equal(4, sequnce.Last());
        }

        [Fact]
        public void ToEnumerable()
        {
            Assert.Equal(1, 1.ToEnumerable().First());
            Assert.Equal(1, 1.ToEnumerable().Last());
        }
    }
}
