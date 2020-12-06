namespace Core.Models
{
    /// <summary>
    /// Represents a single item in our store.
    /// 
    /// -----
    /// Why use a constructor?
    /// Since an Item would be in an invalid state if either SKU or Price is not set, I have decided to use a constructor instead of 
    /// just setting the Properties directly.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Stock-keeping unit. For the proposes of this project it acts as both an UID and a display name. In a real application 
        /// these would be seperate.
        /// </summary>
        public string SKU { get; protected set; }

        /// <summary>
        /// Cost of a single item.
        /// </summary>
        public decimal Price { get; protected set; }

        /// <summary>
        /// Public constructor. 
        /// </summary>
        /// <param name="sku">stick-keeping unit. Also acts as an ID for the item.</param>
        /// <param name="price">Cost of a single item.</param>
        public Item(string sku, decimal price)
        {
            SKU = sku;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Item other = (Item)obj;
                return (Price == other.Price) && (SKU == other.SKU);
            }
        }

        public override int GetHashCode()
        {
            return Price.GetHashCode() ^ SKU.GetHashCode();
        }

    }
}
