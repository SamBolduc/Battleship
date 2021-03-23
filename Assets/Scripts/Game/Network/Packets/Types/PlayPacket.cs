using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    class PlayPacket : GenericPacket
    {

        [JsonProperty("username")]
        public string Username { get; set; }

        public PlayPacket() : base(1)
        {
        }

        public override void Read(MonoBehaviour game)
        {
            Debug.LogWarning(Username);
        }
    }
}
