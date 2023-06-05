using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Redis.InMemoryCache.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;

        public ExampleController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        [HttpGet("Set/{name}")]
        public void Set(string name)
        {
            _memoryCache.Set("name", name);

        }

        [HttpGet("Get")]
        public string Get()
        {
            if (_memoryCache.TryGetValue<string>("name", out string name))
                return name;

            return "";
        }
        [HttpGet("SetDate")]

        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now,options:new MemoryCacheEntryOptions
            {
                AbsoluteExpiration=DateTimeOffset.Now.AddSeconds(30),
                SlidingExpiration=TimeSpan.FromSeconds(5),
            });
        }

        [HttpGet("GetDate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }

    }
}
