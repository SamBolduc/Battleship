using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    public class PlayerWinPacket : GenericPacket
    {

        public PlayerWinPacket() : base(5)
        {
        }

        public override void Read()
        {
            SceneManager.LoadScene(8);
        }
    }
}
