using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteGroupAndProductController : ControllerBase
    {
        [HttpDelete("deleteGroup/{groupID}")]
        public IActionResult DeleteGroup(int groupID)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    var group = context.Group.Find(groupID);
                    if (group != null)
                    {
                        var productInGroup = context.Products.Where(x => x.GroupID == groupID).ToList();
                        if (productInGroup.Any())
                        {
                            context.Products.RemoveRange(productInGroup);
                        }
                        context.Group.Remove(group);
                        context.SaveChanges();
                        return Ok("Group delete succsessful");
                    }
                    else
                    {
                        return NotFound("Group not found");
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
                        return Ok("Product delete succsessful");
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
