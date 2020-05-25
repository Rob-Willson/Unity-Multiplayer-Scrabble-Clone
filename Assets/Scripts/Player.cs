using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [Client]
    private void Update()
    {
        if(!hasAuthority)
        {
            return;
        }

        if(Input.anyKeyDown)
        {
            CmdDealTile(1);
        }
    }

    private void Start()
    {
        
    }

    [Command]
    private void CmdDealTile(int count)
    {



    }




}
