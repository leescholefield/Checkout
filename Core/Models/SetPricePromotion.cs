namespace Core.Models
{

    /// <summary>
    /// A Promotion that offers a set-price when you buy a certain number of items.
    /// </summary>
    public class SetPricePromotion : Promotion
    {

        public decimal Price { get; protected set; }

        public SetPricePromotion(string itemSKU, int numItemsToApply, decimal price) : base(itemSKU, numItemsToApply)
        {
            Price = price;
        }
    }
}
