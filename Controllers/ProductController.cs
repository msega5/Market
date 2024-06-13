using Market.Abstraction;
using Market.Models;
using Market.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromBody] ProductDTO productDTO)
        {
            var result = _productRepository.AddProduct(productDTO);
            return Ok(result);
        }


        [HttpGet("get_products")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
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
