using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PacketManager
{
    public class SpectialDecoders
    {
        public string ReadZeroString(byte[] bytes, out int stringEnd)
        {
            stringEnd = 0;
            for (; stringEnd < bytes.Length; stringEnd++) if (bytes[stringEnd] == 0x0) break;
            return Encoding.UTF8.GetString(bytes.Take(stringEnd).ToArray());
        }

        public static string ReadZeroString(Stream stream)
        {
            List<byte> bytes = new List<byte>();
            while (true)
            {
                var temp = (byte)stream.ReadByte();
                if (temp == 0x0) break;
                bytes.Add(temp);
            }
            return Encoding.UTF8.GetString(bytes.ToArray());
        }
    }
}
