using PacketManager;

namespace PacketManager
{
    public class SpawnSynchronizedObjectPacket : ITCPPacket
    {
        public int packetID => 13;

        public ITCPPacket CreateInstance() => new SpawnSynchronizedObjectPacket();

        public int assetID;
        public int synchronizedObjectID;
        public byte[] objectInitData;

        public SpawnSynchronizedObjectPacket()
        {

        }

        public SpawnSynchronizedObjectPacket(int assetID, int synchronizedObjectID, byte[] objectInitData)
        {
            this.assetID = assetID;
            this.synchronizedObjectID = synchronizedObjectID;
            this.objectInitData = objectInitData;
        }

        public void Read(StreamWrapper stream)
        {
            assetID = stream.ReadInt32();
            synchronizedObjectID = stream.ReadInt32();
            objectInitData = stream.ReadBytes(stream.ReadInt32());
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(assetID);
            stream.WriteInt32(synchronizedObjectID);
            stream.WriteInt32(objectInitData.Length);
            stream.WriteBytes(objectInitData);
        }
    }
}