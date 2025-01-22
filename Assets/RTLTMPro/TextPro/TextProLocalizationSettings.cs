using UnityEngine;
using UnityEngine.Localization.Settings;

namespace YoYo.UI
{
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