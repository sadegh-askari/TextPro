using System;
using System.Collections.Generic;
using UnityEditor.Localization;
using UnityEngine;

namespace YoYo.UI
{
    [CreateAssetMenu(menuName = "Localization/TextPro LocalizationCollection", fileName = "TextProLocalizationCollection")]
    public class TextProLocalizationCollection : ScriptableObject
    {
        [SerializeField] private LocalizationUser _activeUser;
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
            if (_localizationSettings == null || (int)_activeUser == 0) return null;
            return _localizationSettings.Find(settings => settings.user == _activeUser);
        }
    }
}