namespace Core.Models
{

    /// <summary>
    /// A Promotion that offers a percentage discount rather than a set price.
    /// </summary>
    public class PercentagePromotion : Promotion
    {

        /// <summary>
        /// What percentage should be deducted when this promotion is applied.
        /// 
        /// This should be the exact amount, not the fractional equivelant. For example, 50% of should be 50m, not 0.5m.
        /// </summary>
        public decimal PercentOff { get; protected set; }

        public PercentagePromotion(string itemSKU, int numItemsToApply, decimal percentOff) : base(itemSKU, numItemsToApply)
        {
            PercentOff = percentOff;
        }
    }
}
