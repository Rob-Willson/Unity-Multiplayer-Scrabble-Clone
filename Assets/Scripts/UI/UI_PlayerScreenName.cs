using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void NotifyServerOfPlayerScreenName()
    {
        PlayerInstance localPlayerInstance = PlayerManager.instance.GetLocalPlayerInstance();
        if(localPlayerInstance == null)
        {
            Debug.LogError("FAIL: No local PlayerInstance found.");

            //StartCoroutine(QueueUpScreenNameChangeReminder());

            return;
        }

        Debug.Log("LOCAL PLAYER INSTANCE IS: " + localPlayerInstance.gameObject.name);
        localPlayerInstance.ScreenName.AssignScreenName(screenName);
    }

    private IEnumerator QueueUpScreenNameChangeReminder()
    {
        Debug.Log("IEnumerator");
        yield return new WaitForSecondsRealtime(1f);
        NotifyServerOfPlayerScreenName();
        yield return null;
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
