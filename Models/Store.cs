﻿namespace Market.Models
{
    public class Store : BaseModel
    {    
        public virtual List<Product> Products { get; set; } = null!;
        public int Count { get; set; }
    }
}
