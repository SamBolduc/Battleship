using Newtonsoft.Json;
using Assets.Scripts.Game;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    class PlayPacket : GenericPacket
    {

        [JsonProperty("username")]
        public string Username { get; set; }

        public PlayPacket() : base(1)
        {
        }

        public override void Read(Game game)
        {
            // Do something...
            OverlayManager manager = UnityEngine.Object.FindObjectOfType<OverlayManager>();
            if (manager == null) return;

            game.Username = Username;

            SceneManager.LoadScene(3);
            manager.DisplayText("En attente d'un adversaire...", "", 5);
        }
    }
}
