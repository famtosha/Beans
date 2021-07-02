using System;

namespace PacketManager
{

    public class PacketWriter
    {
        public StreamWrapper stream;
        private Action<Exception> failCallback;

        public PacketWriter(StreamWrapper stream, Action<Exception> failCallback)
        {
            this.stream = stream;
            this.failCallback = failCallback;
        }

        public void WritePacket(ITCPPacket packet)
        {
            SaftyWrite(packet);
        }

        private void SaftyWrite(ITCPPacket packet)
        {
            try
            {
                stream.WriteInt32(packet.packetID);
                packet.Write(stream);
            }
            catch (Exception ex)
            {
                failCallback(ex);
            }
        }
    }
}
