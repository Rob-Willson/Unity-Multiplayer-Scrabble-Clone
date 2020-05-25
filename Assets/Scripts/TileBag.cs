using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class TileBag : NetworkBehaviour
{
    [SerializeField] private GameObject tileTemplate;
    private List<UI_LetterTile> tilesInBag = new List<UI_LetterTile>();

    private void Start()
    {
        GenerateAllLetterTiles();
        Shuffle();
    }

    public void Deal(int requiredTileCount)
    {
        List<UI_LetterTile> tilesToDeal = new List<UI_LetterTile>();

        for(int i = 0; i < requiredTileCount; i++)
        {
            if(GetTileFromBag(out UI_LetterTile tile))
            {
                tilesToDeal.Add(tile);
            }
            else
            {
                Debug.Log("No more tiles in bag");
                continue;
            }
        }
    }

    private bool GetTileFromBag(out UI_LetterTile tile)
    {
        if(tilesInBag.Count == 0)
        {
            tile = null;
            return false;
        }

        tile = tilesInBag[tilesInBag.Count-1];
        tilesInBag.RemoveAt(tilesInBag.Count-1);
        return true;
    }

    private void GenerateAllLetterTiles()
    {
        var requiredLetterCounts = GetRequiredLetterCounts();


        foreach(var requireLetter in requiredLetterCounts)
        {
            for(int i = 0; i < requireLetter.Item2; i++)
            {
                LetterTileData newLetterTileData = new LetterTileData(requireLetter.Item1);

                GameObject obj = Instantiate(tileTemplate, this.transform);

                UI_LetterTile newLetterTile = obj.GetComponent<UI_LetterTile>();
                newLetterTile.Intialize(newLetterTileData);

            }
        }

    }

    private List<Tuple<char, int>> GetRequiredLetterCounts()
    {
        var requiredLetterCounts = new List<Tuple<char, int>>();
        requiredLetterCounts.Add(new Tuple<char, int>('A', 9));
        requiredLetterCounts.Add(new Tuple<char, int>('B', 2));
        requiredLetterCounts.Add(new Tuple<char, int>('C', 2));
        requiredLetterCounts.Add(new Tuple<char, int>('D', 4));
        requiredLetterCounts.Add(new Tuple<char, int>('E', 12));
        requiredLetterCounts.Add(new Tuple<char, int>('F', 2));
        requiredLetterCounts.Add(new Tuple<char, int>('G', 3));
        requiredLetterCounts.Add(new Tuple<char, int>('H', 2));
        requiredLetterCounts.Add(new Tuple<char, int>('I', 9));
        requiredLetterCounts.Add(new Tuple<char, int>('J', 1));
        requiredLetterCounts.Add(new Tuple<char, int>('K', 1));
        requiredLetterCounts.Add(new Tuple<char, int>('L', 4));
        requiredLetterCounts.Add(new Tuple<char, int>('M', 2));
        requiredLetterCounts.Add(new Tuple<char, int>('N', 6));
        requiredLetterCounts.Add(new Tuple<char, int>('O', 8));
        requiredLetterCounts.Add(new Tuple<char, int>('P', 2));
        requiredLetterCounts.Add(new Tuple<char, int>('Q', 1));
        requiredLetterCounts.Add(new Tuple<char, int>('R', 6));
        requiredLetterCounts.Add(new Tuple<char, int>('S', 4));
        requiredLetterCounts.Add(new Tuple<char, int>('T', 6));
        requiredLetterCounts.Add(new Tuple<char, int>('U', 4));
        requiredLetterCounts.Add(new Tuple<char, int>('V', 2));
        requiredLetterCounts.Add(new Tuple<char, int>('W', 2));
        requiredLetterCounts.Add(new Tuple<char, int>('X', 1));
        requiredLetterCounts.Add(new Tuple<char, int>('Y', 2));
        requiredLetterCounts.Add(new Tuple<char, int>('Z', 1));
        return requiredLetterCounts;
    }

    private void Shuffle()
    {
        Debug.Log("Shuffling tile bag...");
        tilesInBag.Shuffle();



    }

}