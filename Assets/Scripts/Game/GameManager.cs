using Game.Actions;
using Game.Components;
using Game.InventorySystem;
using UnityEngine;
using UserInterface;
using Zenject;

namespace Game
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("First door that triggers the experience. Root node")]
        private Door _tutorialDoor;

        // Injected
        private IPlayerContext _playerContext;
        private IInventory _inventory;
        private ITextDisplay _textDisplay;
        
        [Inject]
        private void Initialize(IPlayerContext playerContext, IInventory inventory, ITextDisplay textDisplay)
        {
            _playerContext = playerContext;
            _inventory = inventory;
            _textDisplay = textDisplay;
        }
        private void Start()
        {
            _tutorialDoor.Setup(_playerContext, _inventory, _textDisplay);
            _tutorialDoor.ExecuteAction(ActionType.Enter);
            _tutorialDoor.Room.ExecuteAction(ActionType.Look);
        }
    }
}