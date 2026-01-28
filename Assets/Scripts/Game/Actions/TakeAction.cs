using Game.Components;
using UnityEngine;

namespace Game.Actions
{
    /// <summary>
    /// ScriptableObject action representing the player taking an item.
    /// When executed, the item is added to the Inventory system, 
    /// and feedback is displayed to the player via the Text Display.
    /// Part of the reusable action system for context-based interactions.
    /// </summary>
    [CreateAssetMenu(
        order = 1,
        fileName = "TakeAction",
        menuName = "Game Data/Actions/Take Action")]
    public class TakeAction : ActionBase
    {
        public override ActionType Type => ActionType.Take;

        public override void Execute(IItem context, string message, string negativeMessage)
        {
            bool isStored = context.Inventory.StoreItem(context);
            context.TextDisplay.SetDisplayText(isStored
                ? string.Format(message, context.Name)
                : string.Format(negativeMessage, context.Name));
        }
    }
}