using Assets.Scripts.BoatSelector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Network.Packets.Types
{
    public class AttackPacket : GenericPacket
    {
        public float x { get; set; }
        public float y { get; set; }
        public int damageDealt { get; set; }

        public AttackPacket() : base(7)
        {
        }

        public override void Read()
        {
            AttackLog log = new AttackLog();
            log.x = x;
            log.y = y;
            log.DamageDealt = damageDealt;

            Game.AttackLogs.Add(log);
        }
    }
}
