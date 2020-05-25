using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_LetterTile : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI letterText;
    [SerializeField] private TextMeshProUGUI valueText;
    private LetterTileData letterTile;
    private bool initialized = false;

    public void Intialize(LetterTileData letterTile)
    {
        initialized = true;
        this.letterTile = letterTile;
        SetTileText();
    }

    private void SetTileText()
    {
        CheckInitialized();
        letterText.SetText(letterTile.Letter.ToString());
        valueText.SetText(letterTile.Value.ToString());
    }

    private void CheckInitialized ()
    {
        if(!initialized)
        {
            Debug.LogError("FAIL: " + this.GetType().ToString() + " was not initialized before being used. Bug?");
        }
    }

}
