using System.Collections.Generic;
using Game;
using Game.InventorySystem;
using UserInterface;

namespace Game.Components
{
    /// <summary>
    /// Defines a context for an item that can be described,
    /// displayed, and interacted with through player actions.
    /// </summary>
    public interface IItemContext
    {
        /// <summary>
        /// Gets the unique name or key of the item.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the description of the item.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Gets the text display component used to show the result of actions
        /// executed by or over this item.
        /// </summary>
        ITextDisplay TextDisplay { get; }
        /// <summary>
        /// Gets the context of the player interacting with the item.
        /// </summary>
        IPlayerContext PlayerContext { get; }
        /// <summary>
        /// Gets the player’s inventory acting upon this item.
        /// </summary>
        IInventory Inventory { get; }
        /// <summary>
        /// Gets the child items contained within this item.
        /// </summary>
        IReadOnlyList<IItem> GetItems();
    }
}
