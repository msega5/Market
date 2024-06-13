using Market.Models.DTO;

namespace Market.Abstraction
{
    public interface IProductRepository
        {            
            public int AddProduct(ProductDTO product);
            public IEnumerable<ProductDTO> GetProducts();

        }
    }
