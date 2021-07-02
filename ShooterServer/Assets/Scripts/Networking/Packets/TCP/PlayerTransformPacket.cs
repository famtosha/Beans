using UnityEngine;

namespace PacketManager
{
    public class PlayerTransformPacket : ITCPPacket
    {
        public int packetID => 2;

        public int playerID;
        public Vector3 position;
        public Quaternion rotation;

        public ITCPPacket CreateInstance() => new PlayerTransformPacket();

        public PlayerTransformPacket()
        {

        }

        public PlayerTransformPacket(int playerID, Vector3 position, Quaternion rotation)
        {
            this.playerID = playerID;
            this.position = position;
            this.rotation = rotation;
        }

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
