using RTLTMPro;
using YoYo.UI;
using UnityEditor;
using UnityEngine;

public class TextProCoreTool : EditorWindow
{
    string rawText;
    string fixedText;

    bool showTashkeel = true;
    bool useHinduNumbers = true;
    NumberMode numberMode;

    // Add menu item named "Arabic Support Tool" to the Tools menu
    [MenuItem("Tools/TextPro Tool")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(TextProCoreTool));
    }

    private readonly FastStringBuilder finalText = new(RTLSupport.DefaultBufferSize);
    private string GetFixedText(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        finalText.Clear();
        RTLSupport.FixRTL(input, finalText, true, true, true);
        return finalText.ToString();
    }
    
    void OnGUI()
    {
        if (string.IsNullOrEmpty(rawText))
        {
            fixedText = "";
        }
        else
        {
            fixedText = GetFixedText(rawText); //TextProCore.Fix(rawText, numberMode);
        }

        GUILayout.Label("Options:", EditorStyles.boldLabel);
        numberMode = (NumberMode)EditorGUILayout.EnumPopup("Number Mode", numberMode);// ("Fix Text Tags", showTashkeel);
        //useHinduNumbers = EditorGUILayout.Toggle("Use Hindu Numbers", useHinduNumbers);

        GUILayout.Label("Input (Not Fixed)", EditorStyles.boldLabel);
        rawText = EditorGUILayout.TextArea(rawText);

        GUILayout.Label("Output (Fixed)", EditorStyles.boldLabel);
        fixedText = EditorGUILayout.TextArea(fixedText);
        if (GUILayout.Button("Copy"))
        {
            var tempTextEditor = new TextEditor();
            tempTextEditor.text = fixedText;
            tempTextEditor.SelectAll();
            tempTextEditor.Copy();
        }
    }
}