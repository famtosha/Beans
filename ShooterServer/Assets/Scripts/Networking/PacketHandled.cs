using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketManager
{
    public delegate void PacketHandled(Client sender, ITCPPacket packet);
}
