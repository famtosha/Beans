using UnityEngine;
using System.Net;
using PacketManager;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;

public class ClientBehaviour : MonoBehaviour
{
    public Client client = new Client();
    public PacketHandler[] packetHandlers = new PacketHandler[128];
    public SynchronizedObjectList levelObjects;
    public int id => client.id;
    private string playerName;

    private Stopwatch _readTimer = new Stopwatch();

    private bool _isActive = false;

    public void ConnectToServer(IPEndPoint ip, string name)
    {
        client.Start(ip);
        _isActive = true;
        this.playerName = name;
        Debug.Log($"connected to {ip}");
    }

    public void DisconnectFromServer()
    {
        client.WritePacket(new DisconnectPacket(client.id));
        client.Stop();
        _isActive = false;
        Debug.Log($"disconnected");
        SceneLoader.instance.LoadMainMenu();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) DisconnectFromServer();
        if (_isActive)
        {
            _readTimer.Restart();
            _readTimer.Start();
            var readedPackets = ReadAllPackets();
            if (readedPackets.Count > 0)
            {
                foreach (var packet in readedPackets)
                {
                    _readTimer.Restart();
                    packetHandlers[packet.packetID].Invoke(packet);
                }
                _readTimer.Stop();
            }
        }
    }

    private void OnEnable()
    {
        packetHandlers[Packet.SetID].PacketHandled += OnPlayerIDSetted;
    }

    private void OnDisable()
    {
        packetHandlers[Packet.SetID].PacketHandled -= OnPlayerIDSetted;
        if (client.client.Connected) client.WritePacket(new DisconnectPacket(client.id));
    }

    public void WritePacket(ITCPPacket packet)
    {
        client.packetWriter.WritePacket(packet);
    }

    public bool ReadPacket(out ITCPPacket packet)
    {
        return client.packetReader.Read(out packet);
    }

    public List<ITCPPacket> ReadAllPackets()
    {
        List<ITCPPacket> packets = new List<ITCPPacket>();
        while (ReadPacket(out var packet))
        {
            packets.Add(packet);
        }
        return packets;
    }

    private void OnPlayerIDSetted(ITCPPacket packet)
    {
        var playerIDpacket = packet as PlayerIDPacket;
        WritePacket(new PlayerNamePacket(playerIDpacket.id, playerName));
        client.id = playerIDpacket.id;
    }
}
