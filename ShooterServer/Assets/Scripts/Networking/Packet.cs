namespace PacketManager
{
    public static class Packet
    {
        public static int Spawn = 00;
        public static int Despawn = 01;
        public static int Move = 02;
        public static int SetID = 03;
        public static int ChangeName = 04;
        public static int KeepAlive = 05;
        public static int Disconnect = 06;
        public static int PlayerShoot = 07;
        public static int DealDamage = 08;
        public static int Death = 09;
        public static int Respawn = 10;
        public static int ChangeWeapon = 11;
        public static int ReloadWeapon = 12;
        public static int SpawnSynchronizedObject = 13;
        public static int DespawnSynchronizedObject = 14;
        public static int SetSynchronizedObjectData = 15;
        public static int SpawnSynchronizedObjectRequest = 16;
        public static int PingPacket = 17;
    }
}
