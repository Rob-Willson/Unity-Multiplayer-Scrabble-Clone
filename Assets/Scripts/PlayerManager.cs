using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager instance = null;

    public static Action NewPlayerLoggedAsConnected;
    public List<PlayerInstance> allConnectedPlayers = new List<PlayerInstance>();

    public void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [Server]
    public void PromptClientGetAllExistingPlayerInstances ()
    {
        RpcGetAllExistingPlayerInstances();
    }
    
    [ClientRpc]
    public void RpcGetAllExistingPlayerInstances ()
    {
        Debug.Log("RPC: GetAllExistingPlayerInstances");

        GetAllExistingPlayerInstances();
    }

    [Client]
    public void GetAllExistingPlayerInstances ()
    {
        Debug.Log("GetAllExistingPlayerInstances on connections: " + NetworkServer.connections.Count);

        // HACK: It feels like there *MUST* be a better way to do this
        //       Surely Mirror has an in-built way to handle this sort of thing?
        var allPlayerInstances = FindObjectsOfType<PlayerInstance>();

        foreach(var instance in allPlayerInstances)
        {
            AddPlayerInstance(instance);
        }
        allConnectedPlayers.Sort((x, y) => string.Compare(x.ScreenName.screenName, y.ScreenName.screenName));

        Debug.Log("Found " + allConnectedPlayers.Count + " connected players");

        StopAllCoroutines();
        StartCoroutine(AlphabetizeAllConnectedPlayers());
    }

    private void AddPlayerInstance (PlayerInstance newPlayerInstance)
    {
        if(allConnectedPlayers.Contains(newPlayerInstance))
        {
            Debug.LogWarning("FAIL: Player was already included in the list of connected players. Bug?");
            return;
        }
        allConnectedPlayers.Add(newPlayerInstance);
    }

    // HACK:
    private IEnumerator AlphabetizeAllConnectedPlayers()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        allConnectedPlayers.Sort((x, y) => string.Compare(x.ScreenName.screenName, y.ScreenName.screenName));
        NewPlayerLoggedAsConnected?.Invoke();
        Debug.Log("AlphabetizeAllConnectedPlayers done");
        yield break;
    }

    //private void RemovePlayerInstance (PlayerInstance removedPlayerInstance)
    //{
    //    if(!allConnectedPlayers.Contains(removedPlayerInstance))
    //    {
    //        Debug.LogError("FAIL: Disconnected player couldn't be found in the collection of connected players.");
    //        return;
    //    }
    //    allConnectedPlayers.Remove(removedPlayerInstance);
    //}

    public PlayerInstance GetLocalPlayerInstance ()
    {
        Debug.Log("Connected players: " + allConnectedPlayers.Count);

        if(allConnectedPlayers.Count == 0)
        {
            GetAllExistingPlayerInstances();
        }

        foreach(var player in allConnectedPlayers)
        {
            if(player.isLocalPlayer)
            {
                Debug.Log("localPlayer");
                return player;
            }
        }
        return null;
    }

}
