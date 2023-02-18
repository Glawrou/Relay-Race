using UnityEngine;
using UnityEngine.UI;

namespace AndreyNosov.RelayRace.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerMouseInput _inputMouseData;
        [SerializeField] private CursorSphere _cursor;

        [Header("Win window")]
        [SerializeField] private GameObject _winWindow;
        [SerializeField] private Button[] _buttonGoMenu;
        [SerializeField] private Button[] _buttonReturn;
        [SerializeField] private Button[] _buttonNextLevel;

        [Header("Levels")]
        private Level _level;
        private int _currentLevelNumber = 1;
        [SerializeField] private Level[] _levels;

        private void Awake()
        {
            LoadLevel();
            _inputMouseData.OnMouseMove += _cursor.MoveMouseHandler;
            _inputMouseData.OnMouseDown += _cursor.MouseDownHandle;
            _inputMouseData.OnMouseUp += _cursor.MouseUpHandle;
            _level.OnWin += WinHandler;
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

        private void LoadLevel()
        {
            _inputMouseData.gameObject.SetActive(true);
            _level = Instantiate(_levels[_currentLevelNumber - 1], transform);
        }

        private void WinHandler()
        {
            _inputMouseData.gameObject.SetActive(false);
            _winWindow.SetActive(true);
        }

        private void ClickGoMenuHanler()
        {

        }

        private void ClickNextLevelHanler()
        {
            Destroy(_level.gameObject);
            _level = null;
            _winWindow.SetActive(false);
            _currentLevelNumber++;
            LoadLevel();
        }

        private void ClickReturnLevel()
        {
            Destroy(_level.gameObject);
            _level = null;
            _winWindow.SetActive(false);
            LoadLevel();
        }
    }
}
