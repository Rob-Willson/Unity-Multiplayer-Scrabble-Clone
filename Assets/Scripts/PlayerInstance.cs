using System;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerScore))]
[RequireComponent(typeof(PlayerScreenName))]
public class PlayerInstance : NetworkBehaviour
{
    public static Action<NetworkIdentity> RequestingScreenName;

    private PlayerScore score;
    public PlayerScore Score
    {
        get
        {
            if(score == null)
            {
                score = GetComponent<PlayerScore>();
            }
            return score;
        }
    }

    private PlayerScreenName screenName;
    public PlayerScreenName ScreenName
    {
        get
        {
            if(screenName == null)
            {
                screenName = GetComponent<PlayerScreenName>();
            }
            return screenName;
        }
    }

    [Client]
    private void Update()
    {
        if(!hasAuthority)
        {
            return;
        }

        if(Input.anyKeyDown)
        {
            CmdAddToScore(1);
        }
    }

    [Command]
    public void CmdAddToScore (int count)
    {
        // TODO: Validate...

        Score.AddToScore(count);
    }

    [Command]
    public void CmdRequestTileBagShuffle ()
    {
        // TODO: Validate...

        TileBag tileBag = FindObjectOfType<TileBag>();
        tileBag.ShuffleTilesInBag();
    }

    [TargetRpc]
    public void TargetRequestScreenName (NetworkConnection target)
    {
        Debug.Log("TargetRequestScreenName");

        if(!string.IsNullOrWhiteSpace(ScreenName.screenName))
        {
            Debug.LogError("ScreenName has already been set on PlayerInstance requesting screen name: " + this.gameObject.name);
            return;
        }
        RequestingScreenName?.Invoke(this.netIdentity);
    }

}
