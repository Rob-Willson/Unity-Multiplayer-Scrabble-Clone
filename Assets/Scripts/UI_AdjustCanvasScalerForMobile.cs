using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class UI_AdjustCanvasScalerForMobile : MonoBehaviour
{
    #if UNITY_ANDROID

    private void Start()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            ScaleForMobile();
        }
    }

    private void ScaleForMobile ()
    {
        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
        canvasScaler.scaleFactor = 1.5f;
    }

    #endif

}
