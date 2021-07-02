using PacketManager;

namespace PacketManager
{
    public class PingPacket : ITCPPacket
    {
        public int packetID => 17;

        public ITCPPacket CreateInstance() => new PingPacket();

        public void Read(StreamWrapper stream)
        {

        }

        public void Write(StreamWrapper stream)
        {

        }
    }
}