using Game.Components;
using UnityEngine;

namespace Game.Actions
{
    /// <summary>
    /// Abstract base class for defining actions as ScriptableObjects. 
    /// Allows multiple action instances to be added to a single executable list in the inspector, 
    /// enabling flexible and reusable action execution on item contexts.
    /// </summary>
    public abstract class ActionBase : ScriptableObject
    {
        public abstract ActionType Type { get; }
        public abstract void Execute(IItem itemContext, string message, string negativeMessage);
    }
    
    /// <summary>
    /// Provides a base class that encapsulates an action's context,
    /// ensuring that actions only access the methods they need.
    /// </summary>
    /// <typeparam name="TContext">
    /// The specific context required by the action.
    /// <example><see cref="IDoorContext"/> represents the context for door items.</example>
    /// </typeparam>
    public abstract class ActionBase<TContext> : ActionBase
        where TContext : IItemContext
    {
        public override void Execute(IItem itemContext, string message, string negativeMessage)
        {
            if (itemContext is not TContext typedContext)
            {
                return;
            }
            Execute(typedContext, message, negativeMessage);
        }
    
        protected abstract void Execute(TContext context, string message, string negativeMessage);
    }
}
