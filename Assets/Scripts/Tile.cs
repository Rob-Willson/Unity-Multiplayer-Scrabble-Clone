using UnityEngine;
using Mirror;
using TMPro;

public class Tile : NetworkBehaviour
{
    [SerializeField] private TileData tileData;
    public TileData TileData
    {
        get
        {
            if(!IsInitialized)
            {
                Debug.LogError("FAIL: Trying to access TileData before it was initialized.");
            }
            return tileData;
        }
        private set
        {
            tileData = value;
        }
    }

    [SerializeField] public TextMeshProUGUI letterText;
    [SerializeField] private TextMeshProUGUI valueText;

    public void Intialize(TileData tileData)
    {
        this.tileData = tileData;
        RpcSetTileText();
        gameObject.name = "Tile " + tileData.Letter;
    }

    [ClientRpc]
    public void RpcSetTileText()
    {
        if(!IsInitialized)
        {
            Debug.LogError("FAIL: Trying to set tile text before it was initialized correctly.");
        }
        else
        {
            Debug.Log("RpcSetTileText");
        }

        //Debug.Log("  " + tileData.Letter);

        letterText.SetText(tileData.Letter.ToString());
        valueText.SetText(tileData.Value.ToString());
    }

    private bool IsInitialized
    {
        get
        {
            return true;
            //return (tileData != null);
        }
    }

}
