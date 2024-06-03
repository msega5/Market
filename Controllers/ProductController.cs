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

        [HttpGet("get_products")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
                    return Ok(products);
        }

        [HttpGet("get_groups")]
        public IActionResult GetGroups()
        {
            var groups = _productRepository.GetGroups();
            return Ok(groups);
        }

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromBody] ProductDTO productDTO)
        {
            var result = _productRepository.AddProduct(productDTO);
            return Ok(result);
        }

        [HttpPost("add_group")]
        public IActionResult AddGroup([FromBody] GroupDTO groupDTO)
        {
            var result = _productRepository.AddGroup(groupDTO);
            return Ok(result);
        }
    }
}
