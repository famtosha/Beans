namespace PacketManager
{
    public class DespawnPlayerPacket : ITCPPacket
    {
        public int packetID => 1;
        public int playerID;

        public ITCPPacket CreateInstance() => new DespawnPlayerPacket();

        public DespawnPlayerPacket() { }
        public DespawnPlayerPacket(int playerID)
        {
            this.playerID = playerID;
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
