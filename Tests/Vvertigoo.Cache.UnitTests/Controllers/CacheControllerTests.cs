using AutoFixture;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Vvertigoo.Cache.Controllers;
using Vvertigoo.Cache.Services;
using Xunit;

namespace Vvertigoo.Cache.UnitTests.Controllers;

public class CacheControllerTests
{
    private readonly CacheController Controller;
    private readonly Fixture Fixture;
    private readonly Mock<ICacheService> CacheServiceMock;

    public CacheControllerTests()
    {
        CacheServiceMock = new Mock<ICacheService>();
        Controller = new CacheController(CacheServiceMock.Object);
        Fixture = new Fixture();
    }

    [Fact]
    public async Task Get_NonExistingKey_ShouldReturnNull()
    {
        var key = Fixture.Create<string>();
        CacheServiceMock.Setup(x => x.Get(key)).ReturnsAsync((string?)null);

        var response = await Controller.Get(key);

        CacheServiceMock.Verify(x => x.Get(key), Times.Once());
        response.Should().BeNull();
    }

    [Fact]
    public async Task Get_ExistingKey_ShouldReturnValue()
    {
        var key = Fixture.Create<string>();
        var value = Fixture.Create<string>();
        CacheServiceMock.Setup(x => x.Get(key)).ReturnsAsync(value);

        var response = await Controller.Get(key);

        CacheServiceMock.Verify(x => x.Get(key), Times.Once());
        response.Should().Be(value);
    }

    [Fact]
    public async Task Set_ShouldCallSetOnService()
    {
        var key = Fixture.Create<string>();
        var value = Fixture.Create<string>();

        await Controller.Set(key, value);

        CacheServiceMock.Verify(x => x.Set(key, value), Times.Once());
    }
}

