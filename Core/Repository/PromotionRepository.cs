using Core.Models;
using System.Collections.Generic;

namespace Core.Repository
{
    /// <summary>
    /// A Repository for retrieving instances of <see cref="Promotion"/> from the database.
    /// </summary>
    public class PromotionRepository : IRepository<Promotion>
    {

        // In a real project I'd have them in an actual table in a database but for the purposes of this demonstration 
        // I'll just keep them in a dict.
        private static readonly Dictionary<string, Promotion> _promos = new Dictionary<string, Promotion>
        {
            {"B", new SetPricePromotion("B", 3, 40)},
            {"D", new PercentagePromotion("D", 2, 25)}
        };


        public Promotion Get(string id)
        {
            return _promos.GetValueOrDefault(id, null);
        }
    }
}
