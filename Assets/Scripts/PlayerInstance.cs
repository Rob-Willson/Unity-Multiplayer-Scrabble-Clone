using System;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerScore))]
[RequireComponent(typeof(PlayerScreenName))]
public class PlayerInstance : NetworkBehaviour
{
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

    private void Start()
    {
        Initialize();
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        if(!hasAuthority)
        {
            return;
        }

        Initialize();
    }

    public void Initialize ()
    {

    }

    [Command]
    public void CmdAddToScore (int count)
    {
        // TODO: Validate...
        // ...
        Debug.Log("CmdAddToScore"); 
        Score.AddToScore(count);
    }

    [Command]
    public void CmdRequestTileBagShuffle ()
    {
        Debug.Log("CmdRequestTileBagShuffle");
        RequestTileBagShuffle();
    }

    [Server]
    public void RequestTileBagShuffle()
    {
        Debug.Log("RequestTileBagShuffle");
        // TODO: Validate...
        // ...

        TileBag tileBag = FindObjectOfType<TileBag>();
        tileBag.ShuffleTilesInBag();
    }

}
