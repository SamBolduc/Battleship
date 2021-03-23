using Assets.Scripts.Game.Network.Packets;
using Assets.Scripts.Game.Network.Packets.Types;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Game : MonoBehaviour
{

    void Start()
    {
        NetworkManager.Instance.Init();
        NetworkManager.Instance.onReception = NetworkReceptionEvent;

        new PlayPacket()
        {
            Username = "coulis"
        }.Send();
    }

    
    void Update()
    {
        NetworkManager.Instance.ReceiveMsg();
    }

    void NetworkReceptionEvent(byte[] data)
    {
        string msg = Encoding.UTF8.GetString(data);
        JsonMessage jsonMsg = JsonConvert.DeserializeObject<JsonMessage>(msg);

        Type packetType = NetworkManager.PacketHandler.GetType(jsonMsg.PacketId);
        GenericPacket packet = (GenericPacket)JsonConvert.DeserializeObject(jsonMsg.Content, packetType);

        packet.Read(this);
        
        Debug.LogWarning("[MESSAGE] " + Encoding.UTF8.GetString(data));
    }


}
