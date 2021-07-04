using System;
using System.Collections.Generic;
using PacketManager;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    public AnotherPlayerBehaviour[] players = new AnotherPlayerBehaviour[128];
    public PlayerHealthBehaviour player;

    public GameObject playerPrefub;
    public GameObject anotherPlayerPrefub;
    public GameObject deadBody;

    public float sendCD = 0.05f;

    public ClientBehaviour client;

    private void Start()
    {
        client = FindObjectOfType<ClientBehaviour>();
    }

    private void OnEnable()
    {
        client.packetHandlers[Packet.Spawn].PacketHandled += SpawnPlayer;
        client.packetHandlers[Packet.Despawn].PacketHandled += DespawnPlayer;
        client.packetHandlers[Packet.Move].PacketHandled += MovePlayer;
        client.packetHandlers[Packet.ChangeName].PacketHandled += ChangeName;
        client.packetHandlers[Packet.PlayerShoot].PacketHandled += Shoot;
        client.packetHandlers[Packet.DealDamage].PacketHandled += DealDamage;
        client.packetHandlers[Packet.Death].PacketHandled += Death;
        client.packetHandlers[Packet.Respawn].PacketHandled += Respawn;
        client.packetHandlers[Packet.ChangeWeapon].PacketHandled += ChangeWeapon;
        client.packetHandlers[Packet.ReloadWeapon].PacketHandled += Reload;
    }

    private void OnDisable()
    {
        client.packetHandlers[Packet.Spawn].PacketHandled -= SpawnPlayer;
        client.packetHandlers[Packet.Despawn].PacketHandled -= DespawnPlayer;
        client.packetHandlers[Packet.Move].PacketHandled -= MovePlayer;
        client.packetHandlers[Packet.ChangeName].PacketHandled -= ChangeName;
        client.packetHandlers[Packet.PlayerShoot].PacketHandled -= Shoot;
        client.packetHandlers[Packet.DealDamage].PacketHandled -= DealDamage;
        client.packetHandlers[Packet.Death].PacketHandled -= Death;
        client.packetHandlers[Packet.Respawn].PacketHandled -= Respawn;
        client.packetHandlers[Packet.ChangeWeapon].PacketHandled -= ChangeWeapon;
        client.packetHandlers[Packet.ReloadWeapon].PacketHandled -= Reload;
    }

    private void SpawnPlayer(ITCPPacket packet)
    {
        var spawnPacket = packet as SpawnPlayerPacket;

        if (spawnPacket.playerID != client.client.id)
        {
            var clientGameObject = Instantiate(anotherPlayerPrefub);
            var anotherPlayer = clientGameObject.GetComponentInChildren<AnotherPlayerBehaviour>();
            anotherPlayer.id = spawnPacket.playerID;
            anotherPlayer.ChangeName(spawnPacket.playerName);
            players[anotherPlayer.id] = anotherPlayer;
        }
        else
        {
            var player = Instantiate(playerPrefub, spawnPacket.position, Quaternion.identity);
            this.player = player.GetComponent<PlayerHealthBehaviour>();
        }
        Debug.Log($"spawn player: {spawnPacket.playerID}");
    }

    private void DespawnPlayer(ITCPPacket packet)
    {
        var despawnPacket = packet as DespawnPlayerPacket;
        var temp = players[despawnPacket.playerID];
        players[despawnPacket.playerID] = null;
        Destroy(temp.gameObject);
    }

    private void MovePlayer(ITCPPacket packet)
    {
        var movePacket = packet as PlayerTransformPacket;
        Debug.Log($"Recived update of:{movePacket.playerID}; we are:{client.id}");
        if (movePacket.playerID != client.client.id)
        {
            players[movePacket.playerID].Move(movePacket.position, movePacket.rotation);
        }
    }

    private void ChangeName(ITCPPacket packet)
    {
        var changeNamePacket = packet as PlayerNamePacket;
        players[changeNamePacket.playerID].ChangeName(changeNamePacket.name);
    }

    private void Shoot(ITCPPacket packet)
    {
        var shootPacket = packet as ShootPacket;
        if (shootPacket.shooterID != client.id)
        {
            players[shootPacket.shooterID].Shoot(shootPacket.hits);
        }
    }

    private void DealDamage(ITCPPacket packet)
    {
        var p = packet as DealDamgePacket;
        if (p.shootedID == client.id)
        {
            player.DealDamage(p.damage);
            if (player.health.currentHealth <= 0) client.WritePacket(new DeathPacket(p.shootedID, p.shooterID));
            // this is wronge
        }
        else
        {
            players[p.shootedID].ShowDamage(p.damage);
        }
    }

    private void Death(ITCPPacket packet)
    {
        var p = packet as DeathPacket;
        Transform origin;
        if (p.killedID == client.id)
        {
            origin = client.transform;
        }
        else
        {
            origin = players[p.killedID].transform;
            players[p.killedID].DisableVisual();
        }
        Instantiate(deadBody, origin.position, origin.rotation);
    }

    private void Respawn(ITCPPacket packet)
    {
        var p = packet as RespawnPacket;
        var playerID = p.playerID;
        if (playerID == client.id)
        {
            player.GetComponent<PlayerMovement>().Move(p.position, p.rotation);
            player.ResetHealth();
        }
        else
        {
            var anotherPlayer = players[playerID];
            anotherPlayer.EnableVisual();
            anotherPlayer.Move(p.position, p.rotation);
        }
    }

    private void Reload(ITCPPacket packet)
    {
        var p = packet as ReloadWeaponPacket;
        players[p.playerID].Reload();
    }

    private void ChangeWeapon(ITCPPacket packet)
    {
        var p = packet as ChangeWeaponPacket;
        if (p.playerID != client.id)
        {
            players[p.playerID].ChangeWeapon(p.weaponID);
        }
    }
}
