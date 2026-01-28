using System.Collections.Generic;
using Game.InventorySystem;
using UnityEngine;
using UserInterface;

namespace Game.Components
{
    /// <summary>
    /// A special type of item that can be locked and unlocked
    /// using a predefined set of items.
    /// </summary>
    [CreateAssetMenu(
        order = 2,
        fileName = "Door",
        menuName = "Game Data/Components/Door")]
    public class Door : Item, IDoorContext
    {
        [SerializeField]
        [Tooltip("The room on the other side of this door.")]
        private Room _room;

        [SerializeField]
        [Tooltip("Default starting state for the door.")]
        private bool _isUnlocked;
        
        public IRoom Room => _room;
        public IReadOnlyList<IItem> UnlockItems => base.GetItems();
        public bool IsUnlocked => _state.HasValue && _state.Value.IsUnlocked;
        
        private DoorState? _state;

        public override void Setup(IPlayerContext playerContext, IInventory inventory, ITextDisplay textDisplay)
        {
            base.Setup(playerContext, inventory, textDisplay);

            _state ??= new DoorState()
            {
                IsUnlocked = _isUnlocked
            };
        }

        public void UnlockDoor()
        {
            _state = new DoorState()
            {
                IsUnlocked = true
            };
        }
    }

    /// <summary>
    /// Represents the runtime state of a door, including whether it is unlocked.
    /// The state persists during play mode but is reset when the game restarts.
    /// This is required because ScriptableObjects retain their state between exiting and re-entering play mode.
    /// </summary>
    public struct DoorState
    {
        /// <summary>
        /// Gets or sets a value indicating whether the door is unlocked.
        /// </summary>
        public bool IsUnlocked { get; set; }
    }
}