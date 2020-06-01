using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerScore : NetworkBehaviour
{
    public static Action PlayerScoreChange;

    [SyncVar(hook = nameof(NotifyOfScoreChange))]
    [SerializeField] public int score;
    [SerializeField] private List<int> scoreLog = new List<int>();

    [Server]
    public void AddToScore (int amount)
    {
        Debug.Log("PlayerScore.AddToScore");
        if(amount == 0)
        {
            return;
        }

        scoreLog.Add(amount);
        score += amount;
    }

    private void NotifyOfScoreChange (int oldScore, int newScore)
    {
        Debug.Log("SCORE CHANGED...");
        PlayerScoreChange?.Invoke();
    }

}
