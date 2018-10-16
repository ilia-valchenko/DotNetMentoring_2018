using System.Collections.Generic;

namespace Task3
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }

        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }
    }
}
