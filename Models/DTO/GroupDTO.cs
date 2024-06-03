namespace Market.Models.DTO
{
    public class GroupDTO
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();
        public string? Name { get; set; }
    }
}
