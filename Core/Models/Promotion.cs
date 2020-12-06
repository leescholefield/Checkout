namespace Core.Models
{
    public abstract class Promotion
    {
        public string ItemSKU { get; protected set; }

        /// <summary>
        /// The number of items a customer must buy before the promotion is applied.
        /// </summary>
        public int NumItemsRequired { get; protected set; }

        public Promotion(string itemSKU, int numItemToApply)
        {
            ItemSKU = itemSKU;
            NumItemsRequired = numItemToApply;
        }
    }
}
