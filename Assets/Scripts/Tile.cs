using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.EventSystems;

public class Tile : NetworkBehaviour, IPointerClickHandler
{
    [SyncVar(hook = nameof(SetTileText2))]
    [SerializeField] private TileData tileData;

    [SerializeField] public TextMeshProUGUI letterText = null;
    [SerializeField] private TextMeshProUGUI valueText = null;

    [Server]
    public void Intialize(TileData tileData)
    {
        //Debug.Log("INITIALIZING: " + tileData.letter + " (" + tileData.value + ")"); 

        this.tileData = tileData;
        //RpcSetTileText();
        gameObject.name = "Tile " + tileData.letter;
    }

    [ClientRpc]
    public void RpcSetTileText()
    {
        //Debug.Log("RpcSetTileText: " + tileData.letter + " (" + tileData.value + ")");

        letterText.SetText(tileData.letter.ToString());
        valueText.SetText(tileData.value.ToString());
    }


    public void SetTileText2(TileData oldData, TileData tileData)
    {
        Debug.Log("SetTileText(2): " + tileData.letter + " (" + tileData.value + ")");

        letterText.SetText(tileData.letter.ToString());
        valueText.SetText(tileData.value.ToString());
    }


    [Client]
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick on " + this.gameObject.name);

        // TODO: Notify Player that tile was clicked by it
        //       But how to get the right Player?

    }

}
