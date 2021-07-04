using PacketManager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SynchronizedObjectList : MonoBehaviour, ISynchronizedObjectList
{
    public Dictionary<int, ISynchronizedObject> synchronizedObjects = new Dictionary<int, ISynchronizedObject>();
    public SynchronizedObjectsFacotry factory;

    public ClientBehaviour client;

    private void OnEnable()
    {
        client.packetHandlers[Packet.SpawnSynchronizedObject].PacketHandled += OnSpawnSynchronizedObject;
        client.packetHandlers[Packet.DespawnSynchronizedObject].PacketHandled += OnDestroySynchronizedObject;
        client.packetHandlers[Packet.SetSynchronizedObjectData].PacketHandled += OnSetSynchronizedObjectData;
    }

    public void DataUpdated(ISynchronizedObject obj)
    {
        var data = obj.GetObjectData();
        client.WritePacket(new SetSynchronizedObjectDataPacket(obj.objectID, data));
    }

    private void OnSpawnSynchronizedObject(ITCPPacket packet)
    {
        var p = packet as SpawnSynchronizedObjectPacket;
        SpawnSynchronizedObject(p.assetID, p.synchronizedObjectID, p.objectInitData);
    }

    public void SpawnSynchronizedObject(int assetID, int synchronizedObjectID, byte[] objectInitData)
    {
        var newSynchronizedObject = factory.Create(assetID);
        newSynchronizedObject.SetObjectData(objectInitData);
        newSynchronizedObject.objectID = synchronizedObjectID;
        synchronizedObjects.Add(synchronizedObjectID, newSynchronizedObject);
    }

    public void SpawnSynchronizedObject(int assetID, byte[] objectInitData)
    {
        int synchronizedObjectID = GetSmalletID();
        var newSynchronizedObject = factory.Create(assetID);
        newSynchronizedObject.SetObjectData(objectInitData);
        newSynchronizedObject.objectID = synchronizedObjectID;
        synchronizedObjects.Add(synchronizedObjectID, newSynchronizedObject);
    }

    private void OnDestroySynchronizedObject(ITCPPacket packet)
    {
        var p = packet as DespawnSynchronizedObjectPacket;
        DestroySynchronizedObjectByServer(p.synchronizedObjectID);
    }

    public void DestroySynchronizedObjectByServer(int synchronizedObjectID)
    {
        Debug.Log($"Destroyed objectID: {synchronizedObjectID} AssetID: {synchronizedObjects.TryGetValue(synchronizedObjectID, out var obj)},{obj.assetID}");
        synchronizedObjects[synchronizedObjectID].DestroySynchronizedObject();
        synchronizedObjects.Remove(synchronizedObjectID);
    }

    public void DestroySynchronizedObjectByClient(int synchronizedObjectID)
    {
        client.WritePacket(new DespawnSynchronizedObjectPacket(synchronizedObjectID));
        Debug.Log($"Destroyed objectID: {synchronizedObjectID} AssetID: {synchronizedObjects.TryGetValue(synchronizedObjectID,out var obj)},{obj.assetID}");
        synchronizedObjects[synchronizedObjectID].DestroySynchronizedObject();
        synchronizedObjects.Remove(synchronizedObjectID);
    }

    private void OnSetSynchronizedObjectData(ITCPPacket packet)
    {
        var p = packet as SetSynchronizedObjectDataPacket;
        SetSynchronizedObjectData(p.synchronizedObjectID, p.synchronizedObjectData);
    }

    public void SetSynchronizedObjectData(int synchronizedObjectID, byte[] data)
    {
        synchronizedObjects[synchronizedObjectID].SetObjectData(data);
    }

    private int GetSmalletID()
    {
        return synchronizedObjects.Keys.ToList().Min();
    }
}