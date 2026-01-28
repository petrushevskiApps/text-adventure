using System.Text;
using Game.Components;
using UnityEngine;

namespace Game.Actions
{
    /// <summary>
    /// ScriptableObject action representing the player examining a door lock,
    /// inheriting from the more general Look Action.
    /// Displays which items are required to unlock the door, 
    /// highlighting collected items in green and missing items in red.
    /// Part of the reusable action system for handling context-based interactions.
    /// </summary>
    [CreateAssetMenu(
        order = 1,
        fileName = "DoorLookAction",
        menuName = "Game Data/Actions/Door Look Action")]
    public class DoorLookAction : LookAction
    {
        protected override void AddItemToString(StringBuilder sb, IItemContext item, bool isLastItem)
        {
            string color = Context.Inventory.ContainsItem(item.Name)
                ? "green"
                : "red";
            sb.Append(isLastItem ? " and " : ", ");
            sb.Append($"<color={color}>{item.Name}</color>");
        }
    }
}