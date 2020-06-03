using UnityEngine;

public class TileDragIndicator : MonoBehaviour
{
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

    [SerializeField] private MeshRenderer meshRenderer = null;

    private void Awake()
    {
        Hide();
    }

    private void OnEnable()
    {
        PlayerInput.TileDragOngoing += MoveToPosition;
        PlayerInput.TileDragEnd += Hide;
    }

    private void OnDisable()
    {
        PlayerInput.TileDragOngoing -= MoveToPosition;
        PlayerInput.TileDragEnd -= Hide;
    }

    private void MoveToPosition (Vector3 newPosition)
    {
        // Offset above other tile slightly so that it stands out above other tiles
        newPosition += new Vector3(0, 0.1f, 0);

        Tr.position = newPosition;
        meshRenderer.gameObject.SetActive(true);
    }

    private void Hide ()
    {
        meshRenderer.gameObject.SetActive(false);
    }

}
