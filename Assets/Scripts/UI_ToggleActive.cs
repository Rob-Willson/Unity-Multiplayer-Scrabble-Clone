using UnityEngine;

public class UI_ToggleActive : MonoBehaviour
{
    public void ToggleActive ()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
