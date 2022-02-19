using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace PeartreeGames.GamepadModule.Editor
{
    [CustomEditor(typeof(GamepadUIInputModule))]
    public class GamepadUIInputModuleEditor : UnityEditor.Editor
    {
        private SerializedProperty _moveRepeatDelayProperty;
        private SerializedProperty _moveRepeatRateProperty;
        private SerializedProperty _actionsAssetProperty;

        private static readonly string[] ActionNames =
        {
            "moveAction",
            "confirmAction",
            "backAction",
            "menuAction",
            "optionAction",
            "triggerLeftAction",
            "triggerRightAction",
            "bumperLeftAction",
            "bumperRightAction"
        };

        private static InputActionReference[] GetAssetReferencesFromAssetDatabase(InputActionAsset actions)
        {
            if (actions == null) return Array.Empty<InputActionReference>();
            var assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(actions));
            return assets.Where(asset => asset is InputActionReference).Cast<InputActionReference>()
                .OrderBy(x => x.name).ToArray();
        }

        private void OnEnable()
        {
            _moveRepeatDelayProperty = serializedObject.FindProperty("moveRepeatDelay");
            _moveRepeatRateProperty = serializedObject.FindProperty("moveRepeatRate");
            _actionsAssetProperty = serializedObject.FindProperty("actionsAsset");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var elem = new VisualElement();
            var moveRepeatDelayField = new PropertyField(_moveRepeatDelayProperty);
            moveRepeatDelayField.Bind(serializedObject);
            elem.Add(moveRepeatDelayField);

            var moveRepeatRateField = new PropertyField(_moveRepeatRateProperty);
            moveRepeatRateField.Bind(serializedObject);
            elem.Add(moveRepeatRateField);

            var actionsAssetField = new PropertyField(_actionsAssetProperty);
            actionsAssetField.Bind(serializedObject);
            elem.Add(actionsAssetField);

            var assetReferences =
                GetAssetReferencesFromAssetDatabase(_actionsAssetProperty.objectReferenceValue as InputActionAsset);
            foreach (var action in ActionNames)
            {
                var prop = serializedObject.FindProperty(action);
                var dropdown = new DropdownField(prop.displayName)
                {
                    choices = assetReferences.Select(actionReference => actionReference.name).ToList(),
                    value = assetReferences.FirstOrDefault(actionReference =>
                        actionReference.name == prop.objectReferenceValue?.name)?.name
                };
                dropdown.RegisterCallback<ClickEvent>(_ =>
                {
                    assetReferences =
                        GetAssetReferencesFromAssetDatabase(_actionsAssetProperty.objectReferenceValue as InputActionAsset);
                    dropdown.choices = assetReferences.Select(actionReference => actionReference.name).ToList();
                });

                dropdown.RegisterValueChangedCallback(changed =>
                {
                    prop.objectReferenceValue =
                        assetReferences.FirstOrDefault(assetReference => assetReference.name == changed.newValue);
                    serializedObject.ApplyModifiedProperties();
                });
                elem.Add(dropdown);
            }
            return elem;
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}