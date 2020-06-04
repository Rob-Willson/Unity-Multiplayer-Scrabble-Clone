using System;
using UnityEngine;

public static class CameraCalculations
{
    private static Camera mainCamera = null;
    public static Camera MainCamera
    {
        get
        {
            if(mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            return mainCamera;
        }
    }

    /// <summary>
    /// Raycast down from camera through the mouse position, returning the coordinate on the floor plane.
    /// </summary>
    public static Vector3 GetMouseWorldCoordinate ()
    {
        return GetWorldCoordinateFromScreenPosition(Input.mousePosition);
    }

    /// <summary>
    /// Raycast down from camera through a given position in the screen, returning the coordinate on the floor plane.
    /// </summary>
    public static Vector3 GetWorldCoordinateFromScreenPosition (Vector2 screenPosition)
    {
        Ray ray = MainCamera.ScreenPointToRay(screenPosition);
        float distanceToDrawPlane = (0f - ray.origin.y) / ray.direction.y;
        Vector3 screenPositionInWorld = ray.GetPoint(distanceToDrawPlane);
        return screenPositionInWorld;
    }

}
