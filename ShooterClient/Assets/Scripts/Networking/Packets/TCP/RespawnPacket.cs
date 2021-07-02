using UnityEngine;

namespace PacketManager
{
    public class RespawnPacket : ITCPPacket
    {
        public Vector3 position;
        public Quaternion rotation;

        public int packetID => 10;

        public int playerID;

        public RespawnPacket() { }

        public RespawnPacket(int playerID, Vector3 position, Quaternion rotation)
        {
            this.playerID = playerID;
            this.position = position;
            this.rotation = rotation;
        }

        public ITCPPacket CreateInstance() => new RespawnPacket();

        public void Read(StreamWrapper stream)
        {
            playerID = stream.ReadInt32();
            position = stream.ReadVector3();
            rotation = stream.ReadQuaternion();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(playerID);
            stream.WriteVector3(position);
            stream.WriteQuaternion(rotation);
        }
    }
}
