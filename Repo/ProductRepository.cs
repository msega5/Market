using AutoMapper;
using Market.Abstraction;
using Market.Models;
using Market.Models.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
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
                    _cache.Remove("groups");
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
                    _cache.Remove("product");
                }
                return entityProduct.ID;
            }
        }

        public IEnumerable<GroupDTO> GetGroups()
        {
            if (_cache.TryGetValue("groups", out List<GroupDTO> groups))
            {
                return groups;
            }            
            using (var context = new StoreContext())
            {
                var groupList = context.Group.Select(x => _mapper.Map<GroupDTO>(x)).ToList();
                _cache.Set("groups", groupList, TimeSpan.FromMinutes(30));
                return groupList;
            }
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            if (_cache.TryGetValue("product", out List<ProductDTO> product))
            {
                return product;
            }
            using (var context = new StoreContext())
            {
                var productList = context.Products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
                _cache.Set("product", productList, TimeSpan.FromMinutes(30));
                return productList;
            }
        }
    }
}
