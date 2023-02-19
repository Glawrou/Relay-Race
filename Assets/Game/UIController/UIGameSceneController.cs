using System;
using UnityEngine;
using UnityEngine.UI;

namespace AndreyNosov.RelayRace.Game
{
    public class UIGameSceneController : MonoBehaviour
    {
        public Action OnClickGoMenu;
        public Action OnNextLevel;
        public Action OnReturnLevel;

        [SerializeField] private GameObject _playerMouseInput;
        [SerializeField] private GameObject _educationWindow;
        [SerializeField] private GameObject _winWindow;
        [SerializeField] private Button _buttonCloseEducation;
        [SerializeField] private Button[] _buttonGoMenu;
        [SerializeField] private Button[] _buttonReturn;
        [SerializeField] private Button[] _buttonNextLevel;

        private void Awake()
        {
            _playerMouseInput.SetActive(false);
            _buttonCloseEducation.onClick.AddListener(CloseEducationHandler);
            foreach (var button in _buttonGoMenu)
            {
                button.onClick.AddListener(ClickGoMenuHanler);
            }

            foreach (var button in _buttonNextLevel)
            {
                button.onClick.AddListener(ClickNextLevelHanler);
            }

            foreach (var button in _buttonReturn)
            {
                button.onClick.AddListener(ClickReturnLevel);
            }
        }

        public void OpenWinWindow()
        {
            _playerMouseInput.gameObject.SetActive(false);
            _winWindow.SetActive(true);
        }

        public void CloseWinWindow()
        {
            _playerMouseInput.gameObject.SetActive(true);
            _winWindow.SetActive(false);
        }

        private void CloseEducationHandler()
        {
            _playerMouseInput.SetActive(true);
            _educationWindow.SetActive(false);
        }

        private void ClickGoMenuHanler()
        {
            OnClickGoMenu?.Invoke();
        }

        private void ClickNextLevelHanler()
        {
            OnNextLevel?.Invoke();
        }

        private void ClickReturnLevel()
        {
            OnReturnLevel?.Invoke();
        }
    }
}
