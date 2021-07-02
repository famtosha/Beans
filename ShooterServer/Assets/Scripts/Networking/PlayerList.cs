using PacketManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

public class PlayerList
{
    public Client[] players;
    public int maxPlayers;

    private Action<Exception, Client> _errorCallback;
    private Map _map;

    public PlayerList(int maxPlayers, Action<Exception, Client> errorCallback, Map map)
    {
        this.maxPlayers = maxPlayers;
        _errorCallback = errorCallback;
        players = new Client[maxPlayers];
        _map = map;
    }

    public int GetPlayerCount()
    {
        int count = 0;
        foreach (var player in players)
        {
            if (player != null) count++;
        }
        return count;
    }

    public bool TryAddPlayer(TcpClient netClient, out Client client)
    {
        client = null;
        if (players.GetEmptyIndex(out var index))
        {
            client = AddPlayer(index, netClient);
        }
        return client != null;
    }

    private Client AddPlayer(int id, TcpClient netClient)
    {
        Client client = new Client(netClient, id, _errorCallback);
        players[id] = client;
        client.WritePacket(new PlayerIDPacket(id));
        client.position = _map.GetSpawnPoint();
        players.ForeachExpect((anotherPlayer) => anotherPlayer.WritePacket(new SpawnPlayerPacket(client.id, client.position, client.rotation, client.name)), id);
        players.Foreach((anotherPlayer) => client.WritePacket(new SpawnPlayerPacket(anotherPlayer.id, anotherPlayer.position, anotherPlayer.rotation, anotherPlayer.name)));
        players.ForeachExpect(x => client.WritePacket(new ChangeWeaponPacket(x.id, x.currentWeapon)), id);
        Log.Write($"client connected, ID: {id} IP: {netClient.Client.RemoteEndPoint}");
        return client;
    }

    public Dictionary<Client, ITCPPacket> ReadPackets()
    {
        var packets = new Dictionary<Client, ITCPPacket>();

        foreach (var player in players)
        {
            if (player != null && player.ReadPacket(out var packet))
            {
                packets.Add(player, packet);
            }
        }

        return packets;
    }

    public void RemovePlayer(int id)
    {
        var client = players[id];
        players.ForeachExpect(anotherPlayer => anotherPlayer.WritePacket(new DespawnPlayerPacket(id)), id);
        players[id] = null;
        client.Stop();
        Log.Write($"disconnected {id}");
    }

    public void UpdatePosition(int id, Vector3 newPosition, Quaternion newRotation)
    {
        players[id].position = newPosition;
        players[id].rotation = newRotation;
    }

    public void ChangedName(int id, string name)
    {
        players[id].name = name;
        players.ForeachExpect(anotherPlayer => anotherPlayer.WritePacket(new PlayerNamePacket(id, name)), id);
        Log.Write($"client {id} change name to {name}");
    }

    public void Shoot(int id, Vector3[] hits)
    {
        players.ForeachExpect(anotherPlayer => anotherPlayer.WritePacket(new ShootPacket(id, hits)), id);
    }

    public void DealDamage(int shooter, int shooted, int damage)
    {
        players.Foreach(anotherPlayer => anotherPlayer.WritePacket(new DealDamgePacket(shooter, shooted, damage)));
    }

    public void Death(int killed, int killer)
    {
        Log.Write($"Killed: {killed}");
        players.Foreach(p => p.WritePacket(new DeathPacket(killed, killer)));
        players.Foreach(p => p.WritePacket(new RespawnPacket(killed, _map.GetSpawnPoint(), Quaternion.identity)));
    }

    public void ChangeWeapon(int playerID, int weaponID)
    {
        players[playerID].currentWeapon = weaponID;
        players.ForeachExpect(x => x.WritePacket(new ChangeWeaponPacket(playerID, weaponID)), playerID);
    }

    public void RealodWeapon(int playerID)
    {
        players.ForeachExpect(x => x.WritePacket(new ReloadWeaponPacket(playerID)), playerID);
    }
}
