namespace UserInterface
{
    /// <summary>
    /// Defines a UI element that displays short notification messages
    /// to the player, such as invalid commands or error feedback.
    /// </summary>
    public interface INotificationDisplay
    {
        /// <summary>
        /// Displays a notification message to the player.
        /// </summary>
        /// <param name="message">The notification text to display.</param>
        void SetNotificationText(string message);
    }
}