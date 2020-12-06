using Core.Models;

namespace Core.Services
{
    /// <summary>
    /// Calcualtes the total price of a <see cref="Basket"/>.
    /// </summary>
    public interface IPricingService
    {
        /// <summary>
        /// Returns the total price of all items in <paramref name="basket"/> after any applicable promotions have been applied.
        /// </summary>
        decimal CalculateBasketPrice(Basket basket);
    }
}
