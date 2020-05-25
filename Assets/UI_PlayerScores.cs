using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class UI_PlayerScores : NetworkBehaviour
{
    public List<TextMeshProUGUI> allPlayerScoreTexts = new List<TextMeshProUGUI>();

    private void OnEnable()
    {
        PlayerScore.PlayerScoreChange += UpdatePlayerScore;
    }
    private void OnDisable()
    {
        PlayerScore.PlayerScoreChange -= UpdatePlayerScore;
    }

    private void UpdatePlayerScore (Player player, int newScore)
    {
        Debug.Log("Updating score of player: ... ");


        RpcUpdatePlayerScore(0, newScore);
    }


    [ClientRpc]
    private void RpcUpdatePlayerScore (int playerIndex, int newScore)
    {
        allPlayerScoreTexts[playerIndex].SetText("Player X: " + newScore);


        if(Camera.main.backgroundColor == Color.yellow)
        {
            Camera.main.backgroundColor = Color.red;
        }
        else
        {
            Camera.main.backgroundColor = Color.yellow;
        }
    }

}
