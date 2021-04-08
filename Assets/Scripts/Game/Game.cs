using Assets.Scripts.Game.Network.Packets;
using Assets.Scripts.Game.Network.Packets.Types;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Game : MonoBehaviour
    {
        public string Username { get; set; }
        public GameState State { get; set; }

        void Start()
        {
            this.State = GameState.NONE;

            NetworkManager.Instance.onReception = NetworkReceptionEvent;
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
        }
    }
}