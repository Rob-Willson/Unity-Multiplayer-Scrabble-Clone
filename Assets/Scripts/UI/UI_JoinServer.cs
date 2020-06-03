using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class UI_JoinServer : MonoBehaviour
{
    [SerializeField] private TMP_InputField targetServerIPInputField = null;

    /// <summary>
    /// Only to be called from UI event trigger.
    /// </summary>
    public void UICallback_JoinButtonPressed()
    {
        string targetAddress = targetServerIPInputField.text;
        if(ValidateNetworkAddress(ref targetAddress))
        {
            Debug.Log("Trying to connecting to address: " + targetAddress);
            NetworkManagerJumble.instance.networkAddress = targetAddress;
            NetworkManagerJumble.instance.StartClient();
        }
        else
        {
            Debug.LogError("ERROR: Target address invalid. Check inputted IP address and try again.");
        }
    }

    private bool ValidateNetworkAddress(ref string input)
    {
        if(string.IsNullOrWhiteSpace(input))
        {
            input = "localhost";
        }
        return true;
    }

}
