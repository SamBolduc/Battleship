using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    class PlayerLeftPacket : GenericPacket
    {

        public PlayerLeftPacket() : base(6)
        {

        }

        public override void Read()
        {
            SceneManager.LoadScene(8);
        }
    }
}
