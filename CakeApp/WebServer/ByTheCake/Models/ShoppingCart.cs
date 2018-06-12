namespace HTTPServer.ByTheCake.Models
{
    using HTTPServer.ByTheCake.Models.Cakes;
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public const string SessionKey = "^%Current_Shopping_Cart^%";

        public ShoppingCart()
        {
            this.Orders = new List<Cake>();
        }

        public List<Cake> Orders { get; private set; }
    }
}
