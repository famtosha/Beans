namespace PacketManager
{
    public class KeepAlivePacket : ITCPPacket
    {
        public int packetID => 5;

        public int id;

        public ITCPPacket CreateInstance() => new KeepAlivePacket();

        public KeepAlivePacket() { }
        public KeepAlivePacket(int id) { }

        public void Read(StreamWrapper stream)
        {
            id = stream.ReadInt32();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(id);
        }
    }
}
