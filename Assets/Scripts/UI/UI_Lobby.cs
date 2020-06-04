using UnityEngine;
using Mirror;
using TMPro;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] private Canvas lobbyCanvas = null;

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
        UI_AnimatorCanvasFader canvasFader = GetComponent<UI_AnimatorCanvasFader>();
        if(canvasFader != null)
        {
            canvasFader.FadeOutCanvas(2f, 0f, false);
            CanvasGroup canvasGroup = lobbyCanvas.GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

}
