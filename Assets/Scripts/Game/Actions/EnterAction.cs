using Game.Components;
using UnityEngine;

namespace Game.Actions
{
    /// <summary>
    /// ScriptableObject action representing the player passing through a door.
    /// Uses the Door context to switch the player's current room and 
    /// display feedback messages about the action's result.
    /// Part of the reusable action system for handling context-based interactions.
    /// </summary>
    [CreateAssetMenu(
        order = 1,
        fileName = "EnterAction",
        menuName = "Game Data/Actions/Enter Action")]
    public class EnterAction : ActionBase<IDoorContext>
    {
        public override ActionType Type => ActionType.Enter;

        protected override void Execute(IDoorContext context, string message, string negativeMessage)
        {
            if (context.IsUnlocked)
            {
                context.TextDisplay.SetDisplayText(message);
                context.TextDisplay.SetDisplayText($"--- {context.Room.Name} ---");
                context.PlayerContext.SetRoomContext(context.Room);
            }
            else
            {
                context.TextDisplay.SetDisplayText($"<color=red>{negativeMessage}</color>");
            }
        }
    }
}