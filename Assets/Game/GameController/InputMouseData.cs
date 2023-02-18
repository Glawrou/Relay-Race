using UnityEngine;

namespace AndreyNosov.RelayRace.Game
{
    public class InputMouseData
    {
        public Vector3 Position;
        public float PressingDuration;

        public InputMouseData(Vector3 position, float pressingDuration)
        {
            Position = position;
            PressingDuration = pressingDuration;
        }
    }
}
