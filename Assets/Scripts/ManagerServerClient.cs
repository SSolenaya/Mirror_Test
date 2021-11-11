using System.Collections;
using System.Collections.Generic;
using System.Net;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.UI;

public class ManagerServerClient : MonoBehaviour
{
    public NetworkDiscoveryHUD networkDiscoveryHUD;
    public ServItem servItemPrefab;
    public Transform servItemsParent;

    public List<ServItem> listServeItems;


    public InputField inputField;

    public void Start()
    {
        StartCoroutine(IEnum());
    }

    public void StartHost()
    {
        networkDiscoveryHUD.StartHost();
    }

    public void FindServer()
    {
        if (inputField.text != "")
        {
            Debug.LogError("Hard IP");
            networkDiscoveryHUD.GetComponent<NetworkManager>().networkAddress = inputField.text;
            networkDiscoveryHUD.GetComponent<NetworkManager>().StartClient();
        }
        else
        {
            networkDiscoveryHUD.FindServer();
        }
       
    }

    public IEnumerator IEnum()
    {
        
        while (true)
        {
            while (networkDiscoveryHUD.GetDiscoveredServers().Count == 0)
            {
                yield return new WaitForSeconds(1);
            }

            ClearServerItems();
            foreach (ServerResponse info in networkDiscoveryHUD.GetDiscoveredServers().Values)
            {
                Debug.Log(info.EndPoint.Address.ToString());
                ServItem servItem = Instantiate(servItemPrefab, servItemsParent);
                servItem.transform.localScale = Vector3.one;
                servItem.Setup(info, networkDiscoveryHUD);
                listServeItems.Add(servItem);

            }
            yield return new WaitForSeconds(1);
        }
        
    }

    public void ClearServerItems()
    {
        foreach (ServItem si in listServeItems)
        {
            if (si != null)
            {
                Destroy(si.gameObject);
            }
        }
        listServeItems.Clear();
    }
}
