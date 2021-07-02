namespace PacketManager
{
    public class DealDamgePacket : ITCPPacket
    {
        public int packetID => 8;

        public int shooterID;
        public int shootedID;
        public int damage;

        public ITCPPacket CreateInstance() => new DealDamgePacket();

        public DealDamgePacket() { }

        public DealDamgePacket(int shooterID, int shootedID, int damage)
        {
            this.shooterID = shooterID;
            this.shootedID = shootedID;
            this.damage = damage;
        }

        public void Read(StreamWrapper stream)
        {
            shootedID = stream.ReadInt32();
            damage = stream.ReadInt32();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(shootedID);
            stream.WriteInt32(damage);
        }
    }
}
