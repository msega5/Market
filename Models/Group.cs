namespace Market.Models
{
    public class Group : BaseModel
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
