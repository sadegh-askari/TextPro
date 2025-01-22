using YoYo.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class TextProInputField : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextPro txtText;

    void Awake()
    {
        if (!Application.isPlaying)
            return;

        inputField.onValueChanged.AddListener(InputField_OnValueChanged);
        inputField.onSubmit.AddListener(InputField_OnValueChanged);
        InputField_OnValueChanged(inputField.text);
    }

    private void InputField_OnValueChanged(string text)
    {
        txtText.text = text;
    }

/*
#if UNITY_EDITOR
    public void OnGUI()
    {
        txtText.text = inputField.text;
    }
#endif*/
}
