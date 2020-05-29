using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[Serializable]
public class TileData
{
    public char Letter;
    public int Value;

    //public TileData(char letter)
    //{
    //    this.Letter = letter;
    //    Value = 3; // new ConvertLetterToValue().GetValue(letter);
    //}
}


public sealed class ConvertLetterToValue
{
    Dictionary<char, int> letterValuePairs = new Dictionary<char, int>();

    public ConvertLetterToValue()
    {
        GenerateLetterValuePairs();
    }

    public int GetValue(char letter)
    {
        letterValuePairs.TryGetValue(letter, out int value);
        return value;
    }

    private void GenerateLetterValuePairs()
    {
        letterValuePairs.Add('A', 1);
        letterValuePairs.Add('B', 3);
        letterValuePairs.Add('C', 3);
        letterValuePairs.Add('D', 2);
        letterValuePairs.Add('E', 1);
        letterValuePairs.Add('F', 4);
        letterValuePairs.Add('G', 2);
        letterValuePairs.Add('H', 4);
        letterValuePairs.Add('I', 1);
        letterValuePairs.Add('J', 7);
        letterValuePairs.Add('K', 5);
        letterValuePairs.Add('L', 1);
        letterValuePairs.Add('M', 3);
        letterValuePairs.Add('N', 1);
        letterValuePairs.Add('O', 1);
        letterValuePairs.Add('P', 3);
        letterValuePairs.Add('Q', 9);
        letterValuePairs.Add('R', 1);
        letterValuePairs.Add('S', 1);
        letterValuePairs.Add('T', 1);
        letterValuePairs.Add('U', 1);
        letterValuePairs.Add('V', 4);
        letterValuePairs.Add('W', 4);
        letterValuePairs.Add('X', 7);
        letterValuePairs.Add('Y', 4);
        letterValuePairs.Add('Z', 9);
    }

}