using UnityEngine.EventSystems;

namespace PeartreeGames.GamepadModule
{
    public static class GamepadExecuteEvents
    {
        public static ExecuteEvents.EventFunction<IConfirmHandler> ConfirmHandler { get; } = Execute;
        private static void Execute(IConfirmHandler handler, BaseEventData eventData) =>
            handler.OnConfirm(ExecuteEvents.ValidateEventData<ButtonEventData>(eventData));
        
        public static ExecuteEvents.EventFunction<IBackHandler> BackHandler { get; } = Execute;
        private static void Execute(IBackHandler handler, BaseEventData eventData) =>
            handler.OnBack(ExecuteEvents.ValidateEventData<ButtonEventData>(eventData));
        
        public static ExecuteEvents.EventFunction<IMenuHandler> MenuHandler { get; } = Execute;
        private static void Execute(IMenuHandler handler, BaseEventData eventData) =>
            handler.OnMenu(ExecuteEvents.ValidateEventData<ButtonEventData>(eventData));
        
        public static ExecuteEvents.EventFunction<IOptionHandler> OptionHandler { get; } = Execute;
        private static void Execute(IOptionHandler handler, BaseEventData eventData) =>
            handler.OnOption(ExecuteEvents.ValidateEventData<ButtonEventData>(eventData));
        
        public static ExecuteEvents.EventFunction<ITriggerHandler> TriggerLeftHandler { get; } = ExecuteLeft;
        private static void ExecuteLeft(ITriggerHandler handler, BaseEventData eventData) =>
            handler.OnTriggerLeft(ExecuteEvents.ValidateEventData<ButtonEventData>(eventData));
                
        public static ExecuteEvents.EventFunction<ITriggerHandler> TriggerRightHandler { get; } = ExecuteRight;
        private static void ExecuteRight(ITriggerHandler handler, BaseEventData eventData) =>
            handler.OnTriggerRight(ExecuteEvents.ValidateEventData<ButtonEventData>(eventData));
        
        public static ExecuteEvents.EventFunction<IBumperHandler> BumperLeftHandler { get; } = ExecuteLeft;
        private static void ExecuteLeft(IBumperHandler handler, BaseEventData eventData) =>
            handler.OnBumperLeft(ExecuteEvents.ValidateEventData<ButtonEventData>(eventData));
                
        public static ExecuteEvents.EventFunction<IBumperHandler> BumperRightHandler { get; } = ExecuteRight;
        private static void ExecuteRight(IBumperHandler handler, BaseEventData eventData) =>
            handler.OnBumperRight(ExecuteEvents.ValidateEventData<ButtonEventData>(eventData));
    }
}