using System;

namespace DiscountFramework
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public decimal? DiscountPercentage { get; set; }
        // public decimal? DiscountAmount { get; set; }
        public bool Free { get; set; }
        public bool MustBuy { get; set; }
        
        //quantity of buy and amount free
        public int Quantity { get; set; } = 1;
        

        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}