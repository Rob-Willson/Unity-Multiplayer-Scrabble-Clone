using System;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerScore))]
public class Player : NetworkBehaviour
{
    private PlayerScore PlayerScore;

    [Client]
    private void Update()
    {
        if(!hasAuthority)
        {
            return;
        }

        if(Input.anyKeyDown)
        {
            Debug.Log("Key pressed");
            CmdAddToScore(1);
        }
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize ()
    {
        PlayerScore = GetComponent<PlayerScore>();
        if(PlayerScore == null)
        {
            Debug.LogError("FAIL: '" + this.GetType().ToString() + "' could not find required component: 'PlayerScore'");
        }
    }

    [Command]
    private void CmdDealTile(int count)
    {

    }

    [Command]
    public void CmdAddToScore (int count)
    {
        // TODO: Validate...
        // ...
        Debug.Log("CmdAddToScore");
        PlayerScore.AddToScore(this, count);
    }


    //[ClientRpc]
    //private void RpcUpdateScore ()
    //{
    //    if(Camera.main.backgroundColor == Color.yellow)
    //    {
    //        Camera.main.backgroundColor = Color.red;
    //    }
    //    else
    //    {
    //        Camera.main.backgroundColor = Color.yellow;
    //    }
    //}

}
