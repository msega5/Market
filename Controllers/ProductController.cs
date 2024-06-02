using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("getProduct")]
        public IActionResult GetProducts()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    var products = context.Products.Select(x => new Product()
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Description = x.Description
                    });
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("putProduct")]
        public IActionResult PutProducts([FromQuery] string name, string description, int groupId, int price)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (!context.Products.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Product
                        {
                            Name = name,
                            Description = description,
                            Price = price,
                            GroupID = groupId
                        });
                        context.SaveChanges();                        
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
