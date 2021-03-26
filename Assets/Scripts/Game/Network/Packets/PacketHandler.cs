﻿using Assets.Scripts.Game.Network.Packets.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Network.Packets
{
    public class PacketHandler
    {

        private HashSet<GenericPacket> _packets = new HashSet<GenericPacket>();

        public PacketHandler()
        {
            RegisterPacket<PlayPacket>(1);
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
                Console.WriteLine(e.Message);
            }
        }
        
    }
}