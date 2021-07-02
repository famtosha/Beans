namespace PacketManager
{
    public class PlayerIDPacket : ITCPPacket
    {
        public int packetID => 3;

        public int id;

        public ITCPPacket CreateInstance() => new PlayerIDPacket();

        public PlayerIDPacket() { }
        public PlayerIDPacket(int id)
        {
            this.id = id;
        }

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
