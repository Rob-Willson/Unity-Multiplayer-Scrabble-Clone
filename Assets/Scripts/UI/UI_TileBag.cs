using UnityEngine;
using Mirror;

public class UI_TileBag : NetworkBehaviour
{
    [Client]
    public void UICallback_Shuffle ()
    {
        PlayerInstance localPlayerInstance = PlayerManager.instance.GetLocalPlayerInstance();
        if(localPlayerInstance == null)
        {
            Debug.LogError("FAIL: No local PlayerInstance found.");
            return;
        }

        localPlayerInstance.CmdRequestTileBagShuffle();
    }

}
