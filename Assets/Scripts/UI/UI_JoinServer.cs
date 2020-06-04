using UnityEngine;
using TMPro;

public class UI_JoinServer : MonoBehaviour
{
    [SerializeField] private TMP_InputField targetServerIPInputField = null;
    [SerializeField] private UI_PlayerScreenName playerScreenNameComponent = null;

    public void UICallback_JoinButtonPressed()
    {
        if(!playerScreenNameComponent.CheckScreenNameSubmitted())
        {
            return;
        }

        string targetAddress = ValidateTargetNetworkAddress(targetServerIPInputField.text);
        Debug.Log("Trying to connect to address: " + targetAddress);

        NetworkManagerJumble.instance.networkAddress = targetAddress;
        NetworkManagerJumble.instance.StartClient();
    }

    private string ValidateTargetNetworkAddress(string input)
    {
        if(string.IsNullOrWhiteSpace(input))
        {
            input = "localhost";
        }
        return input;
    }

}
