using System;

namespace PacketManager
{
    public interface IUDPPacket
    {
        int packetID { get; }
        IUDPPacket CreateInstance();
        void Read(byte[] bytes);
        byte[] Write();
    }
}
