using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace PeartreeGames.GamepadModule
{
    public class GamepadUIInputModule : BaseInputModule
    {
        [SerializeField] private float moveRepeatDelay = 0.5f;
        [SerializeField] private float moveRepeatRate = 0.1f;

        [SerializeField] private InputActionAsset actionsAsset;

        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference confirmAction;
        [SerializeField] private InputActionReference backAction;
        [SerializeField] private InputActionReference menuAction;
        [SerializeField] private InputActionReference optionAction;
        [SerializeField] private InputActionReference triggerLeftAction;
        [SerializeField] private InputActionReference triggerRightAction;
        [SerializeField] private InputActionReference bumperLeftAction;
        [SerializeField] private InputActionReference bumperRightAction;

        private NavigationModel _navigationState;
        private ButtonEventData buttonData;

        protected override void OnEnable()
        {
            base.OnEnable();
            HookActions();
            EnableActions();
        }

        public override void ActivateModule()
        {
            base.ActivateModule();
            var toSelect = eventSystem.currentSelectedGameObject;
            if (toSelect == null) toSelect = eventSystem.firstSelectedGameObject;
            eventSystem.SetSelectedGameObject(toSelect, GetBaseEventData());
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnhookActions();
            DisableActions();
        }

        private void EnableActions()
        {
            moveAction?.action?.Enable();
            confirmAction?.action?.Enable();
            backAction?.action?.Enable();
            menuAction?.action?.Enable();
            optionAction?.action?.Enable();
            triggerLeftAction?.action?.Enable();
            triggerRightAction?.action?.Enable();
            bumperLeftAction?.action?.Enable();
            bumperRightAction?.action?.Enable();
        }
        private void DisableActions()
        {
            moveAction?.action?.Disable();
            confirmAction?.action?.Disable();
            backAction?.action?.Disable();
            menuAction?.action?.Disable();
            optionAction?.action?.Disable();
            triggerLeftAction?.action?.Disable();
            triggerRightAction?.action?.Disable();
            bumperLeftAction?.action?.Disable();
            bumperRightAction?.action?.Disable();
        }

        private void HookActions()
        {
            HookAction(moveAction, OnMoveCallback);
            HookAction(confirmAction, OnConfirmCallback);
            HookAction(backAction, OnBackCallback);
            HookAction(menuAction, OnMenuCallback);
            HookAction(optionAction, OnOptionCallback);
            HookAction(triggerLeftAction, OnTriggerLeftCallback);
            HookAction(triggerRightAction, OnTriggerRightCallback);
            HookAction(bumperLeftAction, OnBumperLeftCallback);
            HookAction(bumperRightAction, OnBumperRightCallback);
        }
        private void UnhookActions()
        {
            UnhookAction(moveAction, OnMoveCallback);
            UnhookAction(confirmAction, OnConfirmCallback);
            UnhookAction(backAction, OnBackCallback);
            UnhookAction(menuAction, OnMenuCallback);
            UnhookAction(optionAction, OnOptionCallback);
            UnhookAction(triggerLeftAction, OnTriggerLeftCallback);
            UnhookAction(triggerRightAction, OnTriggerRightCallback);
            UnhookAction(bumperLeftAction, OnBumperLeftCallback);
            UnhookAction(bumperRightAction, OnBumperRightCallback);
        }
        
        private static void HookAction(InputAction action, Action<InputAction.CallbackContext> callback)
        {
            if (action == null) return;
            action.started += callback;
            action.performed += callback;
            action.canceled += callback;
        }
        
        private static void UnhookAction(InputAction action, Action<InputAction.CallbackContext> callback)
        {
            if (action == null) return;
            action.started -= callback;
            action.performed -= callback;
            action.canceled -= callback;
        }
        
        private BaseEventData GetButtonEventData(InputAction.CallbackContext ctx)
        {
            buttonData ??= new ButtonEventData(eventSystem);
            buttonData.Reset();
            buttonData.Phase = ctx.phase;
            return buttonData;
        }

        private void ExecuteButtonEvent<T>(ExecuteEvents.EventFunction<T> handler, BaseEventData eventData)
            where T : IEventSystemHandler
        {
            if (eventSystem.currentSelectedGameObject == null) return;
            ExecuteEvents.ExecuteHierarchy(eventSystem.currentSelectedGameObject, eventData, handler);
        }

        private void OnConfirmCallback(InputAction.CallbackContext ctx) =>
            ExecuteButtonEvent(GamepadExecuteEvents.ConfirmHandler, GetButtonEventData(ctx));
        private void OnBackCallback(InputAction.CallbackContext ctx) =>
            ExecuteButtonEvent(GamepadExecuteEvents.BackHandler, GetButtonEventData(ctx));
        private void OnMenuCallback(InputAction.CallbackContext ctx) =>
            ExecuteButtonEvent(GamepadExecuteEvents.MenuHandler, GetButtonEventData(ctx));
        private void OnOptionCallback(InputAction.CallbackContext ctx) =>
            ExecuteButtonEvent(GamepadExecuteEvents.OptionHandler, GetButtonEventData(ctx));
        private void OnTriggerLeftCallback(InputAction.CallbackContext ctx) =>
            ExecuteButtonEvent(GamepadExecuteEvents.TriggerLeftHandler, GetButtonEventData(ctx));
        private void OnTriggerRightCallback(InputAction.CallbackContext ctx) =>
            ExecuteButtonEvent(GamepadExecuteEvents.TriggerRightHandler, GetButtonEventData(ctx));
        private void OnBumperLeftCallback(InputAction.CallbackContext ctx) =>
            ExecuteButtonEvent(GamepadExecuteEvents.BumperLeftHandler, GetButtonEventData(ctx));
        private void OnBumperRightCallback(InputAction.CallbackContext ctx) =>
            ExecuteButtonEvent(GamepadExecuteEvents.BumperRightHandler, GetButtonEventData(ctx));

        private void OnMoveCallback(InputAction.CallbackContext ctx) =>
            _navigationState.Move = ctx.ReadValue<Vector2>();
        
        public override void Process()
        {
            ProcessNavigation(ref _navigationState);
        }

        private static MoveDirection GetMoveDirection(Vector2 vector)
        {
            if (vector.sqrMagnitude < Mathf.Epsilon) return MoveDirection.None;
            if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
                return vector.x > 0 ? MoveDirection.Right : MoveDirection.Left;
            return vector.y > 0 ? MoveDirection.Up : MoveDirection.Down;
        }

        // Modified version of InputSystemUIInputModule.ProcessNavigation
        // https://github.com/Unity-Technologies/InputSystem/blob/develop/Packages/com.unity.inputsystem/InputSystem/Plugins/UI/InputSystemUIInputModule.cs
        // https://unity3d.com/legal/licenses/Unity_Companion_License
        private void ProcessNavigation(ref NavigationModel navigationState)
        {
            var usedSelectionChange = false;
            if (eventSystem.currentSelectedGameObject != null)
            {
                var data = GetBaseEventData();
                ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
                usedSelectionChange = data.used;
            }

            if (!eventSystem.sendNavigationEvents) return;
            var movement = navigationState.Move;
            if (usedSelectionChange || Mathf.Approximately(movement.x, 0f) || Mathf.Approximately(movement.y, 0f))
            {
                navigationState.ConsecutiveMoveCount = 0;
                return;
            }

            var time = Time.unscaledTime;
            var moveDirection = GetMoveDirection(movement);

            if (moveDirection != navigationState.LastMoveDirection) navigationState.ConsecutiveMoveCount = 0;
            if (moveDirection == MoveDirection.None)
            {
                navigationState.ConsecutiveMoveCount = 0;
                return;
            }

            var allow = true;
            if (navigationState.ConsecutiveMoveCount != 0)
            {
                if (navigationState.ConsecutiveMoveCount > 1)
                    allow = time > navigationState.LastMoveTime + moveRepeatRate;
                else
                    allow = time > navigationState.LastMoveTime + moveRepeatDelay;
            }

            if (!allow) return;
            var eventData = navigationState.EventData;
            if (eventData == null)
            {
                eventData = new AxisEventData(eventSystem);
                navigationState.EventData = eventData;
            }

            eventData.Reset();
            eventData.moveVector = movement;
            eventData.moveDir = moveDirection;
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, eventData, ExecuteEvents.moveHandler);
            navigationState.ConsecutiveMoveCount += 1;
            navigationState.LastMoveTime = time;
            navigationState.LastMoveDirection = moveDirection;
        }
    }
}