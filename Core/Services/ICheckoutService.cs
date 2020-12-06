using Core.Models;

namespace Core.Services
{
    /// <summary>
    /// This is reposinsible for keeping track of the items a user has added to their cart and returning the total price.
    /// </summary>
    public interface ICheckoutService
    {
        /// <summary>
        /// Adds the specified number of items to the users basket.
        /// </summary>
        /// <param name="item">Item to add to the basket.</param>
        /// <param name="count">Number of instances of item to add.</param>
        public void Add(Item item, int count = 1);

        /// <summary>
        /// Returns the total price of all items in the basket.
        /// </summary>
        public decimal GetTotal();
    }
}
