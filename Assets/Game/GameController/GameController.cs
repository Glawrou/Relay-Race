using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerMouseInput _inputMouseData;
        [SerializeField] private CursorSphere _tester;

        private void Awake()
        {
            _inputMouseData.OnMouseMove += _tester.MoveMouseHandler;
            _inputMouseData.OnMouseDown += _tester.MouseDownHandle;
            _inputMouseData.OnMouseUp += _tester.MouseUpHandle;
        }
    }
}
