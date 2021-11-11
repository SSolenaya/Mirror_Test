using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Assets;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MessageManager : MonoBehaviour
{
    public static MessageManager inst;
    public Text messageTextContainer;
    public Text textIP;
    public Text clientsListTxt;
    public Button pianoBtn;
    public Button drumBtn;
    public Button violinBtn;
    public Button playBtn;
    public Button pauseBtn;
    public Dropdown dropdown;

    public VideoPlayer videoPlayer;

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


        pianoBtn.onClick.RemoveAllListeners();
        pianoBtn.onClick.AddListener(() => {
            if (ClientManager.inst.HostPlayer == null) return;
            ClientManager.inst.HostPlayer.InstrumentMsg(ClientManager.inst.GetPlayerByInstrument(Musicians.piano));
        });

        drumBtn.onClick.RemoveAllListeners();
        drumBtn.onClick.AddListener(() => {
            if (ClientManager.inst.HostPlayer == null) return;
            ClientManager.inst.HostPlayer.InstrumentMsg(ClientManager.inst.GetPlayerByInstrument(Musicians.drum));
        });

        violinBtn.onClick.RemoveAllListeners();
        violinBtn.onClick.AddListener(() => {
            if (ClientManager.inst.HostPlayer == null) return;
            ClientManager.inst.HostPlayer.InstrumentMsg(ClientManager.inst.GetPlayerByInstrument(Musicians.violin));
        });

        playBtn.onClick.RemoveAllListeners();
        playBtn.onClick.AddListener(() => {
            if (ClientManager.inst.HostPlayer == null) return;
            pauseBtn.interactable = true;
            playBtn.interactable = false;
            ClientManager.inst.HostPlayer.PlayVideo();
        });
        pauseBtn.onClick.RemoveAllListeners();
        pauseBtn.onClick.AddListener(() => {
            if (ClientManager.inst.HostPlayer == null) return;
            playBtn.interactable = true;
            pauseBtn.interactable = false;
            ClientManager.inst.HostPlayer.PauseVideo();
        });

        dropdown.onValueChanged.AddListener((_) => SetInstrument(dropdown.value));
    }

    void SetInstrument(int num)
    {
        ClientManager.inst.LocalPlayer.CmdSetInstrument(num);
    }

    public void PlayVideo()
    {
        if (!videoPlayer.isPlaying) videoPlayer.Play();
    }
    public void PauseVideo()
    {
        if(videoPlayer.isPlaying) videoPlayer.Pause();
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

    public void AddClientInfo(string info)
    {
        clientsListTxt.text += info + "\n";
    }
}
