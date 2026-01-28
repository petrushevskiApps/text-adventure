using System;
using Game.Actions;
using UserInterface;

namespace Game.InputSystem
{
    public class InputProcessor: IInputProcessor
    {
        public event Action<ActionType, string> ActionCommandReadyEvent;

        // Injected
        private readonly INotificationDisplay _notificationDisplay;

        public InputProcessor(INotificationDisplay notificationDisplay)
        {
            _notificationDisplay = notificationDisplay;
        }
        
        public void InputReceived(string inputText)
        {
            string[] parts = inputText.Split(new[] { ' ' }, 2);

            if (!Enum.TryParse(parts[0], ignoreCase: true, out ActionType actionCommand))
            {
                _notificationDisplay.SetNotificationText("Action command is not valid! Try again!");
            }
            
            string itemKey = parts.Length > 1 ? parts[1] : string.Empty;
            ActionCommandReadyEvent?.Invoke(actionCommand, itemKey);
        }
    }
}