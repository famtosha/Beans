namespace PacketManager
{
    public class ChangeWeaponPacket : ITCPPacket
    {
        public int packetID => 11;

        public int playerID;
        public int weaponID;

        public ITCPPacket CreateInstance() => new ChangeWeaponPacket();

        public ChangeWeaponPacket(int playerID, int weaponID)
        {
            this.weaponID = weaponID;
            this.playerID = playerID;
        }

        public ChangeWeaponPacket()
        {

        }

        public void Read(StreamWrapper stream)
        {
            playerID = stream.ReadInt32();
            weaponID = stream.ReadInt32();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(playerID);
            stream.WriteInt32(weaponID);
        }
    }
}
