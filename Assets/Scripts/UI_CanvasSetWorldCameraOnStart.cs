using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UI_CanvasSetWorldCameraOnStart : MonoBehaviour
{
    private void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if(canvas == null)
        {
            Debug.LogError("FAIL: Canvas not found on object");
            return;
        }

        canvas.worldCamera = CameraCalculations.MainCamera;
    }
}
