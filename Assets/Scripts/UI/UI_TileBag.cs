using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UI_TileBag : NetworkBehaviour
{
    [Client]
    public void UICallback_Shuffle ()
    {
        //TileBag.instance.CmdRequestShuffleTilesInBag();

        PlayerInstance localPlayerInstance = PlayerManager.instance.GetLocalPlayerInstance();
        if(localPlayerInstance == null)
        {
            Debug.LogError("FAIL: No local PlayerInstance found.");
            return;
        }

        Debug.Log("LOCAL PLAYER INSTANCE IS: " + localPlayerInstance.gameObject.name);

        localPlayerInstance.CmdRequestTileBagShuffle();

    }

}
