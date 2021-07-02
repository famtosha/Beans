using UnityEngine;

namespace PacketManager
{
    public class SpawnPlayerPacket : ITCPPacket
    {
        public int packetID => 0;
        public int playerID;
        public Vector3 position;
        public Quaternion rotation;
        public string playerName;

        public ITCPPacket CreateInstance() => new SpawnPlayerPacket();

        public SpawnPlayerPacket() { }
        public SpawnPlayerPacket(int id, Vector3 position, Quaternion rotation, string name)
        {
            this.playerID = id;
            this.position = position;
            this.rotation = rotation;
            this.playerName = name;
        }

        public void Read(StreamWrapper stream)
        {
            playerID = stream.ReadInt32();
            position = stream.ReadVector3();
            rotation = stream.ReadQuaternion();
            playerName = stream.ReadString();
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(playerID);
            stream.WriteVector3(position);
            stream.WriteQuaternion(rotation);
            stream.WriteString(playerName);
        }
    }
}
