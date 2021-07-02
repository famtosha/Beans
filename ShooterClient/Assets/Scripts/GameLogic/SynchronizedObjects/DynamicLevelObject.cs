using PacketManager;
using System.IO;

public class DynamicLevelObject : StaticLevelObject
{
    protected override void SetObjectData(MemoryStream stream)
    {
        base.SetObjectData(stream);
    }
}
