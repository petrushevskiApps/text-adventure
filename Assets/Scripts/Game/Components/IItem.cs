using Game.InventorySystem;
using UserInterface;

namespace Game.Components
{
    /// <summary>
    /// Defines a game item that can be interacted with
    /// and execute actions.
    /// </summary>
   public interface IItem
       : IActionExecutor,
           IItemContext
   {
       /// <summary>
       /// Injects the required references for enabling interaction with the item.
       /// </summary>
       /// <param name="playerContext">The player context.</param>
       /// <param name="inventory">The player’s inventory.</param>
       /// <param name="textDisplay">The text display used to show player actions.</param>
       void Setup(
           IPlayerContext playerContext, 
           IInventory inventory,
           ITextDisplay textDisplay);
   
   } 
}
