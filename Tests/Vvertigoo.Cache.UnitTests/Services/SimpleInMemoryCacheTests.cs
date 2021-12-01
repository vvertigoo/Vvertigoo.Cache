using AutoFixture;
using FluentAssertions;
using System.Threading.Tasks;
using Vvertigoo.Cache.Services;
using Xunit;

namespace Vvertigoo.Cache.UnitTests.Services
{
    public class SimpleInMemoryCacheTests
    {
        private readonly Fixture Fixture;

        public SimpleInMemoryCacheTests()
        {
            Fixture = new Fixture();
        }

        [Fact]
        public async Task SetAndGet_ValidKeyAndValueWhenKeyIsNotExits_ShouldReturnValueByKey()
        {
            var cache = new SimpleInMemoryCache();
            var key = Fixture.Create<string>();
            var value = Fixture.Create<string>();

            await cache.Set(key, value);
            var result = await cache.Get(key);

            result.Should().Be(value);
        }

        [Fact]
        public async Task SetAndGet_ValidKeyAndValueWhenKeyExits_ShouldReturnNewValueByKey()
        {
            var cache = new SimpleInMemoryCache();
            var key = Fixture.Create<string>();
            var oldValue = Fixture.Create<string>();
            var newValue = Fixture.Create<string>();

            await cache.Set(key, oldValue);
            await cache.Set(key, newValue);
            var result = await cache.Get(key);

            result.Should().Be(newValue);
        }

        [Fact]
        public async Task Get_KeyNotExits_ShouldReturnNull()
        {
            var cache = new SimpleInMemoryCache();
            var key = Fixture.Create<string>();

            var result = await cache.Get(key);

            result.Should().BeNull();
        }

        [Fact]
        public void ItemsCount_NoItems_ShouldReturnZero()
        {
            var cache = new SimpleInMemoryCache();         

            cache.ItemsCount.Should().Be(0);
        }

        [Fact]
        public void Keys_NoItems_ShouldReturnEmptyCollection()
        {
            var cache = new SimpleInMemoryCache();

            cache.Keys.Should().HaveCount(0);
        }

        [Fact]
        public async Task ItemsCount_OneItems_ShouldReturnZero()
        {
            var cache = new SimpleInMemoryCache();
            var key = Fixture.Create<string>();
            var value = Fixture.Create<string>();

            await cache.Set(key, value);

            cache.ItemsCount.Should().Be(1);
        }

        [Fact]
        public async Task Keys_OneItems_ShouldReturnEmptyCollection()
        {
            var cache = new SimpleInMemoryCache();
            var key = Fixture.Create<string>();
            var value = Fixture.Create<string>();

            await cache.Set(key, value);

            cache.Keys.Should().HaveCount(1).And.Contain(key);
        }
    }
}
