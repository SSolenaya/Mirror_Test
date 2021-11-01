using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerTest : NetworkBehaviour
{
    public string message;
    public int number;
    private int health = 100;
    [SyncVar] public bool isServer;

    public struct Message : NetworkMessage
    {
        public string msgText;
        public int receivedNumber;
    }

    public override void OnStartClient()
    {
        if (isServer && isClient)
        {
            Debug.Log(gameObject.name);
        }
       
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
    public void SetupClient()
    {
        NetworkClient.RegisterHandler<Message>(OnMessage);
        NetworkClient.Connect("localhost");
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

    public void TakeDamage(int amount)
    {
        if(!isServer) return;
        health -= amount;
        RpcDamage(amount);
    }
    [ClientRpc]
    public void RpcDamage(int amount)
    {
        string txt = "Took damage:" + amount;
        Debug.Log(txt);
        MessageManager.inst.ShowMessage(txt);
    }

    
}
