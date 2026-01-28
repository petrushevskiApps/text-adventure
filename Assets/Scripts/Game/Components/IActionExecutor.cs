using Game.Actions;

namespace Game.Components
{
    /// <summary>
    /// Defines the ability to execute actions.
    /// </summary>
    public interface IActionExecutor
    {
        /// <summary>
        /// Executes an action of the specified type if supported.
        /// </summary>
        /// <param name="type">The type of action to execute.</param>
        void ExecuteAction(ActionType type);
    }
}