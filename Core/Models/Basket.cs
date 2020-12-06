using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Models
{

    /// <summary>
    /// Represents a shopping basket for a single user.
    /// 
    /// In order to make accessing the items in the basket easier this class implements the Enumerable interface. This will return 
    /// a <see cref="KeyValuePair{Item, int}"/> where the key is an Item in the basket and the value is the amount of items of that 
    /// type in the basket.
    /// </summary>
    public class Basket : IEnumerable<KeyValuePair<Item, int>>
    {

        private readonly Dictionary<Item, int> _items = new Dictionary<Item, int>();

        /// <summary>
        /// Returns the total number of items in the basket. This includes multiples of the same item.
        /// </summary>
        public int Count
        {
            get
            {
                return _items.Values.Sum();
            }
        }

        /// <summary>
        /// Returns the total amount of items matching <paramref name="item"/> in the basket.
        /// </summary>
        public int this[Item item]
        {
            get
            {
                return _items.GetValueOrDefault(item, 0);
            }
        }

        /// <summary>
        /// Returns the total amount of items where Item.SKU matches the given sku in the basket.
        /// </summary>
        public int this[string sku]
        {
            get
            {
                return _items.Where(x => x.Key.SKU.Equals(sku))
                    .Select(x => x.Value)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds the given amount of items to the basket.
        /// </summary>
        /// <param name="item">Item to add to the basket.</param>
        /// <param name="amount">Number of copies to add.</param>
        /// <exception cref="ArgumentNullException">If item is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If amount is not greater than 0.</exception>
        public void Add(Item item, int amount = 1)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Item is null");
            }

            if (amount < 1)
            {
                throw new ArgumentOutOfRangeException("Invalid amount. Must be greater than 0");
            }

            if (_items.ContainsKey(item))
            {
                _items[item] += amount;
            }
            else
            {
                _items.Add(item, amount);
            }
        }

        #region Enumerable Imp

        // Since we already save the items in a dictionary we may as well return the enumerator from that instead of 
        // having to implement our own.
        public IEnumerator<KeyValuePair<Item, int>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        // Must also implement IEnumerable.GetEnumerator, but implement as a private method.
        private IEnumerator GetEnumerator1()
        {
            return GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        #endregion
    }
}
