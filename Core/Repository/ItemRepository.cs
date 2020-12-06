using Core.Models;
using System.Collections.Generic;

namespace Core.Repository
{

    /// <summary>
    /// A Repository for retrieving instances of <see cref="Item"/> from the database.
    /// </summary>
    public class ItemRepository : IRepository<Item>
    {
        // In a real project I'd have them in an actual table in a database but for the purposes of this demonstration 
        // I'll just keep them in a dict.
        private static readonly Dictionary<string, Item> _items = new Dictionary<string, Item>
        {
            {"A", new Item("A", 10)},
            {"B", new Item("B", 15)},
            {"C", new Item("C", 40)},
            {"D", new Item("D", 55)}
        };


        public Item Get(string id)
        {
            return _items.GetValueOrDefault(id, null);
        }
    }
}
