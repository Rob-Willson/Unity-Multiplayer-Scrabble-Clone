using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class UI_PlayerScreenName : MonoBehaviour
{
    [SerializeField] private TMP_InputField screenNameInputField = null;
    [SerializeField] private Image screenNameBorder = null;
    [SerializeField] private string screenName;

    private void OnEnable()
    {
        screenNameInputField.onValueChanged.AddListener(delegate { SubmitScreenName(); });
        screenNameInputField.onEndEdit.AddListener(delegate { SubmitScreenName(); });
        screenNameInputField.onSubmit.AddListener(delegate { SubmitScreenName(); });
        PlayerInstance.RequestingScreenName += NotifyServerOfPlayerScreenName;
    }
    private void OnDisable()
    {
        screenNameInputField.onValueChanged.RemoveListener(delegate { SubmitScreenName(); });
        screenNameInputField.onEndEdit.RemoveListener(delegate { SubmitScreenName(); });
        screenNameInputField.onSubmit.RemoveListener(delegate { SubmitScreenName(); });
        PlayerInstance.RequestingScreenName -= NotifyServerOfPlayerScreenName;
    }

    public bool CheckScreenNameSubmitted ()
    {
        if(IsValidScreenName(screenName))
        {
            return true;
        }
        screenNameBorder.color = Color.red;
        return false;
    }

    private bool IsValidScreenName(string input)
    {
        if(string.IsNullOrWhiteSpace(input))
        {
            return false;
        }
        return true;
    }

    private void SubmitScreenName ()
    {
        string newScreenName = screenNameInputField.text;
        if(!IsValidScreenName(newScreenName))
        {
            screenNameBorder.color = Color.red;
            return;
        }
        if(newScreenName == screenName)
        {
            return;
        }
        screenNameBorder.color = new Color32(255, 255, 255, 200);
        screenName = newScreenName;
    }

    private void NotifyServerOfPlayerScreenName(NetworkIdentity joiningClientPlayerIdentity)
    {
        Debug.Log("NotifyServerOfPlayerScreenName " + joiningClientPlayerIdentity);

        PlayerInstance playerInstance = joiningClientPlayerIdentity.GetComponent<PlayerInstance>();
        playerInstance.ScreenName.AssignScreenName(screenName);
    }

}
