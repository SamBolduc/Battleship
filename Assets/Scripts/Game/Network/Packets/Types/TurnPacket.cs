using Assets.Scripts.BoatSelector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    public class TurnPacket : GenericPacket
    {
        public bool turn { get; set; }

        public TurnPacket() : base(9)
        {
        }

        public override void Read()
        {
            Game.turn = turn;
            Game game = Object.FindObjectOfType<Game>();
            game.ShowOverlay();
        }
    }
}
