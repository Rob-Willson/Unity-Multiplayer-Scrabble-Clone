using UnityEngine;

public class UI_SnapPositionToGridOnStart : MonoBehaviour
{
    private void Start()
    {
        Vector3 pos = this.transform.localPosition;
        pos = new Vector3(Mathf.RoundToInt(pos.x), 0.005f, Mathf.RoundToInt(pos.z));
        transform.localPosition = pos;
    }
}
