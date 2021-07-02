using PacketManager;
using UnityEngine;
namespace PacketManager
{
    public class SpawnSynchronizedObjectRequestPacket : ITCPPacket
    {
        public int packetID => 16;

        public ITCPPacket CreateInstance() => new SpawnSynchronizedObjectRequestPacket();

        public int assetID;
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotaion;

        public SpawnSynchronizedObjectRequestPacket()
        {

        }

        public SpawnSynchronizedObjectRequestPacket(int assetID, Vector3 position, Vector3 scale, Quaternion rotaion)
        {
            this.assetID = assetID;
            this.position = position;
            this.scale = scale;
            this.rotaion = rotaion;
        }

        public void Read(StreamWrapper stream)
        {
            assetID = stream.ReadInt32();
            position = stream.ReadVector3();
            rotaion = stream.ReadQuaternion();
            scale = stream.ReadVector3();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(assetID);
            stream.WriteVector3(position);
            stream.WriteQuaternion(rotaion);
            stream.WriteVector3(scale);
        }
    }
}