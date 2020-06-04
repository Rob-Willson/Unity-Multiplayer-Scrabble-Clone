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
    private void LocalClientUpdate ()
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
            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(hasTile)
            {
                RequestDropTile();
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
    private Tile GetTileUnderPointer ()
    {
        if(Physics.Raycast(CameraCalculations.MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            Tile hitTile = hit.collider.gameObject.GetComponent<Tile>();
            return hitTile;
        }
        return null;
    }

    [Command]
    private void CmdPickUpTile (NetworkIdentity tileNetworkIdentity)
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
            Debug.LogError("FAIL: Tile component wasn't found on passed NetworkIdentity.");
            return;
        }

        currentlySelectedTile = tile;
    }

    [Client]
    private void RequestDropTile ()
    {
        Vector3 dropPosition = CameraCalculations.GetMouseWorldCoordinate();
        Vector3Int dropPositionSnapped = new Vector3Int(Mathf.RoundToInt(dropPosition.x), 0, Mathf.RoundToInt(dropPosition.z));

        CmdDropTile(currentlySelectedTile.netIdentity, dropPositionSnapped);
    }

    [Command]
    private void CmdDropTile (NetworkIdentity tileNetworkIdentity, Vector3Int requestedDropPosition)
    {
        // TODO: Validate
        // ...

        tileNetworkIdentity.transform.localPosition = requestedDropPosition;
        TargetDoDropTile(base.connectionToClient);
    }

    [TargetRpc]
    private void TargetDoDropTile(NetworkConnection connectionToTargetClient)
    {
        currentlySelectedTile = null;
        TileDragEnd?.Invoke();
    }

    [Command]
    private void CmdFlipTile (NetworkIdentity tileNetworkIdentity)
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
