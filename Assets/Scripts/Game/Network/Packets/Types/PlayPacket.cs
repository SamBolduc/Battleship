using Newtonsoft.Json;
using Assets.Scripts.Game;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    public class PlayPacket : GenericPacket
    {

        [JsonProperty("username")]
        public string Username { get; set; }

        public PlayPacket() : base(1)
        {
        }

        public override void Read()
        {
        }
    }
}
