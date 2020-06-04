using System;
using UnityEngine;
using Mirror;

public class PlayerScreenName : NetworkBehaviour
{
    public static Action PlayerScreenNameChange;

    [SyncVar(hook = nameof(NotifyOfScreenNameChange))]
    [SerializeField] public string screenName;

    [Client]
    public void AssignScreenName (string newScreenName)
    {
        if(!hasAuthority)
        {
            return;
        }

        CmdSubmitScreenName(newScreenName);
    }

    [Command]
    public void CmdSubmitScreenName(string newScreenName)
    {
        screenName = newScreenName;
    }

    private void NotifyOfScreenNameChange (string oldValue, string newValue)
    {
        PlayerScreenNameChange?.Invoke();
    }


}
