using System;
using UnityEngine;
using Mirror;

public class PlayerInput : NetworkBehaviour
{
    public static Action<Vector3> TileDragOngoing;
    public static Action TileDragEnd;

    [SerializeField] private Tile currentlySelectedTile;
    private bool hasTile
    {
        get
        {
            return currentlySelectedTile == null ? false : true;
        }
    }

    private void Update()
    {
        if(isServerOnly)
        {
            return;
        }
        if(!hasAuthority)
        {
            return;
        }
        LocalClientUpdate();
    }

    [Client]
    private void LocalClientUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Mouse0 down");

            if(hasTile)
            {
                Debug.LogError("Already had tile picked up but trying to mouse down. Why wasn't tile dropped?");
                return;
            }

            Tile tileUnderPointer = GetTileUnderPointer();
            if(tileUnderPointer != null)
            {
                CmdPickUpTile(tileUnderPointer.netIdentity);
                if(Application.platform == RuntimePlatform.Android)
                {
                    CmdFlipTile(tileUnderPointer.netIdentity);
                }

            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(hasTile)
            {
                RequestDropTile();
                TileDragEnd?.Invoke();
            }
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(!hasTile)
            {
                return;
            }

            Vector3 mousePositionInWorld = CameraCalculations.GetMouseWorldCoordinate();
            TileDragOngoing?.Invoke(mousePositionInWorld);
        }

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Tile tileUnderPointer = GetTileUnderPointer();
            if(tileUnderPointer != null)
            {
                CmdFlipTile(tileUnderPointer.netIdentity);
            }
        }
    }

    [Client]
    private Tile GetTileUnderPointer()
    {
        Tile hitTile = null;
        if(Physics.Raycast(CameraCalculations.MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            hitTile = hit.collider.gameObject.GetComponent<Tile>();
        }
        return hitTile;
    }

    [Command]
    private void CmdPickUpTile(NetworkIdentity tileNetworkIdentity)
    {
        // TODO: Validate
        // ...

        TargetDoPickUpTile(base.connectionToClient, tileNetworkIdentity);
    }

    [TargetRpc]
    private void TargetDoPickUpTile(NetworkConnection connectionToTargetClient, NetworkIdentity tileNetworkIdentity)
    {
        Tile tile = tileNetworkIdentity.GetComponent<Tile>();
        if(tile == null)
        {
            Debug.LogError("FAIL: Tile component wasn't found on passed NetworkIdentity. ");
            return;
        }

        currentlySelectedTile = tile;
    }

    [Client]
    private void RequestDropTile()
    {
        Vector3 dropPosition = CameraCalculations.GetMouseWorldCoordinate();
        Vector3Int dropPositionSnapped = new Vector3Int(Mathf.RoundToInt(dropPosition.x), 0, Mathf.RoundToInt(dropPosition.z));

        // Reject placement if there is a tile already under the pointer
        if(GetTileUnderPointer() != null)
        {
            DropCurrentlySelectedTile();
            return;
        }

        CmdDropTile(base.netIdentity, currentlySelectedTile.netIdentity, dropPositionSnapped);
    }

    [Command]
    private void CmdDropTile(NetworkIdentity requestingClientIdentity, NetworkIdentity tileNetworkIdentity, Vector3Int requestedDropPosition)
    {
        bool droppedOnBoard = false;

        if(Physics.Raycast(requestedDropPosition + new Vector3(0, 5, 0), Vector3.down, out RaycastHit hit, 10f))
        {
            Tile hitTile = hit.collider.gameObject.GetComponent<Tile>();
            if(hitTile != null)
            {
                Debug.Log("  square already has a tile on it: " + hitTile.name);
                return;
            }

            Board hitBoard = hit.collider.gameObject.GetComponent<Board>();
            if(hitBoard == null)
            {
                droppedOnBoard = false;
            }
            else
            {
                droppedOnBoard = true;
            }
        }

        Tile tile = tileNetworkIdentity.GetComponent<Tile>();
        tile.RpcMakeVisible(droppedOnBoard);
        tile.TargetMakeVisible(requestingClientIdentity.connectionToClient, true);

        tileNetworkIdentity.transform.localPosition = requestedDropPosition;

        TargetDoDropTile(requestingClientIdentity.connectionToClient);
    }

    [TargetRpc]
    private void TargetDoDropTile(NetworkConnection connectionToTargetClient)
    {
        DropCurrentlySelectedTile();
    }

    [Client]
    private void DropCurrentlySelectedTile ()
    {
        currentlySelectedTile = null;
    }

    [Command]
    private void CmdFlipTile(NetworkIdentity tileNetworkIdentity)
    {
        // TODO: Validate
        // ...

        Tile tile = tileNetworkIdentity.GetComponent<Tile>();
        if(tile == null)
        {
            Debug.LogError("FAIL: Tile component not found on passed NetworkIdentity.");
            return;
        }

        tile.Flip();
    }

}
