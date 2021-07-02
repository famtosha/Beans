public interface ISynchronizedObjectList
{
    void SpawnSynchronizedObject(int assetID, int synchronizedObjectID, byte[] objectInitData);
    void SpawnSynchronizedObject(int assetID, byte[] objectInitData);
    void DestroySynchronizedObjectByServer(int synchronizedObjectID);
    void SetSynchronizedObjectData(int synchronizedObjectID, byte[] data);
}
