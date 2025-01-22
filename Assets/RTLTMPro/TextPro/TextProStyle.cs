using System;
using System.Collections;

using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

#if UNITY_EDITOR
using UnityEditor;
#endif


[RequireComponent(typeof(TMP_Text))]
public class TextProStyle : MonoBehaviour
{
    public TextStyle Style;
    public StyleSettings AdvanceSettings;
    [SerializeField, HideInInspector] private TMP_Text _txtReference;
    [SerializeField] private TMP_Text _txtTarget;

    private void Awake()
    {
        SetStyle();
    }
    
    private async void SetStyle()
    {
        if (_txtTarget == null) return;

        if (_txtReference == null || !_txtReference.name.Equals(Style.ToString()))
        {
            Debug.LogError($"Load Style {Style}");
            var referenceObj = await Addressables.LoadAssetAsync<GameObject>(Style.ToString()).Task;
            if (referenceObj != null)
                _txtReference = referenceObj.GetComponent<TMP_Text>();
        }

        if (_txtReference == null)
        {
            Debug.LogError($"Cant load text style: {Style}");
            return;
        }

        if (AdvanceSettings.OverrideFont)
        {
            _txtTarget.font = _txtReference.font;
            _txtTarget.fontStyle = _txtReference.fontStyle;
            _txtTarget.fontSharedMaterial = _txtReference.fontSharedMaterial;
        }
        
        if (AdvanceSettings.OverrideSize)
        {
            _txtTarget.enableAutoSizing = _txtReference.enableAutoSizing;
            _txtTarget.fontSizeMin = _txtReference.fontSizeMin;
            _txtTarget.fontSizeMax = _txtReference.fontSizeMax;
            _txtTarget.fontSize = _txtReference.fontSize;
        }

        if (AdvanceSettings.OverrideColor)
            _txtTarget.color = _txtReference.color;

        #if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
        Canvas.ForceUpdateCanvases();
        #endif

    }

    private void Reset()
    {
        _txtTarget = GetComponent<TMP_Text>();
        #if UNITY_EDITOR
        UnityEditorInternal.ComponentUtility.MoveComponentUp(this);
        #endif
    }

    [Serializable]
    public class StyleSettings
    {
        public bool OverrideFont = true;
        public bool OverrideSize = true;
        public bool OverrideColor = true;
    }

    // private IEnumerable GetNames()
    // {
    //     var list = new ValueDropdownList<int>();
    //
    //     var names = Enum.GetNames(typeof(TextStyle));
    //     for (var index = 0; index < names.Length; index++)
    //     {
    //         list.Add(names[index].Replace("_", "/"), index);
    //     }
    //
    //     return list;
    // }

    public enum TextStyle
    {
        Primary_Medium_BigDisplay,
        Primary_Medium_Display,
        Primary_Medium_Title,
        Primary_Medium_Header,
        Primary_Medium_Body,
        Primary_Medium_SubBody,
        Primary_Medium_Sub,

        Primary_Bold_BigDisplay,
        Primary_Bold_Display,
        Primary_Bold_Title,
        Primary_Bold_Header,
        Primary_Bold_Body,
        Primary_Bold_SubBody,
        Primary_Bold_Sub,

        Secondary_Medium_BigDisplay,
        Secondary_Medium_Display,
        Secondary_Medium_Title,
        Secondary_Medium_Header,
        Secondary_Medium_Body,
        Secondary_Medium_SubBody,
        Secondary_Medium_Sub,

        Secondary_Bold_BigDisplay,
        Secondary_Bold_Display,
        Secondary_Bold_Title,
        Secondary_Bold_Header,
        Secondary_Bold_Body,
        Secondary_Bold_SubBody,
        Secondary_Bold_Sub,

        Tertiary_Medium_BigDisplay,
        Tertiary_Medium_Display,
        Tertiary_Medium_Title,
        Tertiary_Medium_Header,
        Tertiary_Medium_Body,
        Tertiary_Medium_SubBody,
        Tertiary_Medium_Sub,

        Tertiary_Bold_BigDisplay,
        Tertiary_Bold_Display,
        Tertiary_Bold_Title,
        Tertiary_Bold_Header,
        Tertiary_Bold_Body,
        Tertiary_Bold_SubBody,
        Tertiary_Bold_Sub,

        Orange_Medium_BigDisplay,
        Orange_Medium_Display,
        Orange_Medium_Title,
        Orange_Medium_Header,
        Orange_Medium_Body,
        Orange_Medium_SubBody,
        Orange_Medium_Sub,

        Orange_Bold_BigDisplay,
        Orange_Bold_Display,
        Orange_Bold_Title,
        Orange_Bold_Header,
        Orange_Bold_Body,
        Orange_Bold_SubBody,
        Orange_Bold_Sub,

        White_Medium_BigDisplay,
        White_Medium_Display,
        White_Medium_Title,
        White_Medium_Header,
        White_Medium_Body,
        White_Medium_SubBody,
        White_Medium_Sub,

        White_Bold_BigDisplay,
        White_Bold_Display,
        White_Bold_Title,
        White_Bold_Header,
        White_Bold_Body,
        White_Bold_SubBody,
        White_Bold_Sub,
    }
}