using TMPro;
using UnityEngine;

namespace YoYo.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextPro_TMProUI : TextPro
    {
        public override void FindRequiredComponent()
        {
            gameObject.AddComponent<TextPro_TMPro>();
            DestroyImmediate(this);
        }
    }
}
