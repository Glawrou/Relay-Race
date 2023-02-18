using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace AndreyNosov.RelayRace.Game
{
    public class Level : MonoBehaviour
    {
        public Action OnWin;
        [SerializeField] public Point[] _points;

        private void Start()
        {
            foreach (var point in _points)
            {
                point.OnReceived += ReceivedHandler;
            }
        }

        private void ReceivedHandler(Point point)
        {
            var list = new List<Point>();
            list.AddRange(_points);
            list.Remove(point);
            if (list.Count == 0)
            {
                WinHandler();
            }

            _points = list.ToArray();
        }

        private void WinHandler()
        {
            OnWin?.Invoke();
        }
    }
}
