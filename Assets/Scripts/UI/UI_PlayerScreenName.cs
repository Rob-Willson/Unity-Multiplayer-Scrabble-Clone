﻿using UnityEngine;
using TMPro;
using Mirror;

public class UI_PlayerScreenName : MonoBehaviour
{
    [SerializeField] private TMP_InputField screenNameInputField = null;
    [SerializeField] private string screenName;

    private void OnEnable()
    {
        NetworkManagerJumble.ClientJoinedServer += NotifyServerOfPlayerScreenName;
    }
    private void OnDisable()
    {
        NetworkManagerJumble.ClientJoinedServer -= NotifyServerOfPlayerScreenName;
    }

    public void UICallback_SubmitScreenName ()
    {
        string newScreenName = screenNameInputField.text;
        if(!ScreenNameIsValid(newScreenName))
        {
            Debug.LogError("Screen name not valid in 'UICallback'");
            return;
        }
        screenName = newScreenName;
    }

    public void NotifyServerOfPlayerScreenName(NetworkIdentity joiningClientPlayerIdentity)
    {
        PlayerInstance playerInstance = joiningClientPlayerIdentity.GetComponent<PlayerInstance>();
        playerInstance.ScreenName.AssignScreenName(screenName);
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
