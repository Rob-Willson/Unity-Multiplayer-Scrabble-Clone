using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UI_TileBag : NetworkBehaviour
{
    [Client]
    public void UICallback_Shuffle ()
    {
        TileBag.instance.CmdRequestShuffleTilesInBag();
    }

}
