using PacketManager;
using System;
using UnityEngine;
using System.Linq;
using System.Net.Sockets;
using System.Collections.Generic;

public class Server
{
    public int port;
    public int maxPlayers = 32;
    public PacketHandler[] packetHandlers = new PacketHandler[128];
    public TcpListener server;
    public ClientAccepter clientAccepter;
    public PlayerList players;

    public static float dynamicObjectUpdateRate = 0.1f;

    public event Action<Client> ClientConnected;
    public event Action<Client> ClientDisconnecting;

    private Map _map;

    public Server(int port, ClientAccepter clientAccepter, Map map)
    {
        this.port = port;
        _map = map;
        this.clientAccepter = clientAccepter;
        this.players = new PlayerList(maxPlayers, HandleNetworkException, _map);

        server = TcpListener.Create(port);
        server.Start();

        packetHandlers[Packet.ChangeName].PacketHandled += NameChanged;
        packetHandlers[Packet.Move].PacketHandled += PositionUpdated;
        packetHandlers[Packet.Disconnect].PacketHandled += ClientDisconncted;
        packetHandlers[Packet.PlayerShoot].PacketHandled += Shoot;
        packetHandlers[Packet.DealDamage].PacketHandled += DealDamage;
        packetHandlers[Packet.Death].PacketHandled += Death;
        packetHandlers[Packet.ChangeWeapon].PacketHandled += ChangeWeapon;
        packetHandlers[Packet.ReloadWeapon].PacketHandled += Reload;
        packetHandlers[Packet.PingPacket].PacketHandled += Ping;
    }

    public void Stop()
    {
        server.Stop();
    }

    public void ReadPackets()
    {
        var readedPackets = players.ReadPackets();
        foreach (var packet in readedPackets)
        {
            packetHandlers[packet.Value.packetID].Invoke(packet.Key, packet.Value);
        }
    }

    public void HandleNetworkException(Exception ex, Client client)
    {
        players.RemovePlayer(client.id);
        Log.WriteWarning($"client {client.id} got network exception {ex}");
    }

    public void ConnectClient(TcpClient netClient)
    {
        if (players.TryAddPlayer(netClient, out var client))
        {
            ClientConnected?.Invoke(client);
        }
    }

    public void DisconnectClient(Client client)
    {
        ClientDisconnecting?.Invoke(client);
        players.RemovePlayer(client.id);
    }

    private void NameChanged(Client sender, ITCPPacket packet)
    {
        var p = packet as PlayerNamePacket;
        players.ChangedName(p.playerID, p.name);
    }

    private void PositionUpdated(Client sender, ITCPPacket packet)
    {
        var p = packet as PlayerTransformPacket;
        players.UpdatePosition(sender.id, p.position, p.rotation);
    }

    private void ClientDisconncted(Client sender, ITCPPacket packet)
    {
        DisconnectClient(sender);
    }

    private void Shoot(Client sender, ITCPPacket packet)
    {
        var p = packet as ShootPacket;
        players.Shoot(sender.id, p.hits);
    }

    private void DealDamage(Client sender, ITCPPacket packet)
    {
        var p = packet as DealDamgePacket;
        players.DealDamage(sender.id, p.shootedID, p.damage);
    }

    private void Death(Client sender, ITCPPacket packet)
    {
        var p = packet as DeathPacket;
        players.Death(p.killedID, p.killerID);
    }

    private void ChangeWeapon(Client sender, ITCPPacket packet)
    {
        var p = packet as ChangeWeaponPacket;
        players.ChangeWeapon(sender.id, p.weaponID);
    }

    private void Reload(Client sender, ITCPPacket packet)
    {
        players.RealodWeapon(sender.id);
    }

    private void Ping(Client sender, ITCPPacket packet)
    {
        sender.WritePacket(packet);
    }
}