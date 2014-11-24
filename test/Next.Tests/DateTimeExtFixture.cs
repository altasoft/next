using System;
using System.Linq;
using Xunit;

namespace Next.Tests
{
    public sealed class DateTimeExtFixture
    {
        [Fact]
        public void EndOfMonth()
        {
            Assert.Equal(new DateTime(2014, 10, 31), new DateTime(2014, 10, 23).EndOfMonth());
            Assert.Equal(new DateTime(2016, 2, 29), new DateTime(2016, 2, 17).EndOfMonth());
            Assert.Equal(new DateTime(2017, 2, 28), new DateTime(2017, 2, 17).EndOfMonth());
            Assert.Equal(new DateTime(2014, 11, 30), new DateTime(2014, 11, 11).EndOfMonth());
        }

        [Fact]
        public void DateTimeIsEndOfMonth()
        {
            Assert.True(new DateTime(2014, 10, 31).IsEndOfMonth());
            Assert.True(new DateTime(2016, 2, 29).IsEndOfMonth());
            Assert.True(new DateTime(2017, 2, 28).IsEndOfMonth());
            Assert.True(new DateTime(2014, 11, 30).IsEndOfMonth());
        }

        [Fact]
        public void FollowingMonths()
        {
            Assert.True(new DateTime(2014, 10, 31)
                .FollowingMonths()
                .Take(5)
                .SequenceEqual(new[]
                {
                    new DateTime(2014, 11, 30),
                    new DateTime(2014, 12, 31),
                    new DateTime(2015, 1, 31),
                    new DateTime(2015, 2, 28),
                    new DateTime(2015, 3, 31)
                }));

            Assert.True(new DateTime(2014, 10, 23)
                .FollowingMonths()
                .Take(5)
                .SequenceEqual(new[]
                {
                    new DateTime(2014, 11, 23),
                    new DateTime(2014, 12, 23),
                    new DateTime(2015, 1, 23),
                    new DateTime(2015, 2, 23),
                    new DateTime(2015, 3, 23)
                }));

            Assert.True(new DateTime(2014, 10, 31)
                .FollowingMonths(2)
                .Take(5)
                .SequenceEqual(new[]
                {                    
                    new DateTime(2014, 12, 31),                    
                    new DateTime(2015, 2, 28),
                    new DateTime(2015, 4, 30),
                    new DateTime(2015, 6, 30),
                    new DateTime(2015, 8, 31)
                }));

            Assert.True(new DateTime(2014, 10, 23)
                .FollowingMonths(2)
                .Take(5)
                .SequenceEqual(new[]
                {                    
                    new DateTime(2014, 12, 23),
                    new DateTime(2015, 2, 23),
                    new DateTime(2015, 4, 23),
                    new DateTime(2015, 6, 23),
                    new DateTime(2015, 8, 23)
                }));
        }

        [Fact]
        public void DateTimeFollowingEndOfMonths()
        {
            Assert.True(new DateTime(2014, 10, 31)
                .FollowingEndOfMonths()
                .Take(5)
                .SequenceEqual(new[]
                {
                    new DateTime(2014, 11, 30),
                    new DateTime(2014, 12, 31),
                    new DateTime(2015, 1, 31),
                    new DateTime(2015, 2, 28),
                    new DateTime(2015, 3, 31)
                }));

            Assert.True(new DateTime(2014, 10, 23)
                .FollowingEndOfMonths()
                .Take(5)
                .SequenceEqual(new[]
                {
                    new DateTime(2014, 10, 31),
                    new DateTime(2014, 11, 30),
                    new DateTime(2014, 12, 31),
                    new DateTime(2015, 1, 31),
                    new DateTime(2015, 2, 28)                    
                }));

            Assert.True(new DateTime(2014, 10, 31)
                .FollowingEndOfMonths(2)
                .Take(5)
                .SequenceEqual(new[]
                {                    
                    new DateTime(2014, 12, 31),                    
                    new DateTime(2015, 2, 28),
                    new DateTime(2015, 4, 30),
                    new DateTime(2015, 6, 30),
                    new DateTime(2015, 8, 31)
                }));

            Assert.True(new DateTime(2014, 10, 23)
                .FollowingEndOfMonths(2)
                .Take(5)
                .SequenceEqual(new[]
                {                    
                    new DateTime(2014, 12, 31),                    
                    new DateTime(2015, 2, 28),
                    new DateTime(2015, 4, 30),
                    new DateTime(2015, 6, 30),
                    new DateTime(2015, 8, 31)
                }));
        }
    }
}
