#if UNITY_EDITOR
using UnityEditor.Localization;
#endif
using UnityEngine;
namespace YoYo.UI
{
    [CreateAssetMenu(menuName = "Localization/TextPro LocalizationSettings", fileName = "TextProLocalizationSettings")]
    public class TextProLocalizationSettings : ScriptableObject
    {
        [SerializeField] private int _counter;

        [SerializeField] private bool _overrideLocalize;

        #if UNITY_EDITOR
        [SerializeField] private StringTableCollection _stringTable;
        public StringTableCollection StringTable => _stringTable;
        #endif

        [SerializeField] private string[] _locales;
        public bool OverrideLocalization => _overrideLocalize;
        public string[] Locales => _locales;

        public int GetLocalizeKeyIndex()
        {
            return _counter++;
        }
    }
}