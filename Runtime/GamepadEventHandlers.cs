using UnityEngine.EventSystems;

namespace PeartreeGames.GamepadModule
{
    // Same as ISubmitHandler but with ButtonEventData
    public interface IConfirmHandler : IEventSystemHandler
    {
        void OnConfirm(ButtonEventData eventData);
    }

    // Same as ICancelHandler but with ButtonEventData
    public interface IBackHandler : IEventSystemHandler
    {
        void OnBack(ButtonEventData eventData);
    }

    public interface IMenuHandler : IEventSystemHandler
    {
        void OnMenu(ButtonEventData eventData);
    }

    public interface IOptionHandler : IEventSystemHandler
    {
        void OnOption(ButtonEventData eventData);
    }

    public interface ITriggerHandler : IEventSystemHandler
    {
        void OnTriggerLeft(ButtonEventData eventData);
        void OnTriggerRight(ButtonEventData eventData);
    }

    public interface IBumperHandler : IEventSystemHandler
    {
        void OnBumperLeft(ButtonEventData eventData);
        void OnBumperRight(ButtonEventData eventData);
    }
    
    public interface IDpadHandler : IEventSystemHandler
    {
        void OnDPad(ButtonAxisEventData eventData);
    }
}