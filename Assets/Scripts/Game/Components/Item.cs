using System.Collections.Generic;
using System.Linq;
using Game.Actions;
using Game.InventorySystem;
using JetBrains.Annotations;
using UnityEngine;
using UserInterface;

namespace Game.Components
{
    [CreateAssetMenu(
        order = 3,
        fileName = "Item",
        menuName = "Game Data/Components/Item")]
    public class Item : ScriptableObject, IItem
    {
        [field: SerializeField] 
        [field: Tooltip("The unique name of the item, used as a key in room context.")]
        public string Name { get; [UsedImplicitly] private set; }
    
        [field: SerializeField] 
        [field: Tooltip("The description of the item, shown when the player examines it.")]
        public string Description { get; [UsedImplicitly] private set; }
    
        [field: SerializeField]
        [field: Tooltip("Child items contained within this item.")]
        private List<Item> _items;
    
        [SerializeField]
        [field: Tooltip("Actions that can be executed on this item.")]
        private List<ActionData> _itemActions;
        
        public IPlayerContext PlayerContext { get; private set; }
        public IInventory Inventory { get; private set; }
        public ITextDisplay TextDisplay { get; private set; }
    
        public virtual void Setup(
            IPlayerContext playerContext, 
            IInventory inventory,
            ITextDisplay textDisplay)
        {
            PlayerContext = playerContext;
            TextDisplay = textDisplay;
            Inventory = inventory;
        }
    
        public void ExecuteAction(ActionType type)
        {
            ActionData actionData = _itemActions.FirstOrDefault(action => action.Action.Type == type);
            if (actionData.Action != null)
            {
                actionData.Action.Execute(this, actionData.Message, actionData.NegativeMessage);
            }
        }
    
        public virtual IReadOnlyList<IItem> GetItems()
        {
            return _items.Cast<IItem>().ToList();
        }
    }
}
