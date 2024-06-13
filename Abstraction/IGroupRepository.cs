using Market.Models.DTO;

namespace Market.Abstraction
{
    public interface IGroupRepository
    {
        public int AddGroup(GroupDTO group);
        public IEnumerable<GroupDTO> GetGroups();
    }
}
