using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class CustomerBasket
    {
        public string BuyerId { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public CustomerBasket()
        {

        }

        public CustomerBasket(string customerId)
        {
            BuyerId = customerId;
        }
    }
}
