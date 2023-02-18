using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class Cursor : MonoBehaviour
    {
        private MeshRenderer _renderer;

        private const float AcceptableHeight = 1;

        public void MoveMouseHandler(Vector3 input)
        {
            transform.position = new Vector3(input.x, AcceptableHeight, input.z);
        }

        public void MouseDownHandle(Vector3 input)
        {
            CheckRenderer();
            _renderer.material.color = Color.red;
        }

        public void MouseUpHandle(InputMouseData inputMouseData)
        {
            CheckRenderer();
            _renderer.material.color = Color.white;
        }

        private void CheckRenderer()
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<MeshRenderer>();
            }
        }
    }
}
