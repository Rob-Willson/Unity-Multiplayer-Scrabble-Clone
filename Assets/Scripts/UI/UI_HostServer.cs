using UnityEngine;
using TMPro;

public class UI_HostServer : MonoBehaviour
{

    /// <summary>
    /// Only to be called from UI event trigger.
    /// </summary>
    public void UICallback_HostButtonPressed ()
    {
        NetworkManagerJumble.instance.StartHost();
    }

}
