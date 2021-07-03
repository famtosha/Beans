using PacketManager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SynchronizedObjectList : ISynchronizedObjectList
{
    public SynchronizedObjectDictionary synchronizedObjects = new SynchronizedObjectDictionary();

    private ISynchronizedObjectsFacotry _factory;
    private Server _server;
    private GameObject _rootObject;

    public SynchronizedObjectList(Server server, GameObject rootObject, ISynchronizedObjectsFacotry factory)
    {
        _server = server;
        _rootObject = rootObject;
        _factory = factory;

        _server.packetHandlers[Packet.SpawnSynchronizedObjectRequest].PacketHandled += OnSpawnSynchronizedObject;
        _server.packetHandlers[Packet.DespawnSynchronizedObject].PacketHandled += OnDestroySynchronizedObject;
        _server.packetHandlers[Packet.SetSynchronizedObjectData].PacketHandled += OnSetSynchronizedObjectData;

        AddObjectsFromMap();

        _server.ClientConnected += OnClientConnected;
    }

    ~SynchronizedObjectList()
    {
        _server.ClientConnected -= OnClientConnected;
    }

    private void OnClientConnected(Client client)
    {
        foreach (var obj in synchronizedObjects)
        {
            client.WritePacket(new SpawnSynchronizedObjectPacket(obj.Value.assetID, obj.Key, obj.Value.GetObjectData()));
        }
    }

    private void AddObjectsFromMap()
    {
        List<Transform> c = _rootObject.transform.GetAllChilds();
        foreach (var obj in c)
        {
            var temp = obj.GetComponent<ISynchronizedObject>();
            if (temp != null)
            {
                obj.parent = null;
                AddSyncronyzedObject(temp);
            }
        }
    }

    private void AddSyncronyzedObject(ISynchronizedObject obj)
    {
        int id = GetSmalletID();
        synchronizedObjects.Add(id, obj);
        obj.objectID = id;

    }

    private void OnSpawnSynchronizedObject(Client sender, ITCPPacket packet)
    {
        var p = packet as SpawnSynchronizedObjectRequestPacket;
        SpawnSynchronizedObject(p.assetID, p.position, p.rotaion, p.scale);
    }

    public void SpawnSynchronizedObject(int assetID, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        int synchronizedObjectID = GetSmalletID();
        var newSynchronizedObject = _factory.Create(assetID, position, rotation, scale);
        newSynchronizedObject.objectID = synchronizedObjectID;
        synchronizedObjects.Add(synchronizedObjectID, newSynchronizedObject);
        _server.players.players.Foreach(x => x.WritePacket(new SpawnSynchronizedObjectPacket(assetID, synchronizedObjectID, newSynchronizedObject.GetObjectData())));
    }

    private void OnDestroySynchronizedObject(Client sender, ITCPPacket packet)
    {
        var p = packet as DespawnSynchronizedObjectPacket;
        DestroySynchronizedObject(p.synchronizedObjectID);
        _server.players.players.ForeachExpect(x => x.WritePacket(packet), sender.id);
    }

    public void DestroySynchronizedObjectByServer(int id)
    {
        DestroySynchronizedObject(id);
        _server.players.players.Foreach(x => x.WritePacket(new DespawnSynchronizedObjectPacket(id)));
    }

    public void DestroySynchronizedObject(int synchronizedObjectID)
    {
        if (!synchronizedObjects.ContainsKey(synchronizedObjectID)) return;
        synchronizedObjects[synchronizedObjectID].DestroySynchronizedObject();
        synchronizedObjects.Remove(synchronizedObjectID);
    }

    private void OnSetSynchronizedObjectData(Client sender, ITCPPacket packet)
    {
        var p = packet as SetSynchronizedObjectDataPacket;
        SetSynchronizedObjectData(p.synchronizedObjectID, p.synchronizedObjectData);
        _server.players.players.ForeachExpect(x => x.WritePacket(packet), sender.id);
    }

    public void SetSynchronizedObjectData(int synchronizedObjectID, byte[] data)
    {
        synchronizedObjects[synchronizedObjectID].SetObjectData(data);
    }

    public void UpdateSynchronizedObjectData(int synchronizedObjectID)
    {
        var data = synchronizedObjects[synchronizedObjectID].GetObjectData();
        _server.players.players.Foreach(x => x.WritePacket(new SetSynchronizedObjectDataPacket(synchronizedObjectID, data)));
    }

    private int GetSmalletID()
    {
        if (synchronizedObjects.Keys.Count == 0) return 0;
        return synchronizedObjects.Keys.ToList().Max() + 1;
    }
}