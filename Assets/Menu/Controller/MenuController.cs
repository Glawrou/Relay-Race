using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AndreyNosov.RelayRace.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button _buttonPlay;

        private const string GameSceneName = "Game";

        private void Awake()
        {
            _buttonPlay.onClick.AddListener(ClickPlayHandler);
        }

        private void ClickPlayHandler()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}
