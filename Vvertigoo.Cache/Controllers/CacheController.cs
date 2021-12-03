using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Vvertigoo.Cache.Services;

namespace Vvertigoo.Cache.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CacheController : ControllerBase
{
    private readonly ICacheService cacheService;

    public CacheController(ICacheService cacheService)
    {
        this.cacheService = cacheService;
    }

    [HttpGet("{id}")]
    public async Task<string?> Get(string id)
    {
        return await cacheService.Get(id);
    }

    [HttpPost("{id}")]
    public async Task Set(string id, [FromBody, Required] string value)
    {
        await cacheService.Set(id, value);
    }
}

