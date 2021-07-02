using System;
using System.Collections.Generic;
using System.Linq;

namespace PacketManager
{
    public class PacketReader
    {
        public StreamWrapper stream;
        public ITCPPacket[] packets = new ITCPPacket[128];
        private Action<Exception> failCallback;

        public PacketReader(StreamWrapper stream, Action<Exception> failCallback)
        {
            this.stream = stream;
            this.failCallback = failCallback;
            AddPackets();
        }

        public void AddPackets()
        {
            var packetInterface = typeof(ITCPPacket);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => packetInterface.IsAssignableFrom(p))
                .Where(p => p.IsClass);

            foreach (var type in types)
            {
                var instance = (ITCPPacket)Activator.CreateInstance(type);
                AddPacket(instance);
            }
        }

        public void AddPacket(ITCPPacket packet)
        {
            packets[packet.packetID] = packet;
        }

        public bool ReadPacket(out ITCPPacket packet)
        {
            return SaftyRead(out packet);
        }

        private bool SaftyRead(out ITCPPacket packet)
        {
            try
            {
                bool result = false;
                packet = null;
                if (stream.DataAvailable)
                {
                    int packetID = stream.ReadInt32();
                    packet = packets[packetID].CreateInstance();
                    packet.Read(stream);
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                failCallback(ex);
                packet = null;
                return false;
            }
        }
    }
}
