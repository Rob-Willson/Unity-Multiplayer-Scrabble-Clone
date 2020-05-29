using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class TileBag : NetworkBehaviour
{
    public static TileBag instance = null;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] private List<Tile> tilesInBag = new List<Tile>();

    private void OnEnable()
    {
        //NetworkManagerJumble.ClientJoinedServer += GetAllTiles;
    }

    private void OnDisable()
    {
        //NetworkManagerJumble.ClientJoinedServer -= GetAllTiles;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        Debug.Log("OnStartServer");

        GenerateAllLetterTiles();
        ShuffleTilesInBag();
    }

    public void Deal(int requiredTileCount)
    {
        ShuffleTilesInBag();
        List<Tile> tilesToDeal = new List<Tile>();

        for(int i = 0; i < requiredTileCount; i++)
        {
            if(GetTileFromBag(out Tile tile))
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

    private bool GetTileFromBag(out Tile tile)
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
        GameObject objPrefab = NetworkManagerJumble.instance.FindRegisteredPrefabByName("Tile");

        if(objPrefab == null)
        {
            Debug.LogError("FAIL: Couldn't find prefab of name 'Tile'. Missing registered prefab?");
            return;
        }

        foreach(var requireLetter in requiredLetterCounts)
        {
            for(int i = 0; i < requireLetter.Item2; i++)
            {
                TileData tileData = new TileData();
                tileData.Letter = requireLetter.Item1;
                tileData.Value = 5;

                GameObject spawnedObj = Instantiate(objPrefab, this.transform);
                NetworkServer.Spawn(spawnedObj);

                Tile newTile = spawnedObj.GetComponent<Tile>();
                newTile.Intialize(tileData);

                tilesInBag.Add(newTile);
            }
        }
    }

    [ClientRpc]
    public void RpcMessageString_TEST(string msg)
    {
        Debug.Log("RPC TEST: " + msg);
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

    [Command]
    public void CmdRequestShuffleTilesInBag ()
    {
        Debug.Log("Request shuffle tiles in bag");
        ShuffleTilesInBag();
    }

    [Server]
    public void ShuffleTilesInBag()
    {
        Debug.Log("Shuffling tile bag... SERVER");
        tilesInBag.Shuffle();

        foreach(var tile in tilesInBag)
        {
            tile.RpcSetTileText();
            RpcDisplayAllTilesInBag();
        }

        RpcMessageString_TEST(" dfsfsdfs ");

    }

    [Client]
    public void DisplayTiles ()
    {
        RpcDisplayAllTilesInBag();
    }

    [ClientRpc]
    private void RpcDisplayAllTilesInBag()
    {
        int tileCount = tilesInBag.Count;
        int sqrtTileCount = Mathf.CeilToInt(Mathf.Sqrt(tileCount));

        int i = 0;
        for(int y = sqrtTileCount; y >= 0; y--)
        {
            for(int x = 0; x < sqrtTileCount; x++)
            {
                if(i >= tileCount)
                {
                    return;
                }

                tilesInBag[i++].transform.position = new Vector3(x - (sqrtTileCount / 2f), y - (sqrtTileCount / 2f));
            }
        }

    }

}