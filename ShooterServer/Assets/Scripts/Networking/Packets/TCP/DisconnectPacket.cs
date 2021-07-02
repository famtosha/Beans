namespace PacketManager
{
    public class DisconnectPacket : ITCPPacket
    {
        public int packetID => 6;

        public ITCPPacket CreateInstance() => new DisconnectPacket();

        public int playerID;

        public DisconnectPacket() { }
        public DisconnectPacket(int id)
        {
            playerID = id;
        }

        public void Read(StreamWrapper stream)
        {
            playerID = stream.ReadInt32();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(playerID);
        }
    }
}
