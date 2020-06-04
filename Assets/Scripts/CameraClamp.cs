using UnityEngine;

/// <summary>
/// Clamps the camera within certain bounds.
/// </summary>
public class CameraClamp : MonoBehaviour
{
    public static CameraClamp instance = null;

    [SerializeField] private Vector3 originOffset;
    [SerializeField] private float maximumDistanceFromOrigin = 200f;

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

    private void LateUpdate ()
    {
        Vector3 deltasFromOrigin = new Vector3(Tr.position.x + originOffset.x, 0, Tr.position.z + originOffset.z);

        float distanceFromOrigin2D = Mathf.Sqrt(Mathf.Pow(deltasFromOrigin.x, 2) + Mathf.Pow(deltasFromOrigin.z, 2));

        if(distanceFromOrigin2D > maximumDistanceFromOrigin * 1f)
        {
            float ratioDistance = maximumDistanceFromOrigin / distanceFromOrigin2D;
            Tr.position = new Vector3(Tr.position.x * ratioDistance, Tr.position.y, Tr.position.z * ratioDistance);
        }
    }

    private float AttentuationByZoom
    {
        get
        {
            float value = Tr.position.y / CameraZoom.instance.maxZoom;
            return value;
        }
    }

}
