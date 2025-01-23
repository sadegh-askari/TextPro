#if UNITY_EDITOR && UNITY_LOCALIZATION
using System;
using System.Collections.Generic;
using UnityEditor.Localization;
using UnityEngine;

namespace Hexagon.UI
{
    [CreateAssetMenu(menuName = "Localization/TextPro LocalizationCollection", fileName = "TextProLocalizationCollection")]
    public class TextProLocalizationCollection : ScriptableObject
    {
        [SerializeField] private string _activeUser;
        [SerializeField] private List<TextProLocalizationSettings> _localizationSettings;

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
            var settings = GetActiveSettings();
            if (settings == null)
            {
                throw new NullReferenceException("Cannot find Localization setting for active user");
            }

            return settings.GetLocalizeKeyIndex();
        }

        private TextProLocalizationSettings GetActiveSettings()
        {
            if (_localizationSettings == null || string.IsNullOrEmpty(_activeUser)) return null;
            return _localizationSettings.Find(settings => settings.UserName.Equals(_activeUser));
        }
    }
}
#endif