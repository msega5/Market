using AutoMapper;
using Market.Abstraction;
using Market.Models;
using Market.Models.DTO;

namespace Market.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public int AddGroup(GroupDTO group)
        {            
            using (var context = new StoreContext())
            {
                var entityGroup = context.Group.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if (entityGroup == null)
                {
                entityGroup = _mapper.Map<Models.Group>(group);
                context.Group.Add(entityGroup);
                context.SaveChanges();
                }
                return entityGroup.ID;
            }             
        }

        public int AddProduct(ProductDTO product)
        {
            using (var context = new StoreContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Models.Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
                }
                return entityProduct.ID;
            }
        }

        public IEnumerable<GroupDTO> GetGroups()
        {
            using (var context = new StoreContext())
            {
                var groupList = context.Group.Select(x => _mapper.Map<GroupDTO>(x)).ToList();
                return groupList;
            }
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            using (var context = new StoreContext())
            {
                var productList = context.Products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
                return productList;
            }
        }
    }
}
