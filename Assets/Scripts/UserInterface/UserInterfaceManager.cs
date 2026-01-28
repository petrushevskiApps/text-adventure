using System;
using System.Collections;
using Game.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UserInterface
{
    public class UserInterfaceManager
        : MonoBehaviour,
            ITextDisplay,
            INotificationDisplay
    {
        [SerializeField]
        private TextMeshProUGUI _displayText;
    
        [SerializeField]
        private TextMeshProUGUI _notificationText;
    
        [SerializeField]
        private TMP_InputField _inputField;
    
        [SerializeField]
        private ScrollRect _scroll;
    
        private Coroutine _notificationCoroutine;
    
        // Injected
        private IInputProcessor _inputProcessor;
        
        [Inject]
        public void Initialize(IInputProcessor inputProcessor)
        {
            _inputProcessor = inputProcessor;
        }
        
        private void OnEnable()
        {
            _inputField.onSubmit.AddListener(OnCommandSubmitted);
        }
    
        private void OnDisable()
        {
            _inputField.onSubmit.RemoveListener(OnCommandSubmitted);
        }
    
        private void OnCommandSubmitted(string inputText)
        {
            _inputProcessor.InputReceived(inputText);
            _inputField.text = string.Empty;
            StartCoroutine(ReselectNextFrame());
        }
    
        public void SetDisplayText(string textToDisplay)
        {
            _displayText.text += textToDisplay;
            _displayText.text += "\n\n";
            ScrollToBottom();
        }
    
        public void SetNotificationText(string message)
        {
            if (_notificationCoroutine != null)
            {
                StopCoroutine(_notificationCoroutine);
                _notificationCoroutine = null;
            }
            _notificationText.text = message;
            _notificationText.gameObject.SetActive(true);
            _notificationCoroutine = StartCoroutine(
                DelayAction(5, () =>
                {
                    _notificationText.gameObject.SetActive(false);
                }));
        }
    
        private IEnumerator DelayAction(float seconds, Action delay)
        {
            yield return new WaitForSecondsRealtime(seconds);
            delay?.Invoke();
        }
        
        private IEnumerator ReselectNextFrame()
        {
            yield return null;
            _inputField.ActivateInputField();
            EventSystem.current.SetSelectedGameObject(_inputField.gameObject);
        }
    
        private void ScrollToBottom()
        {
            StartCoroutine(ScrollNextFrame());
        }
    
        private IEnumerator ScrollNextFrame()
        {
            yield return null;
            _scroll.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
    }
}
