using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerScore : NetworkBehaviour, IReset
{
    public static Action<Player, int> PlayerScoreChange;

    [SerializeField] private int score;
    [SerializeField] private List<int> scoreLog;

    public void Reset ()
    {
        score = 0;
        scoreLog.Clear();
    }

    public void AddToScore (Player myPlayer, int amount)
    {
        score += amount;
        scoreLog.Add(amount);

        PlayerScoreChange?.Invoke(myPlayer, score);
        //RpcUpdateScore();
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
