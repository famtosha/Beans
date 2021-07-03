using System.IO;
using PacketManager;
using UnityEngine;

public class StaticLevelObject : MonoBehaviour, ISynchronizedObject
{
    [SerializeField] private int _assetID;
    public int assetID { get => _assetID; set => _assetID = value; }
    public int objectID { get; set; }

    protected SynchronizedObjectList _synchronizedObjectList;

    public SynchronizedObjectList synchronizedObjectList => _synchronizedObjectList;

    protected virtual void Awake()
    {
        _synchronizedObjectList = FindObjectOfType<SynchronizedObjectList>();
    }

    public void InitSynchronizedObject(byte[] data)
    {

    }

    public void DestroySynchronizedObject()
    {
        Destroy(gameObject);
    }

    public virtual byte[] GetObjectData()
    {
        using (var stream = new MemoryStream())
        {
            stream.WriteVector3(transform.position);
            stream.WriteQuaternion(transform.rotation);
            stream.WriteVector3(transform.localScale);
            GetObjectData(stream);
            return stream.ToArray();
        }
    }

    protected virtual void GetObjectData(MemoryStream stream)
    {

    }

    public void SetObjectData(byte[] data)
    {
        using (var stream = new MemoryStream(data))
        {
            SetTransformData(stream.ReadVector3(), stream.ReadQuaternion(), stream.ReadVector3());
            SetObjectData(stream);
        }
    }

    protected virtual void SetTransformData(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
    }

    protected virtual void SetObjectData(MemoryStream stream)
    {

    }
}
