namespace DiscountFramework
{
    public class DiscountProduct
    {
        public DiscountProduct()
        {
            Quantity = 1;
        }

        public virtual int ProductId { get; set; }
        public virtual decimal? DiscountPercentage { get; set; }
        public virtual decimal? DiscountAmount { get; set; }
        public virtual bool Free { get; set; }
        public virtual bool MustBuy { get; set; }
        public virtual int Quantity { get; set; }
    }
}