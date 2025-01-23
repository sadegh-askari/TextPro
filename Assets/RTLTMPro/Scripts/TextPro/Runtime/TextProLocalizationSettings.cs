using UnityEngine;

namespace Hexagon.UI
{
    [CreateAssetMenu(menuName = "Localization/TextPro LocalizationSettings", fileName = "TextProLocalizationSettings")]
    public class TextProLocalizationSettings : ScriptableObject
    {
        public string UserName; 
        [SerializeField] private int _counter;

        public int GetLocalizeKeyIndex()
        {
            return _counter++;
        }
    }
}