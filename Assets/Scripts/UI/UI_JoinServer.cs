using UnityEngine;
using TMPro;

public class UI_JoinServer : MonoBehaviour
{
    [SerializeField] private TMP_InputField targetServerIPInputField = null;

    public void UICallback_JoinButtonPressed()
    {
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
