using System.Collections.Generic;
using Game.Components;

namespace Game
{
    /// <summary>
    /// Defines the player’s ability to remember items seen in a room
    /// and store them for later use within that room ( context ).
    /// </summary>
    public interface IPlayerContext
    {
        /// <summary>
        /// Adds items observed in the current room to the player’s memory.
        /// </summary>
        /// <param name="items">The items found in the room.</param>
        void AddItems(IReadOnlyList<IItem> items);
        
        /// <summary>
        /// Sets the current room context,
        /// clearing memory of items from the previous room
        /// that were not stored in the inventory.
        /// </summary>
        /// <param name="roomContext">The context of the new room.</param>
        void SetRoomContext(IRoom roomContext);
    }
}