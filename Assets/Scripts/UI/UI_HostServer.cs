using UnityEngine;

public class UI_HostServer : MonoBehaviour
{

    public void UICallback_HostButtonPressed ()
    {
        NetworkManagerJumble.instance.StartHost();
    }

}
