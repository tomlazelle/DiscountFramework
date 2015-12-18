
using System.Collections.Generic;

namespace DiscountFramework.TestObjects
{
    public class CartView
    {
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public int Id { get; set; }

        public IEnumerable<CartItemView> Items
        {
            get { return _items; }
            set { }
        }

        private IList<CartItemView> _items = new List<CartItemView>();
        public void AddItem(CartItemView item)
        {
            SubTotal += item.Amount*item.Quantity;

            _items.Add(item);
        }

    }
}