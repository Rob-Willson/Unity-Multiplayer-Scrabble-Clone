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

    [Server]
    public void Intialize(TileData tileData)
    {
        this.tileData = tileData;
        SetTileText();
        gameObject.name = "Tile " + tileData.Letter;
    }

    [Server]
    private void SetTileText()
    {
        if(!IsInitialized)
        {
            Debug.LogError("FAIL: Trying to set tile text before it was initialized correctly.");
        }

        letterText.SetText(tileData.Letter.ToString());
        valueText.SetText(tileData.Value.ToString());
    }

    private bool IsInitialized
    {
        get
        {
            return (tileData != null);
        }
    }

}
