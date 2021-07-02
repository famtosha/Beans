using System;
using System.Net.Sockets;
using PacketManager;
using UnityEngine;

public class Client
{
    public TcpClient client;
    public int id;

    public Vector3 position;
    public Quaternion rotation;
    public string name = "player";
    public int currentWeapon;

    private PacketReader packetReader;
    private PacketWriter packetWriter;

    private NetworkStream stream;
    private StreamWrapper wrappedStream;

    private Action<Exception, Client> networkExcetionCallback;

    public Client(TcpClient client, int id, Action<Exception, Client> networkExcetionCallback)
    {
        this.client = client;
        this.id = id;
        stream = client.GetStream();
        wrappedStream = new StreamWrapper(stream);
        packetReader = new PacketReader(wrappedStream, HandleNetworkException);
        packetWriter = new PacketWriter(wrappedStream, HandleNetworkException);
        this.networkExcetionCallback = networkExcetionCallback;
    }

    public bool ReadPackets(out ITCPPacket packet)
    {
        return packetReader.ReadPacket(out packet);
    }

    public void WritePacket(ITCPPacket packet)
    {
        packetWriter.WritePacket(packet);
    }

    public bool ReadPacket(out ITCPPacket packet)
    {
        return packetReader.ReadPacket(out packet);
    }

    public void Stop()
    {
        client.Close();
        packetReader = null;
        packetWriter = null;
    }

    private void HandleNetworkException(Exception ex)
    {
        networkExcetionCallback(ex, this);
    }
}
