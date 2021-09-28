using Assets.Scripts.Game.Network.Packets.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Network.Packets
{
    public class PacketHandler
    {

        private HashSet<GenericPacket> _packets = new HashSet<GenericPacket>();

        public PacketHandler()
        {
            RegisterPacket<PlayPacket>(1);
            RegisterPacket<OpponentFoundPacket>(2);
            RegisterPacket<BoatPositionsPacket>(3);
            RegisterPacket<GameStartPacket>(4);
            RegisterPacket<PlayerWinPacket>(5);
            RegisterPacket<PlayerLeftPacket>(6);
            RegisterPacket<AttackPacket>(7);
            RegisterPacket<BoatStatusPacket>(8);
            RegisterPacket<TurnPacket>(9);
            RegisterPacket<PlayerLoosePacket>(10);
        }

        public Type GetType(int packetId)
        {
            GenericPacket packet = _packets.FirstOrDefault(p => p.Id.Equals(packetId));
            if(packet != null)
            {
                return packet.GetType();
            }
            return null;
        }

        public void RegisterPacket<T>(int id) where T : GenericPacket
        {
            try
            {
                GenericPacket packet = (GenericPacket)Activator.CreateInstance(typeof(T));
                packet.Id = id;
                _packets.Add(packet);
            } catch(Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
        
    }
}
