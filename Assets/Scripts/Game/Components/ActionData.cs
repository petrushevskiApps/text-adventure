using System;
using Game.Actions;
using UnityEngine;

namespace Game.Components
{
    /// <summary>
    /// Represents data for a specific action instance, including the action itself
    /// and messages displayed on success or failure.
    /// </summary>
    [Serializable]
    public struct ActionData
    {
        [SerializeField]
        [Tooltip("The action to execute on the item.")]
        public ActionBase Action;
    
        [SerializeField]
        [Tooltip("Message displayed when the action succeeds.")]
        public string Message;

        [SerializeField]
        [Tooltip("Message displayed when the action fails.")]
        public string NegativeMessage;
    }
}
