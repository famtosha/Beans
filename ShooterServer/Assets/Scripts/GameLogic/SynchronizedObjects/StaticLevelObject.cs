using System.Collections.Generic;
using System.IO;
using PacketManager;
using UnityEngine;

public class StaticLevelObject : MonoBehaviour, ISynchronizedObject
{
    [SerializeField] private int _assetID;
    public int objectID { get; set; }
    public int assetID { get => _assetID; set => _assetID = value; }

    protected SynchronizedObjectList _sol;
    public SynchronizedObjectList sol => _sol;

    protected virtual void Start()
    {
        _sol = FindObjectOfType<Map>().objectList;
    }

    public void InitSynchronizedObject(byte[] data)
    {

    }

    public void DestoryByServer()
    {
        _sol.DestroySynchronizedObjectByServer(objectID);
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
            transform.position = stream.ReadVector3();
            transform.rotation = stream.ReadQuaternion();
            transform.localScale = stream.ReadVector3();
            SetObjectData(stream);
        }
    }

    protected virtual void SetObjectData(MemoryStream stream)
    {

    }
}
