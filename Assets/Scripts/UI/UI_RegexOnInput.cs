using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

/// <summary>
/// Listens in on a UI InputField, instantly amending inputted text according to the regex setting applied.
/// </summary>
public class UI_RegexOnInput : MonoBehaviour
{
    public enum RegexType { None, ScreenName, IPAddress };
    [SerializeField] private RegexType regexType = RegexType.None;
    [SerializeField] private TMP_InputField targetServerIPInputField;

    private void OnEnable()
    {
        targetServerIPInputField.onValueChanged.AddListener(delegate { ApplyRegex(targetServerIPInputField.text); });
    }

    private void OnDisable()
    {
        targetServerIPInputField.onValueChanged.RemoveListener(delegate { ApplyRegex(targetServerIPInputField.text); });
    }

    private void ApplyRegex (string input)
    {
        switch(regexType)
        {
            case RegexType.None:
                Debug.LogWarning("No regex setting applied to '" + this.GetType().ToString() + "'. Did you forget to assign it one in the inspector?");
                return;
            case RegexType.ScreenName:
                ValidateInputAsScreenName(input);
                return;
            case RegexType.IPAddress:
                ValidateInputAsIPAddress(input);
                return;
            default:
                Debug.LogError("FAIL: Unexpected regex setting applied to '" + this.GetType().ToString() + "'. Bug?");
                return;
        }
    }

    private void ValidateInputAsScreenName(string input)
    {
        input = Regex.Replace(input, @"[^a-zA-Z ]", "");
        targetServerIPInputField.text = input;
    }

    private void ValidateInputAsIPAddress(string input)
    {
        // Alphanumerical characters are allowed here deliberately
        // Otherwise it would be difficult to type "localhost", which is useful for testing
        input = Regex.Replace(input, @"[^a-z0-9.:]", "");
        targetServerIPInputField.text = input;
    }

}
