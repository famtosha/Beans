using UnityEngine;

namespace PacketManager
{
    public class ShootPacket : ITCPPacket
    {
        public int packetID => 7;

        public int shooterID;
        public Vector3[] hits;

        public ITCPPacket CreateInstance() => new ShootPacket();

        public ShootPacket() { }
        public ShootPacket(int shooterID, Vector3[] hits)
        {
            this.shooterID = shooterID;
            this.hits = hits;
        }

        public ShootPacket(int id, Vector3 hit)
        {
            shooterID = id;
            hits = new Vector3[] { hit };
        }

        public void Read(StreamWrapper stream)
        {
            shooterID = stream.ReadInt32();
            int hitCount = stream.ReadInt32();
            hits = new Vector3[hitCount];
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i] = stream.ReadVector3();
            }
        }

        public void Write(StreamWrapper stream)
        {
            stream.WriteInt32(shooterID);
            stream.WriteInt32(hits.Length);
            foreach (var hit in hits)
            {
                stream.WriteVector3(hit);
            }
        }
    }
}
