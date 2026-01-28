using System;
using System.Collections.Generic;
using Game.Actions;
using Game.Components;
using Game.InputSystem;
using Game.InventorySystem;
using UserInterface;

namespace Game
{
    public class PlayerContext: IPlayerContext, IDisposable
    {
        // Internal
        private readonly Dictionary<string, IItem> _roomItems = new();
        private IRoom _roomContext;
        
        // Injected
        private readonly IInventory _inventory;
        private readonly ITextDisplay _textDisplay;
        private readonly IInputProcessor _inputProcessor;
        private readonly INotificationDisplay _notificationDisplay;
        
        public PlayerContext(
            IInventory inventory, 
            ITextDisplay textDisplay,
            IInputProcessor inputProcessor,
            INotificationDisplay notificationDisplay)
        {
            _inventory = inventory;
            _textDisplay = textDisplay;
            _inputProcessor = inputProcessor;
            _notificationDisplay = notificationDisplay;
            
            _inputProcessor.ActionCommandReadyEvent += OnInputReceived;
        }

        private void OnInputReceived(ActionType actionCommand, string itemKey)
        {
            if (!string.IsNullOrWhiteSpace(itemKey))
            {
                _roomItems.TryGetValue(itemKey.ToLower(), out IItem item);
                item ??= _inventory.GetItem(itemKey.ToLower());
                item?.ExecuteAction(actionCommand);
                if (item == null)
                {
                    _notificationDisplay.SetNotificationText($"There is no item with {itemKey}. Try again!");
                }
            }
            else
            {
                _roomContext.ExecuteAction(actionCommand);
            }
        }

        public void AddItems(IReadOnlyList<IItem> items)
        {
            foreach (IItem item in items)
            {
                if (_roomItems.TryAdd(item.Name.ToLower(), item))
                {
                    item.Setup(this, _inventory, _textDisplay);
                }
            }
        }

        public void SetRoomContext(IRoom roomContext)
        {
            _roomItems.Clear();
            _roomContext = roomContext;
            _roomContext.Setup(this, _inventory, _textDisplay);
            _roomContext.ExecuteAction(ActionType.Narrate);
        }

        public void Dispose()
        {
            _inputProcessor.ActionCommandReadyEvent -= OnInputReceived;
        }
    }
}