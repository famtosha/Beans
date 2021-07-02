using UnityEngine;

public interface ISynchronizedObjectList
{
    void SpawnSynchronizedObject(int assetID, Vector3 position, Quaternion rotation, Vector3 scale);
    void DestroySynchronizedObject(int synchronizedObjectID);
    void SetSynchronizedObjectData(int synchronizedObjectID, byte[] data);
}
