using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ScreenName : MonoBehaviour
{
    [SerializeField] private TMP_InputField screenNameInputField;

    public void UICallback_SubmitScreenName ()
    {
        SubmitScreenName();
    }

    private void SubmitScreenName ()
    {
        if(ScreenNameIsValid(screenNameInputField.text))
        {

        }
    }

    private bool ScreenNameIsValid(string input)
    {
        if(string.IsNullOrWhiteSpace(input))
        {
            return false;
        }
        return true;
    }
}
