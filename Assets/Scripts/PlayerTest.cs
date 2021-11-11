using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using Mirror;
using UnityEngine.Video;

public enum Musicians
{
    violin,
    drum,
    piano
}

public class PlayerTest : NetworkBehaviour
{
    public string message;
    public int number;
    private int health = 100;
    public NetworkIdentity playerIdentity;

    //[SyncVar] public float videoTimeStamp;
    [SyncVar] public Musicians instrument;
    [SyncVar] public string deviceID;
    // [SyncVar] public bool isServer;



    public struct Message : NetworkMessage
    {
        public string msgText;
        public int receivedNumber;
    }

    public override void OnStartClient()
    {
        
        playerIdentity = GetComponent<NetworkIdentity>();
        if (isServer && isClient)
        {
            ClientManager.inst.HostPlayer = this;
            Debug.Log(gameObject.name);
        }

        if (isClient && isLocalPlayer)
        {
            ClientManager.inst.LocalPlayer = this;
        }

        ClientManager.inst.AddToPlayersList(this);
        CmdSetupClientInfo();

    }

    [ContextMenu("Send message")]
    public void SendMsg()
    {
        Message msg = new Message()
        {
            msgText = message,
            receivedNumber = number
        };
        NetworkServer.SendToAll(msg);
    }
   
    public void OnMessage(Message msg)
    {
        string txt = "OnScoreMessage " + msg.msgText + " " + msg.receivedNumber;
        Debug.Log(txt);
        MessageManager.inst.ShowMessage(txt);
    }

    [ContextMenu("Send damage")]
    public void SendDamage()
    {
        TakeDamage(number);
    }

    [Command]
    public void CmdSetInstrument(int num)
    {
        instrument = (Musicians) Enum.GetValues(typeof(Musicians)).GetValue(num);
    }

    [Command]
    public void CmdSetupClientInfo()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        MessageManager.inst.AddClientInfo(deviceID);
    }

    public void TakeDamage(int amount)
    {
        if(!isServer) return;
        health -= amount;
        RpcDamage(amount);
    }

    [ClientRpc(includeOwner = false)]
    public void RpcDamage(int amount)
    {
        string txt = "Took damage:" + amount;
        Debug.Log(txt);
        MessageManager.inst.ShowMessage(txt);
    }

    public void InstrumentMsg(PlayerTest targetObj)
    {
        if (!isServer) return;
        TargetInstrumentMsg(targetObj.connectionToClient, targetObj.instrument.ToString() + " from serv", targetObj);
    }
   

    [TargetRpc]
    public void TargetInstrumentMsg(NetworkConnection target, string instrName, PlayerTest player)
    {
        if (!player.isLocalPlayer) return;
        string txt = "Took damage:" + instrName;
        Debug.Log(txt);
        MessageManager.inst.ShowMessage(txt);
    }

    public void PlayVideo()
    {
        if (!isServer) return;
        RpcPlayVideo();
    }

    [ClientRpc(includeOwner = true)]
    public void RpcPlayVideo()
    {
        MessageManager.inst.PlayVideo();
    }

    public void PauseVideo()
    {
        if (!isServer) return;
        RpcPauseVideo();
    }

    [ClientRpc(includeOwner = true)]
    public void RpcPauseVideo()
    {
        MessageManager.inst.PauseVideo();
    }
}
