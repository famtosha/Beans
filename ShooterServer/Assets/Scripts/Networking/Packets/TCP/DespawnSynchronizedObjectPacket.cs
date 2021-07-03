using PacketManager;
using UnityEngine;

namespace PacketManager
{
    public class DespawnSynchronizedObjectPacket : ITCPPacket
    {
        public int packetID => 14;

        public ITCPPacket CreateInstance() => new DespawnSynchronizedObjectPacket();

        public int synchronizedObjectID;

        public DespawnSynchronizedObjectPacket()
        {
        }

        public DespawnSynchronizedObjectPacket(int synchronizedObjectID)
        {
            this.synchronizedObjectID = synchronizedObjectID;
        }

        public void Read(StreamWrapper stream)
        {
            synchronizedObjectID = stream.ReadInt32();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(synchronizedObjectID);
        }
    }
}