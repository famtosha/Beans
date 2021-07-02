namespace PacketManager
{
    public class DeathPacket : ITCPPacket
    {
        public int packetID => 9;

        public int killedID;
        public int killerID;

        public ITCPPacket CreateInstance() => new DeathPacket();

        public DeathPacket() { }
        public DeathPacket(int killedID, int killerID)
        {
            this.killedID = killedID;
            this.killerID = killerID;
        }

        public void Read(StreamWrapper stream)
        {
            killedID = stream.ReadInt32();
            killerID = stream.ReadInt32();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(killedID);
            stream.WriteInt32(killerID);
        }
    }
}
