namespace Market.Models.DTO
{
    public class StoreDTO
    {
        public virtual List<Product> Products { get; set; } = null!;
        public int Count { get; set; }
    }
}
