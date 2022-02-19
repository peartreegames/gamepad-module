using UnityEngine;
using UnityEngine.EventSystems;

namespace PeartreeGames.GamepadModule
{
    public struct NavigationModel
    {
        public Vector2 Move;
        public int ConsecutiveMoveCount;
        public MoveDirection LastMoveDirection;
        public float LastMoveTime;
        public AxisEventData EventData;
    }
}