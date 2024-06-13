using Market.Abstraction;
using Market.Models;
using Market.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }


        [HttpGet("get_groups")]
        public IActionResult GetGroups()
        {
            var groups = _groupRepository.GetGroups();
            return Ok(groups);
        }


        [HttpPost("add_group")]
        public IActionResult AddGroup([FromBody] GroupDTO groupDTO)
        {
            var result = _groupRepository.AddGroup(groupDTO);
            return Ok(result);
        }


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
