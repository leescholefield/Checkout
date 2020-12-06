namespace Core.Repository
{

    /// <summary>
    /// Responsible for retrieving items of a single type from a data store.
    /// </summary>
    /// <typeparam name="T">The type of item that the repository contains.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Returns a single item of type T.
        /// </summary>
        /// <param name="id">Id of the item to search for. For the purposes of this project we are using an Items SKU for both 
        /// the item's ID and the Promotions ID.
        /// </param>
        T Get(string id);
    }
}
