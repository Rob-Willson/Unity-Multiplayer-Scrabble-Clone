using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerScore : NetworkBehaviour, IReset
{
    [SerializeField] private int score;
    [SerializeField] private List<int> scoreLog;

    public void Reset ()
    {
        score = 0;
        scoreLog.Clear();
    }

    public void AddToScore (int amount)
    {
        score += amount;
        scoreLog.Add(amount);
    }

}
