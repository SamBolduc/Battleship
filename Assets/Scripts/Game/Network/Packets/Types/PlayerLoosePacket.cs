using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    public class PlayerLoosePacket : GenericPacket
    {

        public PlayerLoosePacket() : base(10)
        {
        }

        public override void Read()
        {
            SceneManager.LoadScene(10);
        }
    }
}
