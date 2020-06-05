using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.EventSystems;

public class Tile : NetworkBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SyncVar(hook = nameof(SetTileText))]
    [SerializeField] private TileData tileData;

    [SyncVar(hook = nameof(SetColor))]
    [SerializeField] private Color color;
    private MaterialRecolourer materialRecolourer = new MaterialRecolourer();

    [SerializeField] private MeshRenderer meshRenderer = null;
    [SerializeField] private Collider boxCollider = null;
    [SerializeField] private TextMeshProUGUI letterText = null;
    [SerializeField] private TextMeshProUGUI valueText = null;

    [Server]
    public void Intialize(TileData tileData)
    {
        this.tileData = tileData;
    }

    public void SetTileText(TileData oldData, TileData newTileData)
    {
        gameObject.name = "Tile " + tileData.letter;
        letterText.SetText(newTileData.letter.ToString());
        valueText.SetText(newTileData.value.ToString());
    }

    [Client]
    public void OnPointerEnter(PointerEventData eventData)
    {
        materialRecolourer.Emission(meshRenderer, Color.grey, 0.5f);
    }

    [Client]
    public void OnPointerExit(PointerEventData eventData)
    {
        materialRecolourer.Reset(meshRenderer);
    }

    [Server]
    private void SetColor(Color oldColor, Color newColor)
    {
        color = newColor;
    }

    [Server]
    public void Flip()
    {
        meshRenderer.transform.Rotate(new Vector3(0, 0, 1), 180);
    }

    [Server]
    public void ServerForceVisiblity(bool visible)
    {
        RpcMakeVisible(visible);
    }

    [ClientRpc]
    public void RpcMakeVisible(bool visible)
    {
        MakeVisible(visible);
    }

    [TargetRpc]
    public void TargetMakeVisible(NetworkConnection connection, bool visible)
    {
        MakeVisible(visible);
    }

    [Client]
    private void MakeVisible(bool visible)
    {
        meshRenderer.gameObject.SetActive(visible);
        boxCollider.enabled = visible;
    }

}
