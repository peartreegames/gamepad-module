using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace PeartreeGames.GamepadModule
{
    public class ButtonEventData : BaseEventData
    {
        public InputActionPhase Phase;
        public ButtonEventData(EventSystem eventSystem) : base(eventSystem)
        {
            Phase = InputActionPhase.Disabled;
        }

        public override void Reset()
        {
            base.Reset();
            Phase = InputActionPhase.Disabled;
        }
    }
}