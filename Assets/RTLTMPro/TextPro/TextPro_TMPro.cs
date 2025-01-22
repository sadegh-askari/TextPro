using System;
using RTLTMPro;
using TMPro;
using UnityEngine;

namespace YoYo.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextPro_TMPro : TextPro
    {
        public TMP_Text textSource;
        public FontWeight FontWeight;
        public bool farsi;
        public bool fixTags;
        public bool preserveNumbers;

        protected override void Start()
        {
            base.Start();
            textSource.fontWeight = FontWeight;
            FindRequiredComponent();
        }

        public override void FindRequiredComponent()
        {
            try
            {
                if (textSource == null && gameObject != null)
                {
                    textSource = GetComponent<TMP_Text>();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public override void SetText()
        {
#if UNITY_EDITOR
            textSource.fontWeight = FontWeight;
#endif

            FindRequiredComponent();
            UpdateText();
        }


        private void UpdateText()
        {
            _text ??= "";

            if (TextUtils.IsRTLInput(_text) == false)
            {
                textSource.isRightToLeftText = false;
                textSource.text = text;
            }
            else
            {
                textSource.isRightToLeftText = true;
                textSource.text = GetFixedText(text);
            }
        }

        private readonly FastStringBuilder finalText = new(RTLSupport.DefaultBufferSize);
        private string GetFixedText(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            finalText.Clear();
            RTLSupport.FixRTL(input, finalText, farsi, fixTags, preserveNumbers);
            finalText.Reverse();
            return finalText.ToString();
        }
    }
}