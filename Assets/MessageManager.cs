using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public static MessageManager inst;
    public Text messageTextContainer;
    public Text textIP;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Test();
    }

   //public static void ChekerID()
   //{
   //    if (!NetworkManager.)
   //    {
   //        return;
   //    }
   //    listNetworkObject = FindObjectsOfType<NetworkObject>().ToList();
   //    NetworkObject server = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
   //    for (int i = 0; i < listNetworkObject.Count; i++)
   //    {
   //        if (server.NetworkObjectId == listNetworkObject[i].NetworkObjectId)
   //        {
   //            HelloWorldPlayer player = listNetworkObject[i].GetComponent<HelloWorldPlayer>();
   //            player.isServer.Value = true;
   //        }
   //        else
   //        {
   //            HelloWorldPlayer player = listNetworkObject[i].GetComponent<HelloWorldPlayer>();
   //            player.isServer.Value = false;
   //        }
   //    }
   //}

    [ContextMenu("Test")]
    public void Test()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }

        textIP.text = localIP;
        Debug.Log(localIP);
    }

    public void ShowMessage(string msg)
    {
        messageTextContainer.text = msg;
    }
}
