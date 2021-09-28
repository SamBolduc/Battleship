using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    class JsonMessage
    {
        [JsonProperty("packetId")]
        public int PacketId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
