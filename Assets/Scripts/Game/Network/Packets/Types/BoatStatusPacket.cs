using Assets.Scripts.BoatSelector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    public class BoatStatusPacket : GenericPacket
    {
        public List<Boat> myBoats { get; set; }
        public List<Boat> enemyBoats { get; set; }

        public BoatStatusPacket() : base(8)
        {
        }

        public override void Read()
        {
            Game.UpdateBoats(myBoats, enemyBoats);
        }
    }
}
