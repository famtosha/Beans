namespace PacketManager
{
    public interface ITCPPacket
    {
        int packetID { get; }
        ITCPPacket CreateInstance();
        void Read(StreamWrapper stream);
        void Write(StreamWrapper stream);
    }
}
