using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game.Network.Packets.Types
{

    class GameStartPacket : GenericPacket
    {

        public bool turn { get; set; }

        public GameStartPacket() : base(4) { 

        }

        public override void Read()
        {
            SceneManager.LoadScene(3);
            Game.turn = turn;
        }
    }
}
