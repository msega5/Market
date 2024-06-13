using Market.Abstraction;
using Market.Models;
using Market.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;

        public ProductController(IProductRepository productRepository, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromBody] ProductDTO productDTO)
        {
            var result = _productRepository.AddProduct(productDTO);
            _cache.Remove("products");
            return Ok(result);
        }


        [HttpGet("get_products")]
        public IActionResult GetProducts()
        {
            if (_cache.TryGetValue("products", out List<Product> products))
            {
                return Ok(products);
            }

            products = _productRepository.GetProducts().Select(x => new Product { Name = x.Name, Price = x.Price }).ToList();
            _cache.Set("products", products, TimeSpan.FromMinutes(30));
            return Ok(products);
        }


        [HttpPut("priceUpdate/{productID}")]
        public IActionResult PriceUpdate(int productID, [FromQuery] int newPrice)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    var product = context.Products.Find(productID);
                    if (product != null)
                    {
                        product.Price = newPrice;
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpDelete("deleteProduct/{productID}")]
        public IActionResult DeleteProduct(int productID)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    var product = context.Products.Find(productID);
                    if (product != null)
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
