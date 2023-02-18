using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AndreyNosov.RelayRace.Game
{
    public class PlayerMouseInput : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        [SerializeField] private Camera _camera;

        public Action<Vector3> OnClick { get; set; }
        public Action<Vector3> OnMouseDown { get; set; }
        public Action<Vector3> OnMouseMove { get; set; }
        public Action<InputMouseData> OnMouseUp { get; set; }
        public Action<InputMouseData> OnLongPress { get; set; }

        private const float LongPressLength = 1.8f;
        private Vector3 _position = new Vector3();
        private bool _isNowPressed = false;
        private DateTime LastClickTime;

        private void Update()
        {
            if (GetTiming() >= LongPressLength && _isNowPressed)
            {
                OnLongPress?.Invoke(new InputMouseData(_position, GetTiming()));
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _position = _camera.ScreenToWorldPoint(eventData.position);
            OnClick?.Invoke(_position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            LastClickTime = DateTime.Now;
            _position = _camera.ScreenToWorldPoint(eventData.position);
            OnMouseDown?.Invoke(_position);
            _isNowPressed = true;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _position = _camera.ScreenToWorldPoint(eventData.position);
            OnMouseMove?.Invoke(_position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _position = _camera.ScreenToWorldPoint(eventData.position);
            OnMouseUp?.Invoke(new InputMouseData(_position, GetTiming()));
            _isNowPressed = false;
        }

        private float GetTiming()
        {
            return (float)(DateTime.Now - LastClickTime).TotalSeconds;
        }
    }
}
