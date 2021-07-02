using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public interface ISynchronizedObject
{
    int assetID { get; set; }
    int objectID { get; set; }
    void InitSynchronizedObject(byte[] data);
    void DestroySynchronizedObject();
    void SetObjectData(byte[] data);
    byte[] GetObjectData();
}
