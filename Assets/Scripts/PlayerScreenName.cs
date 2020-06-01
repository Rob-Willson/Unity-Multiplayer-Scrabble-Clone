using UnityEngine;
using Mirror;

public class PlayerScreenName : NetworkBehaviour
{
    //[SyncVar(hook = nameof(NotifyOfScreenNameChange))]
    [SerializeField] public string screenName;

    [Client]
    public void AssignScreenName (string newScreenName)
    {
        if(!hasAuthority)
        {
            Debug.Log("out");
            return;
        }

        Debug.Log("AssignScreenName : '" + newScreenName + "'");
        CmdSubmitScreenName(newScreenName);
    }

    [Command]
    public void CmdSubmitScreenName(string newScreenName)
    {
        Debug.Log("Command done... " + newScreenName);
        ProcessScreenNameChange(newScreenName);
    }

    [Server]
    private void ProcessScreenNameChange(string newScreenName)
    {
        Debug.Log("Processing..." + newScreenName); 
        RpcNotifyAllClientsOfChangedName(newScreenName);
    }

    [ClientRpc]
    private void RpcNotifyAllClientsOfChangedName(string newScreenName)
    {
        this.screenName = newScreenName;
        Debug.Log("RPC reply notifying of screen name change to: '" + newScreenName + "'...");
    }

}
