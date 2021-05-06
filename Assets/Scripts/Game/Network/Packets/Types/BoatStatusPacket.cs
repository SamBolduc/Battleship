using Assets.Scripts.BoatSelector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    public class BoatStatusPacket : GenericPacket
    {
        public List<Boat> myBoats { get; }
        public List<Boat> enemyBoats { get; }

        public BoatStatusPacket() : base(8)
        {
        }

        public override void Read()
        {
            Game.UpdateBoats(this);
        }
    }
}
