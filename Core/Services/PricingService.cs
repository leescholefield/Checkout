﻿using Core.Models;
using Core.Repository;
using System;
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
                // check for promotion
                Promotion promo = _promoRepository.Get(entry.Key.SKU);
                if (promo != null)
                {
                    total += ApplyPromotion(promo, entry.Key, entry.Value);
                }
                else
                {
                    total += entry.Key.Price * entry.Value;
                }
            }

            return total;
        }

        private decimal ApplyPromotion(Promotion promo, Item item, int amount)
        {
            if (amount < promo.NumItemsRequired)
            {
                return item.Price * amount;
            }

            decimal total = 0;
            if (promo.GetType() == typeof(PercentagePromotion))
            {
                // convert promo percentoff to a decimal between 0 and 1
                decimal percentage = (((PercentagePromotion)promo).PercentOff / 100);
                // price of a batch (number of items required for promotion to apply) before promo is applied
                decimal batchPrice = item.Price * promo.NumItemsRequired;
                // how much we should deduct from batchPrice
                decimal batchDiscount = batchPrice * percentage;
                // price of a single batch multiplied by how many batches we have
                total = (batchPrice - batchDiscount) * (amount / promo.NumItemsRequired);
            }
            else if (promo.GetType() == typeof(SetPricePromotion))
            {
                total = ((SetPricePromotion)promo).Price * (amount / promo.NumItemsRequired);
            }
            else
            {
                throw new NotImplementedException("ApplyPromotion has not been implemented for promotion of type " + promo.GetType());
            }

            // add on price for any additional items
            if (amount % promo.NumItemsRequired != 0)
            {
                total += (amount % promo.NumItemsRequired) * item.Price;
            }

            return total;
        }
    }
}
