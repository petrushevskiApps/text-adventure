using Game.Components;
using UnityEngine;

namespace Game.Actions
{
    /// <summary>
    /// ScriptableObject action representing the player reading an item's message.
    /// When executed, it displays the item's message to the player via the Text Display.
    /// Part of the reusable action system for context-based interactions.
    /// </summary>
    [CreateAssetMenu(
        order = 1,
        fileName = "ReadAction",
        menuName = "Game Data/Actions/Read Action")]
    public class ReadAction : ActionBase<IItemContext>
    {
        public override ActionType Type => ActionType.Read;

        protected override void Execute(IItemContext context, string message, string negativeMessage)
        {
            context.TextDisplay.SetDisplayText($"The {context.Name} reads: <i><color=orange>{message}</color></i>");
        }
    }
}