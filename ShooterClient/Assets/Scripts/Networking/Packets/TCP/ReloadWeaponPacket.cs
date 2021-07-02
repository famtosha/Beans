namespace PacketManager
{
    public class ReloadWeaponPacket : ITCPPacket
    {
        public int packetID => 12;

        public ITCPPacket CreateInstance() => new ReloadWeaponPacket();

        public int playerID;

        public ReloadWeaponPacket()
        {

        }

        public ReloadWeaponPacket(int playerID)
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
