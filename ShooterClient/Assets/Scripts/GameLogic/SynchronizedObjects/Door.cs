using PacketManager;
using System.IO;
using UnityEngine;

[SelectionBase]
public class Door : StaticLevelObject
{
    [SerializeField] private Animator _animator;

    private bool _isOpened;
    public bool isOpened
    {
        get => _isOpened;
        set
        {
            _isOpened = value;
            _animator.SetBool("IsOpened", _isOpened);
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeState()
    {
        isOpened = !isOpened;
        FindObjectOfType<SynchronizedObjectList>().DataUpdated(this);
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
