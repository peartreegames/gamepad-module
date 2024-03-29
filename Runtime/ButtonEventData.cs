﻿using UnityEngine.EventSystems;
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
    }
}