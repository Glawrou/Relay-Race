using UnityEngine;
using UnityEngine.SceneManagement;

namespace AndreyNosov.RelayRace.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerMouseInput _inputMouseData;
        [SerializeField] private CursorSphere _cursor;
        [SerializeField] private UIGameSceneController _uIGameSceneController;

        private const string MenuSceneName = "Menu";

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
            _uIGameSceneController.OnClickGoMenu += ClickGoMenuHanler;
            _uIGameSceneController.OnNextLevel += ClickNextLevelHanler;
            _uIGameSceneController.OnReturnLevel += ClickReturnLevel;
            _level.OnWin += WinHandler;
        }

        private void LoadLevel()
        {
            _inputMouseData.gameObject.SetActive(true);
            _level = Instantiate(_levels[_currentLevelNumber - 1], transform);
        }

        private void WinHandler()
        {
            _uIGameSceneController.OpenWinWindow();
        }

        private void ClickGoMenuHanler()
        {
            SceneManager.LoadScene(MenuSceneName);
        }

        private void ClickNextLevelHanler()
        {
            if (_currentLevelNumber == _levels.Length)
            {
                ClickGoMenuHanler();
            }

            Destroy(_level.gameObject);
            _level = null;
            _uIGameSceneController.CloseWinWindow();
            _currentLevelNumber++;
            LoadLevel();
        }

        private void ClickReturnLevel()
        {
            Destroy(_level.gameObject);
            _level = null;
            _uIGameSceneController.CloseWinWindow();
            LoadLevel();
        }
    }
}
