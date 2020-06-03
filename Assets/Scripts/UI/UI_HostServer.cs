using UnityEngine;
using TMPro;
using System.Net;
using System.Linq;

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
