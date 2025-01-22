#if UNITY_EDITOR
using UnityEditor.Localization;
#endif
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

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
    
    [CreateAssetMenu(menuName = "Localization/TextPro LocalizationSettings", fileName = "TextProLocalizationSettings")]
    public class TextProLocalizationSettings : ScriptableObject
    {
        public LocalizationUser user; 
        [SerializeField] private int _counter;

        public int GetLocalizeKeyIndex()
        {
            if (_counter == 0)
                _counter = (int)user;
            
            return _counter++;
        }
    }
    
    public enum LocalizationUser
    {
        Sadegh = 1,
        Parsa = 1000000,
        Akbar = 2000000,
        Kourosh = 3000000,
        Sina = 4000000,
    }
}