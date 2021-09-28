using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    public class OpponentFoundPacket : GenericPacket
    {

        public string test { get; set; }

        public OpponentFoundPacket() : base(2)
        {

        }

        public override void Read()
        {
            Debug.LogWarning("Opponent found");
            SceneManager.LoadScene(4);
        }
    }
}
