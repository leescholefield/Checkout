using Core.Models;
using Core.Repository;
using System.Collections.Generic;

namespace Core.Services
{
    /// <inheritdoc cref="IPricingService"/>
    public class PricingService : IPricingService
    {

        private readonly IRepository<Promotion> _promoRepository;


        public PricingService(IRepository<Promotion> promoRepository)
        {
            _promoRepository = promoRepository;
        }

        public decimal CalculateBasketPrice(Basket basket)
        {
            decimal total = 0;

            foreach(KeyValuePair<Item, int> entry in basket)
            {
                total += entry.Key.Price;
            }

            return total;
        }
    }
}
