using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    [CreateAssetMenu(
        order = 1,
        fileName = "Room",
        menuName = "Game Data/Components/Room")]
    public class Room: Item, IRoom
    {
        [SerializeField]
        [Tooltip("List of doors present in the room.")]
        private List<Door> _doors;
        
        public override IReadOnlyList<IItem> GetItems()
        {
            List<IItem> roomItems = new List<IItem>();
            roomItems.AddRange(base.GetItems());
            roomItems.AddRange(_doors);
            return roomItems;
        }
    }
}