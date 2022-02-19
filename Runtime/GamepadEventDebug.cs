using UnityEngine;

namespace PeartreeGames.GamepadModule
{
    public class GamepadEventDebug : MonoBehaviour, IConfirmHandler, IBackHandler, ITriggerHandler, IBumperHandler, IMenuHandler, IOptionHandler
    {
        public void OnConfirm(ButtonEventData eventData)
        {
            Debug.Log($"{gameObject.name}:OnConfirm:{eventData.Phase}");
        }

        public void OnBack(ButtonEventData eventData)
        {
            Debug.Log($"{gameObject.name}:OnBack:{eventData.Phase}");
        }

        public void OnTriggerLeft(ButtonEventData eventData)
        {
            Debug.Log($"{gameObject.name}:OnTriggerLeft:{eventData.Phase}");
        }

        public void OnTriggerRight(ButtonEventData eventData)
        {
            Debug.Log($"{gameObject.name}:OnTriggerRight:{eventData.Phase}");
        }

        public void OnBumperLeft(ButtonEventData eventData)
        {
            Debug.Log($"{gameObject.name}:OnBumperLeft:{eventData.Phase}");
        }

        public void OnBumperRight(ButtonEventData eventData)
        {
            Debug.Log($"{gameObject.name}:OnBumperRight:{eventData.Phase}");
        }

        public void OnMenu(ButtonEventData eventData)
        {
            Debug.Log($"{gameObject.name}:OnMenu:{eventData.Phase}");
        }

        public void OnOption(ButtonEventData eventData)
        {
            Debug.Log($"{gameObject.name}:OnOption:{eventData.Phase}");
        }
    }
}