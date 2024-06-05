using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceAddUpdateController : ControllerBase
    {
        [HttpDelete("priceUpdate/{productID}")]
        public IActionResult priceUpdate(int productID, [FromQuery] int newPrice)
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
                        return Ok("Price update succsessfull");
                    }
                    else
                    {
                        return NotFound("Product not found");
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
