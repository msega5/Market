using AutoMapper;
using Market.Models;
using Market.Models.DTO;

namespace Market.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Group, GroupDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Store, StoreDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
