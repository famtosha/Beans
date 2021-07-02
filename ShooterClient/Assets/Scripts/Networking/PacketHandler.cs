using System;
using UnityEngine;

namespace PacketManager
{
    public struct PacketHandler
    {
        public event Action<ITCPPacket> PacketHandled;

        public void Invoke(ITCPPacket packet)
        {
            if (PacketHandled != null)
            {
                PacketHandled.Invoke(packet);
            }
            else
            {
                Debug.LogError($"Packet {packet.packetID} wasn't handled");
            }
        }
    }
}