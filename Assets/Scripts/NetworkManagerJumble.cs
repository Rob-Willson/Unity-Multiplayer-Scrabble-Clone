using System;
using UnityEngine;
using Mirror;

public class NetworkManagerJumble : NetworkManager
{
    public static NetworkManagerJumble instance = null;

    public static Action ClientJoinedServer;

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

    [SerializeField] private int minimumPlayerCount = 2;


    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        Debug.Log("OnClientConnect");
        ClientJoinedServer?.Invoke();
    }

    // HACK: Not a big fan of the use of magic strings here
    // Consider a cleaner alternative...
    public GameObject FindRegisteredPrefabByName (string name)
    {
        GameObject prefabObj = spawnPrefabs.Find(prefab => prefab.name == name);
        return prefabObj;
    }

}
