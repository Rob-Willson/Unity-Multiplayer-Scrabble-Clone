using System;
using UnityEngine;
using Mirror;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] private Canvas lobbyCanvas;

    private void OnEnable()
    {
        NetworkManagerJumble.ClientJoinedServer += Hide;
    }

    private void OnDisable()
    {
        NetworkManagerJumble.ClientJoinedServer -= Hide;
    }

    private void Hide()
    {
        Debug.Log("Hiding lobby UI");
        lobbyCanvas.enabled = false;
    }

}
