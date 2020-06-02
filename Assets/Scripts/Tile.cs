using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.EventSystems;

public class Tile : NetworkBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SyncVar(hook = nameof(SetTileText2))]
    [SerializeField] private TileData tileData;

    [SyncVar(hook = nameof(SetColor))]
    [SerializeField] private Color color;
    private MaterialRecolourer materialRecolourer = new MaterialRecolourer();

    [SerializeField] private MeshRenderer meshRenderer = null;
    [SerializeField] private TextMeshProUGUI letterText = null;
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
        Debug.Log("RpcSetTileText: " + tileData.letter + " (" + tileData.value + ")");

        letterText.SetText(tileData.letter.ToString());
        valueText.SetText(tileData.value.ToString());
    }

    public void SetTileText2(TileData oldData, TileData newTileData)
    {
        Debug.Log("SetTileText(2): " + newTileData.letter + " (" + newTileData.value + ")");

        letterText.SetText(newTileData.letter.ToString());
        valueText.SetText(newTileData.value.ToString());
    }

    [Client]
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter on " + this.gameObject.name);

        materialRecolourer.Emission(meshRenderer, Color.grey, 0.5f);
    }

    [Client]
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit on " + this.gameObject.name);

        materialRecolourer.Reset(meshRenderer);
    }

    [Client]
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick on " + this.gameObject.name);

        PlayerInstance localPlayerInstance = PlayerManager.instance.GetLocalPlayerInstance();
        localPlayerInstance.CmdRequestTileSelect(base.netIdentity, localPlayerInstance.netIdentity);
    }

    [Server]
    public void Select (NetworkConnection requestingClientConnection)
    {
        //Debug.Log("Tile.Select");
        //Flip();
    }

    [Server]
    private void SetColor (Color oldColor, Color newColor)
    {
        Debug.Log("Tile.SetColor");

        color = newColor;
    }

    [Server]
    private void Flip ()
    {
        Debug.Log("Flipping...");
        meshRenderer.transform.Rotate(new Vector3(0, 0, 1), 180);
    }
}
