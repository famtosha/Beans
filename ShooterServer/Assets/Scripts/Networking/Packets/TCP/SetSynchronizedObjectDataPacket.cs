using PacketManager;
namespace PacketManager
{
    public class SetSynchronizedObjectDataPacket : ITCPPacket
    {
        public int packetID => 15;

        public ITCPPacket CreateInstance() => new SetSynchronizedObjectDataPacket();

        public int synchronizedObjectID;
        public byte[] synchronizedObjectData;

        public SetSynchronizedObjectDataPacket()
        {

        }

        public SetSynchronizedObjectDataPacket(int synchronizedObjectID, byte[] synchronizedObjectData)
        {
            this.synchronizedObjectID = synchronizedObjectID;
            this.synchronizedObjectData = synchronizedObjectData;
        }

        public void Read(StreamWrapper stream)
        {
            synchronizedObjectID = stream.ReadInt32();
            synchronizedObjectData = stream.ReadBytes(stream.ReadInt32());
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(synchronizedObjectID);
            //TODO: replace with WriteArray(byte[] array);
            stream.WriteInt32(synchronizedObjectData.Length);
            stream.WriteBytes(synchronizedObjectData);
        }
    }
}