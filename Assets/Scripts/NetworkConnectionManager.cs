using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.UI;

public class NetworkConnectionManager : MonoBehaviour
{
    public Button btn_client_start; 
    public Button btn_client_stop;
    public Button btn_host_start;
    public Button btn_host_stop;
    public ServItem servItemPrefab;
    public Transform servItemsParent;

    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    public NetworkDiscovery networkDiscovery;

    void Start()
    {
        return;
        networkDiscovery.OnServerFound.AddListener(OnDiscoveredServer);
        //UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);

        btn_client_start.onClick.RemoveAllListeners();
        btn_client_stop.onClick.RemoveAllListeners();
        btn_host_start.onClick.RemoveAllListeners();
        btn_host_stop.onClick.RemoveAllListeners();

        btn_client_start.onClick.AddListener(StartClient);
        btn_client_stop.onClick.AddListener(StopClient);
        btn_host_start.onClick.AddListener(StartHost);
        btn_host_stop.onClick.AddListener(StopHost);
    }

    //void OnGUI()
    //{
    //    if (NetworkManager.singleton == null) return;
    //
    //        btn_client_start.gameObject.SetActive(!NetworkClient.active && !NetworkClient.isConnected);
    //        btn_host_start.gameObject.SetActive(!NetworkClient.active && !NetworkClient.isConnected);
    //
    //        btn_host_stop.gameObject.SetActive(NetworkServer.active);
    //    //btn_client_stop.gameObject.SetActive(NetworkServer.active);
    //
    //    AddServers();
    //}

    //public void AddServers()
    //{
    //    if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
    //        foreach (ServerResponse info in discoveredServers.Values)
    //        {
    //            var serv = Instantiate(servItemPrefab, servItemsParent);
    //            serv.transform.localScale = Vector3.one;
    //            serv.Setup(info.EndPoint.Address.ToString(), () => {
    //                Connect(info);
    //            });
    //        }
    //}

    public void StartClient()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
    }
    public void StopClient()
    {
        NetworkManager.singleton.StopClient();
        networkDiscovery.StopDiscovery();
    }
    public void StartHost()
    {
        discoveredServers.Clear();
        NetworkManager.singleton.StartHost();
        networkDiscovery.AdvertiseServer();
    }
    public void StopHost()
    {
        NetworkManager.singleton.StopHost();
        networkDiscovery.StopDiscovery();
    }

    public void OnDiscoveredServer(ServerResponse info)
    {
        // Note that you can check the versioning to decide if you can connect to the server or not using this method
        discoveredServers[info.serverId] = info;
        Debug.Log(info.ToString());
    }
    void Connect(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        NetworkManager.singleton.StartClient(info.uri);
    }
}
