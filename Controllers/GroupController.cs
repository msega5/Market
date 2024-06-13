using Market.Abstraction;
using Market.Models;
using Market.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMemoryCache _cache;

        public GroupController(IGroupRepository groupRepository, IMemoryCache cache)
        {
            _groupRepository = groupRepository;
            _cache = cache;
        }


        [HttpPost("add_group")]
        public IActionResult AddGroup([FromBody] GroupDTO groupDTO)
        {
            var result = _groupRepository.AddGroup(groupDTO);
            _cache.Remove("groups");
            return Ok(result);
        }


        [HttpGet("get_groups")]
        public IActionResult GetGroups()
        {
            if (_cache.TryGetValue("groups", out List<Group> groups))
            {
                return Ok(groups);
            }
            groups = _groupRepository.GetGroups().Select(x => new Group { Name = x.Name }).ToList();
            _cache.Set("groups", groups, TimeSpan.FromMinutes(30));
            return Ok(groups);
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
