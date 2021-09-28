using Assets.Scripts.Game.Network.Packets.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Network.Packets
{
    public abstract class GenericPacket
    {

        [JsonIgnore]
        public int Id { get; set; }

        public GenericPacket(int id)
        {
            Id = id;
        }

        public abstract void Read();
        
        public void Send()
        {
            JsonMessage jsonMsg = new JsonMessage()
            {
                PacketId = Id,
                Content = JsonConvert.SerializeObject(this)
            };
            NetworkManager.Instance.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(jsonMsg)));
        }
    }
}
