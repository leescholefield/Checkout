using Core.Models;
using Core.Repository;

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

            return total;
        }
    }
}
