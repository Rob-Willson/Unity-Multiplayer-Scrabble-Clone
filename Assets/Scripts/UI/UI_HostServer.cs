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


        string ip = GetLocalIPv4();
        Debug.Log("IP: " + ip);
    }

     public string GetLocalIPv4()
     {
         return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
     }    

}
