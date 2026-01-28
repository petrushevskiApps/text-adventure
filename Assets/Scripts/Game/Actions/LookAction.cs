using System.Collections.Generic;
using System.Text;
using Game.Components;
using UnityEngine;
using UserInterface;

namespace Game.Actions
{
    /// <summary>
    /// ScriptableObject action representing the player examining or investigating an item.
    /// Iterates over the item's child elements, records discovered items in the Player context,
    /// and provides feedback to the player via the Text Display.
    /// Part of the reusable action system for context-based interactions.
    /// </summary>
    [CreateAssetMenu(
        order = 1,
        fileName = "LookAction",
        menuName = "Game Data/Actions/Look Action")]
    public class LookAction : ActionBase<IItemContext>
    {
        public override ActionType Type => ActionType.Look;
    
        protected IItemContext Context;

        protected override void Execute(IItemContext actionContext, string message, string negativeMessage)
        {
            Context = actionContext;
            IReadOnlyList<IItem> items = actionContext.GetItems();
            if (items.Count != 0)
            {
                actionContext.PlayerContext.AddItems(items);
                Display(message, items, actionContext.TextDisplay);
            }
            else
            {
                actionContext.TextDisplay.SetDisplayText(string.Format(negativeMessage, actionContext.Name));
            }
        }

        private void Display(string message, IReadOnlyList<IItem> items, ITextDisplay textDisplay)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                AddItemToString(sb, items[i], i == items.Count - 1 && items.Count > 1);
            }
            textDisplay.SetDisplayText(string.Format(message, sb));
        }

        protected virtual void AddItemToString(StringBuilder sb, IItemContext item, bool isLastItem)
        {
            sb.Append(isLastItem ? " and " : ", ");
            sb.Append($"<color=green>{item.Name}</color> ");
            sb.Append(item.Description);
        }
    }
}