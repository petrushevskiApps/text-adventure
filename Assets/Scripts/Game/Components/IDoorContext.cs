using System.Collections.Generic;

namespace Game.Components
{
    /// <summary>
    /// Defines a door item that can be unlocked with specific items
    /// and provides access to a target room.
    /// </summary>
    public interface IDoorContext: IItem
    {
        /// <summary>
        /// Gets the items required to unlock the door.
        /// </summary>
        IReadOnlyList<IItem> UnlockItems { get; }
        /// <summary>
        /// Gets the room that the door leads to.
        /// </summary>
        IRoom Room { get; }
        /// <summary>
        /// Gets a value indicating whether the door is unlocked.
        /// </summary>
        bool IsUnlocked { get; }
        /// <summary>
        /// Unlocks the door, allowing access to the connected room.
        /// </summary>
        void UnlockDoor();
    }
}