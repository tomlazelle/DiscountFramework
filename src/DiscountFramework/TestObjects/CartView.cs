
using System.Collections.Generic;
using System.Linq;

namespace DiscountFramework.TestObjects
{
    public class CartView
    {
        public decimal Total => GetTotal();
        public decimal SubTotal => GetSubTotal();
        public decimal Tax { get; set; }
        public int Id { get; set; }
        public decimal ShippingAmount { get; set; }

        public IEnumerable<CartItemView> Items
        {
            get { return _items; }
            set { }
        }

        private IList<CartItemView> _items = new List<CartItemView>();
        public void AddItem(CartItemView item)
        {
            _items.Add(item);
        }

        private decimal GetSubTotal()
        {
            return _items.Sum(x => x.Amount * x.Quantity);
        }

        private decimal GetTotal()
        {
            var tax = GetSubTotal() * Tax;

            return GetSubTotal() + tax;
        }
    }
}