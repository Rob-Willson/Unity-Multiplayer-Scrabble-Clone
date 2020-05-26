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


}
