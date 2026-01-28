using Game.Components;
using UnityEngine;

namespace Game.Actions
{
    /// <summary>
    /// ScriptableObject action representing the narrator describing the current scene.
    /// When executed, it displays the action's message to the player via the Text Display.
    /// Part of the reusable action system for context-based interactions.
    /// </summary>
    [CreateAssetMenu(
        order = 1,
        fileName = "NarrateAction",
        menuName = "Game Data/Actions/Narrate Action")]
    public class NarrateAction : ActionBase<IItemContext>
    {
        public override ActionType Type => ActionType.Narrate;
        protected override void Execute(IItemContext context, string message, string negativeMessage)
        {
            context.TextDisplay.SetDisplayText(message);
        }
    }
}
