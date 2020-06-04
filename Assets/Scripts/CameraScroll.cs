using UnityEngine;
using System.Collections;

/// <summary>
/// Scrolls the camera (x/z direction).
/// </summary>
public class CameraScroll : MonoBehaviour
{
    public static CameraScroll instance = null;

    [SerializeField] private float scrollSpeed = 70f;

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

    private void Update ()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            ScrollCameraInDirectionLinear(new Vector3Int(0, 0, 1));
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            ScrollCameraInDirectionLinear(new Vector3Int(0, 0, -1));
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            ScrollCameraInDirectionLinear(new Vector3Int(-1, 0, 0));
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            ScrollCameraInDirectionLinear(new Vector3Int(1, 0, 0));
        }
    }

    /// <summary>
    /// Scroll the camera up/down/left/right, based on the passed scroll direction vector.
    /// Intended for use by WASD movement.
    /// </summary>
    public void ScrollCameraInDirectionLinear (Vector3Int scrollDirection)
    {
        Tr.position += new Vector3(scrollDirection.x * scrollSpeed * Time.unscaledDeltaTime, 0, scrollDirection.z * scrollSpeed * Time.unscaledDeltaTime);
    }

}
