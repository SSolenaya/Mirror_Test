using System;
using System.Collections;
using System.Collections.Generic;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.UI;

public class ServItem : MonoBehaviour
{
    public Button button;
    public Text serverIPText;
    public ServerResponse serverResponse;
    public NetworkDiscoveryHUD _networkDiscoveryHUD;

    public void Setup(ServerResponse info, NetworkDiscoveryHUD networkDiscoveryHUD)
    {
        _networkDiscoveryHUD = networkDiscoveryHUD;
        serverIPText.text = info.EndPoint.Address.ToString();
        serverResponse = info;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {
            _networkDiscoveryHUD.Connect(serverResponse);
        });
    }

    
}
