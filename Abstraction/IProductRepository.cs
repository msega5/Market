using Market.Models.DTO;


namespace Market.Abstraction
{
    public interface IProductRepository
        {
            public int AddGroup(GroupDTO group);
            public IEnumerable<GroupDTO> GetGroups();


            public int AddProduct(ProductDTO product);
            public IEnumerable<ProductDTO> GetProducts();

        }
    }
