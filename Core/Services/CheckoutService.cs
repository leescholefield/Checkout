using Core.Models;
using System;

namespace Core.Services
{
    /// <inheritdoc cref="ICheckoutService"/>
    public class CheckoutService : ICheckoutService
    {

        private readonly IPricingService _pricingService;
        private readonly Basket _basket;

        public CheckoutService(IPricingService pricingService)
        {
            _pricingService = pricingService;
            _basket = new Basket();
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">If item is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If count is not greater than 0.</exception>
        public void Add(Item item, int count = 1)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item is null");
            }

            _basket.Add(item, count);
        }

        /// <inheritdoc/>
        public decimal GetTotal()
        {
            return _pricingService.CalculateBasketPrice(_basket);
        }
    }
}
