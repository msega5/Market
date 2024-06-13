using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Market.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {

        private readonly IEnumerable<CacheController> cache;


        private string GetCache(IEnumerable<CacheController> cache)
        {
            StringBuilder sb = new();
            foreach (var c in cache)
            {
                sb.AppendLine(c + "\n");
            }
            return sb.ToString();
        }


        [HttpGet("get_cache")]
        public IActionResult GetCacheStats(IMemoryCache _cache)
        {
            var content = GetCache(cache);
            //return File(new System.Text.UTF8Encoding().GetBytes(content), "text/cache", "report.csv");

            string fileName = null;

            fileName = "cache" + DateTime.Now.ToBinary().ToString() + ".csv";

            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName), content);

            return "https://" + Request.Host.ToString() + "/static/" + fileName;

        }
    }
}
