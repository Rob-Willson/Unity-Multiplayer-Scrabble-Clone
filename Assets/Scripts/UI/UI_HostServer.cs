using UnityEngine;

public class UI_HostServer : MonoBehaviour
{
    [SerializeField] private UI_PlayerScreenName playerScreenNameComponent = null;

    public void UICallback_HostButtonPressed ()
    {
        if(!playerScreenNameComponent.CheckScreenNameSubmitted())
        {
            return;
        }

        NetworkManagerJumble.instance.StartHost();
    }

}
