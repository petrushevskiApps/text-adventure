namespace UserInterface
{
    /// <summary>
    /// Represents a UI element that displays feedback messages 
    /// to the player, describing game events and changes in the world state.
    /// </summary>
    public interface ITextDisplay
    {
        /// <summary>
        /// Updates the display with a feedback message for the player.
        /// </summary>
        /// <param name="textToDisplay">The message to display.</param>
        void SetDisplayText(string textToDisplay);
    }
}
