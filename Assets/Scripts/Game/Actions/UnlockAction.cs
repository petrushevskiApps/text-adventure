using Game.Components;
using UnityEngine;

namespace Game.Actions
{
    /// <summary>
    /// ScriptableObject action representing the player unlocking an item.
    /// When executed, it checks whether the item is already unlocked. 
    /// If the item is still locked, it verifies whether the player has all required items in the inventory. 
    /// If so, the item is unlocked and feedback is displayed to the player via the Text Display.
    /// Part of the reusable action system for context-based interactions.
    /// </summary>
    [CreateAssetMenu(
        order = 1,
        fileName = "UnlockAction",
        menuName = "Game Data/Actions/Unlock Action")]
    public class UnlockAction : ActionBase<IDoorContext>
    {
        public override ActionType Type => ActionType.Unlock;

        protected override void Execute(IDoorContext context, string message, string negativeMessage)
        {
            if (context.IsUnlocked)
            {
                context.TextDisplay.SetDisplayText($"{context.Name} is already unlocked!");
                return;
            }

            if (context.UnlockItems.Count == 0)
            {
                SetDoorUnlocked(context, message);
                return;
            }

            bool isUnlocked = true;
            foreach (IItem item in context.UnlockItems)
            {
                bool isInInventory = context.Inventory.ContainsItem(item.Name);
                if (!isInInventory)
                {
                    context.TextDisplay.SetDisplayText(negativeMessage);
                    isUnlocked = false;
                }

                break;
            }

            if (isUnlocked)
            {
                SetDoorUnlocked(context, message);
            }
        }

        private void SetDoorUnlocked(IDoorContext context, string message)
        {
            context.UnlockDoor();
            context.TextDisplay.SetDisplayText(message);
        }
    }
}