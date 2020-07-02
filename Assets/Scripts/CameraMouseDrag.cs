using UnityEngine;

/// <summary>
/// Handles moving the camera according to mouse drag inputs.
/// </summary>
public class CameraMouseDrag : MonoBehaviour
{
    public static CameraMouseDrag instance = null;

    private Vector3 currentPositionInWorld;
    private Vector3 previousPositionInWorld;

    private Transform tr;
    private Transform Tr
    {
        get
        {
            if(tr == null)
            {
                tr = transform;
            }
            return tr;
        }
    }

    private void Awake ()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("Duplicate instances of '" + this.GetType().ToString() + "'. Bug?");
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse2))
        {
            StartCameraDrag();
            return;
        }
        if(Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse2))
        {
            OngoingCameraDrag();
        }
    }

    /// <summary>
    /// Caches the mouse position in world, since it is required in order to determine the magnitude of any ongoing drag calls.
    /// </summary>
    public void StartCameraDrag ()
    {
        previousPositionInWorld = CameraCalculations.GetMouseWorldCoordinate();
    }

    /// <summary>
    /// Compare previous with current world position of mouse in order to move the Camera by the appropriate amount.
    /// </summary>
    public void OngoingCameraDrag ()
    {
        currentPositionInWorld = CameraCalculations.GetMouseWorldCoordinate();
        Vector3 displacement = currentPositionInWorld - previousPositionInWorld;
        Vector3 moveCameraToPosition = Tr.position - displacement;
        Tr.position = moveCameraToPosition;
    }

}
