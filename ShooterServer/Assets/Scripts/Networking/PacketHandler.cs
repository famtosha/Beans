using System;
using UnityEngine;

namespace PacketManager
{
    public struct PacketHandler
    {
        public event PacketHandled PacketHandled;

        public void Invoke(Client sender, ITCPPacket packet)
        {
            if (PacketHandled != null)
            {
                PacketHandled.Invoke(sender, packet);
            }
            else
            {
                Log.Write($"Packet {packet.packetID} wasn't handled");
            }
        }
    }

    public delegate void PacketHandled(Client sender, ITCPPacket packet);
}