using Assets.Scripts.BoatSelector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    public class BoatPositionsPacket : GenericPacket
    {

        public Board Board { get; set; }

        public BoatPositionsPacket() : base(3)
        {
        }

        public override void Read()
        {
        }
    }
}
