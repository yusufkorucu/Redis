using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Redis.DistributedCahe.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        readonly IDistributedCache _distributedCache;

        public ExampleController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet("Set")]
        public async Task<IActionResult> Set(string name, string surname)
        {
            await _distributedCache.SetStringAsync("name", name, options: new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
                SlidingExpiration = TimeSpan.FromSeconds(15)
            });

            await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname));

            return Ok();
        }

        [HttpGet("Get")]

        public async Task<IActionResult> Get()
        {
            var name = await _distributedCache.GetStringAsync("name");

            var surnameBinary = await _distributedCache.GetAsync("surname");

            var surname = Encoding.UTF8.GetString(surnameBinary);

            return Ok(new
            {
                name,
                surname
            });
        }
    }
}
