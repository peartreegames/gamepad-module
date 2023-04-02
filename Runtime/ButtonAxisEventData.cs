using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace PeartreeGames.GamepadModule
{
    public class ButtonAxisEventData : AxisEventData
    {
        public InputActionPhase Phase;
        public ButtonAxisEventData(EventSystem eventSystem) : base(eventSystem)
        {
            Phase = InputActionPhase.Disabled;
        }
    }
}