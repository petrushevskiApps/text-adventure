using Game.Components;

namespace Game.InventorySystem
{
    /// <summary>
    /// Manages the player’s inventory, allowing items to be carried
    /// across rooms or scenes without being lost and reused later.
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item to store.</param>
        /// <returns>
        /// <c>true</c> if the item was successfully stored; 
        /// <c>false</c> if the item was already present.
        /// </returns>
        bool StoreItem(IItem item);
        
        /// <summary>
        /// Retrieves an item from the inventory by its key.
        /// </summary>
        /// <param name="itemKey">The unique key of the requested item.</param>
        /// <returns>
        /// The item with the given key, or <c>null</c> if not found.
        /// </returns>
        IItem GetItem(string itemKey);
        
        /// <summary>
        /// Checks whether the inventory contains an item with the given key.
        /// </summary>
        /// <param name="key">The unique key of the item to check for.</param>
        /// <returns>
        /// <c>true</c> if the inventory contains an item with the specified key;
        /// otherwise, <c>false</c>.
        /// </returns>
        bool ContainsItem(string key);
    }
}