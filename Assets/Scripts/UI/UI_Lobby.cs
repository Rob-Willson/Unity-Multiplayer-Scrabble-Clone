using UnityEngine;
using Mirror;
using TMPro;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] private Canvas lobbyCanvas;
    [SerializeField] private NetworkManager networkManager;

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


        UI_AnimatorCanvasFader canvasFader = GetComponent<UI_AnimatorCanvasFader>();
        if(canvasFader != null)
        {
            canvasFader.FadeOutCanvas(2f, 0f, true);
        }
        else
	    {
            Debug.LogWarning("Couldn't find UI_AnimatorCanvasFader component on object");
            lobbyCanvas.enabled = false;
        }
    }

}
