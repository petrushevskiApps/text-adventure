using System;
using Game.Actions;

namespace Game.InputSystem
{
    /// <summary>
    /// Receives composite input, breaks it into components,
    /// and converts it into Action Commands for use by other systems.
    /// </summary>
    public interface IInputProcessor
    {
        /// <summary>
        /// Raised when an Action Command has been received
        /// and is ready for use by subscribed systems.
        /// </summary>
        event Action<ActionType, string> ActionCommandReadyEvent;
        
        /// <summary>
        /// Receives and processes composite input, converting it into Action Commands.
        /// </summary>
        /// <param name="inputText">String-based composite input to process.</param>
        void InputReceived(string inputText);
    }
}