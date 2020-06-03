using System;
using UnityEngine;

[Serializable]
public class TileData
{
    [SerializeField] public char letter;
    [SerializeField] public int value;

    public TileData()
    {
    }

    public TileData(char letter)
    {
        this.letter = letter;
        value = new ConvertLetterToValue().GetValue(letter);
    }
    
}
