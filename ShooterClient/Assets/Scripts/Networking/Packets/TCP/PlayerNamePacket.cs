namespace PacketManager
{
    public class PlayerNamePacket : ITCPPacket
    {
        public int packetID => 4;

        public int playerID;
        public string name;

        public ITCPPacket CreateInstance() => new PlayerNamePacket();

        public PlayerNamePacket() { }
        public PlayerNamePacket(int playerID, string name)
        {
            this.name = name;
            this.playerID = playerID;
        }

        public void Read(StreamWrapper stream)
        {
            playerID = stream.ReadInt32();
            name = stream.ReadString();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(playerID);
            stream.WriteString(name);
        }
    }
}
