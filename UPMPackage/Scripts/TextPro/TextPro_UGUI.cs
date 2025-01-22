using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YoYo.UI
{
    [RequireComponent(typeof(Text))]
    public class TextPro_UGUI : TextPro
    {
        public Text textSource;

        public override void FindRequiredComponent()
        {
            if (textSource == null)
                textSource = GetComponent<Text>();
        }

        public override void SetText()
        {
            // if (textSource != null)
            // {
            //     // Populate base text in rect transform and calculate number of lines.
            //     string baseText = base.text;
            //     textSource.cachedTextGenerator.Populate(baseText, textSource.GetGenerationSettings(textSource.rectTransform.rect.size));
            //     // Make list of lines
            //     List<UILineInfo> lines = textSource.cachedTextGenerator.lines as List<UILineInfo>;
            //     if (lines == null)
            //     {
            //         textSource.text = "";
            //         return;
            //     }
            //
            //     StringBuilder linedText = new StringBuilder("");
            //     int lineCount = lines.Count;
            //     for (int i = 0; i < lineCount; i++)
            //     {
            //         // Find Start and Length of RTL line and append Line Ending character.
            //         if (i < lineCount - 1)
            //         {
            //             int startIndex = lines[i].startCharIdx;
            //             int length = lines[i + 1].startCharIdx - lines[i].startCharIdx;
            //             linedText.Insert(0, baseText.Substring(startIndex, length));
            //
            //             if (linedText.Length > 0 && linedText[linedText.Length - 1] != '\n' && linedText[linedText.Length - 1] != '\r')
            //                 linedText.Insert(0, "\n");
            //         }
            //         else
            //         {
            //             // For the Last line, we only need startIndex and line continues to the end.
            //             linedText.Insert(0, baseText.Substring(lines[i].startCharIdx));
            //         }
            //     }
            //
            //     textSource.text = TextProCore.Fix(linedText.ToString(), NumberMode);
            //}          
        }
    }
}