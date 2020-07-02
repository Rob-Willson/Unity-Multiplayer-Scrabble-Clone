using System;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(PlayerManager))]
public class NetworkManagerJumble : NetworkManager
{
    public static NetworkManagerJumble instance = null;

    public static Action ClientJoinedServer;
    public static Action ClientLeftServer;

    public override void Awake()
    {
        base.Awake();
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection connection)
    {
        base.OnServerAddPlayer(connection);

        Debug.Log("OnServerAddPlayer: " + connection.identity);
        PlayerManager.instance.PromptClientGetAllExistingPlayerInstances();

        PlayerInstance newPlayerInstance = connection.identity.GetComponent<PlayerInstance>();
        if(newPlayerInstance == null)
        {
            Debug.LogError("FAIL: Added PlayerInstance of newly added Player was null.");
            return;
        }

        newPlayerInstance.TargetRequestScreenName(connection.identity.connectionToClient);
    }

    public override void OnClientConnect(NetworkConnection connection)
    {
        base.OnClientConnect(connection);

        Debug.Log("OnClientConnect: " + connection);
        ClientJoinedServer?.Invoke();
    }

    public override void OnServerDisconnect(NetworkConnection connection)
    {
        base.OnServerDisconnect(connection);

        Debug.Log("OnServerDisconnect: " + connection.identity);
        ClientLeftServer?.Invoke();
    }

    // HACK: Not a big fan of the use of magic strings here
    // Consider a cleaner alternative...
    public GameObject FindRegisteredPrefabByName(string name)
    {
        GameObject prefabObj = spawnPrefabs.Find(prefab => prefab.name == name);
        return prefabObj;
    }

}
