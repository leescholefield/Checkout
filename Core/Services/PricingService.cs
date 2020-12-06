using Core.Models;
using Core.Repository;
using System;

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
            throw new NotImplementedException();
        }
    }
}
