using System.IO;
using PacketManager;
using UnityEngine;

[SelectionBase]
public class Door : StaticLevelObject
{
    private bool _isOpened;
    public bool isOpened
    {
        get => _isOpened;
        set
        {
            _isOpened = value;
        }
    }

    public void ChangeState()
    {
        isOpened = !isOpened;
    }

    protected override void SetObjectData(MemoryStream stream)
    {
        base.SetObjectData(stream);
        isOpened = stream.ReadBool();
    }

    protected override void GetObjectData(MemoryStream stream)
    {
        base.GetObjectData(stream);
        stream.WriteBool(isOpened);
    }
}
