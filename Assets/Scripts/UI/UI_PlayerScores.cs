using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class UI_PlayerScores : NetworkBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> allPlayerScoreTexts = new List<TextMeshProUGUI>();

    private void OnEnable()
    {
        NetworkManagerJumble.ClientJoinedServer += UpdatePlayerScores;
        NetworkManagerJumble.ClientLeftServer += UpdatePlayerScores;
        PlayerScore.PlayerScoreChange += UpdatePlayerScores;
        PlayerScreenName.PlayerScreenNameChange += UpdatePlayerScores;
        PlayerManager.NewPlayerLoggedAsConnected += UpdatePlayerScores;
    }
    private void OnDisable()
    {
        NetworkManagerJumble.ClientJoinedServer -= UpdatePlayerScores;
        NetworkManagerJumble.ClientLeftServer -= UpdatePlayerScores;
        PlayerScore.PlayerScoreChange -= UpdatePlayerScores;
        PlayerScreenName.PlayerScreenNameChange -= UpdatePlayerScores;
        PlayerManager.NewPlayerLoggedAsConnected -= UpdatePlayerScores;
    }

    private void UpdatePlayerScores()
    {
        for(int i = 0; i < allPlayerScoreTexts.Count; i++)
        {
            if(i >= PlayerManager.instance.allConnectedPlayers.Count)
            {
                allPlayerScoreTexts[i].transform.parent.gameObject.SetActive(false);
                continue;
            }

            PlayerInstance playerInstance = PlayerManager.instance.allConnectedPlayers[i];
            if(playerInstance == null)
            {
                Debug.Log("PlayerInstance could not be found on connection. So client disconnected but was not removed from list of all connected players");
                allPlayerScoreTexts[i].transform.parent.gameObject.SetActive(false);
                continue;
            }

            allPlayerScoreTexts[i].SetText(playerInstance.ScreenName.screenName + " :  " + playerInstance.Score.score);
            allPlayerScoreTexts[i].transform.parent.gameObject.SetActive(true);
        }

        StopAllCoroutines();
        StartCoroutine(ResetLayoutGroup());
    }

    // HACK: Fix for an issue caused by the combined use of nested layout groups and content size fitter
    private IEnumerator ResetLayoutGroup()
    {
        yield return new WaitForEndOfFrame();
        VerticalLayoutGroup verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
        verticalLayoutGroup.CalculateLayoutInputHorizontal();
        verticalLayoutGroup.CalculateLayoutInputVertical();
        verticalLayoutGroup.SetLayoutHorizontal();
        verticalLayoutGroup.SetLayoutVertical();
        yield break;
    }

    public void IncrementScore(int playerIndex, int amount)
    {
        if(PlayerManager.instance.allConnectedPlayers[playerIndex] == null)
        {
            return;
        }
        PlayerManager.instance.allConnectedPlayers[playerIndex].CmdAddToScore(amount);
    }

}
