using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using Market.Abstraction;
using Market.Models.DTO;

namespace Market.Controllers
{
    public class CsvController : ControllerBase
    {
        public IProductRepository _productRepositoryCsv;
        public IDistributedCache _cache;

        public CsvController(IProductRepository productRepositoryCsv, IDistributedCache cache)
        {
            _productRepositoryCsv = productRepositoryCsv;
            _cache = cache;
        }

        public T GetData<T>(string key)
        {
            var value = _cache.GetString(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            var data = GetData<T>(key);
            if (data == null)
            {
                value = default;
                return false;
            }
            else
            {
                value = data;
                return true;
            }
        }

        private string GetCsv(IEnumerable<Product> products)
        {
            StringBuilder sb = new();
            foreach (var p in products)
            {
                sb.AppendLine(p.Name + ";" + p.Price + "\n");
            }
            return sb.ToString();
        }


        [HttpGet("GetProductCSV")]
        public FileContentResult GetProductsCsv()
        {
            using (var _productRepository = new StoreContext())
            {
                var products = _productRepository.Products.Select(x => new ProductDTO { Description = x.Description, Price = x.Price }).ToList();
                var content = GetCsv(products);
                return File(new System.Text.UTF8Encoding().GetBytes(content), "text/csv", "report.csv");
            }
        }
    }
}
