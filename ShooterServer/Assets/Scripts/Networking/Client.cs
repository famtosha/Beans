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

    private PacketReader _packetReader;
    private PacketWriter _packetWriter;

    private NetworkStream _stream;
    private StreamWrapper _wrappedStream;

    private Action<Exception, Client> _networkExcetionCallback;

    public Client(TcpClient client, int id, Action<Exception, Client> networkExcetionCallback)
    {
        this.client = client;
        this.id = id;
        _stream = client.GetStream();
        _wrappedStream = new StreamWrapper(_stream);
        _packetReader = new PacketReader(_wrappedStream, HandleNetworkException);
        _packetWriter = new PacketWriter(_wrappedStream, HandleNetworkException);
        this._networkExcetionCallback = networkExcetionCallback;
    }

    public bool ReadPackets(out ITCPPacket packet)
    {
        return _packetReader.ReadPacket(out packet);
    }

    public void WritePacket(ITCPPacket packet)
    {
        _packetWriter.WritePacket(packet);
    }

    public bool ReadPacket(out ITCPPacket packet)
    {
        return _packetReader.ReadPacket(out packet);
    }

    public void Stop()
    {
        client.Close();
        _packetReader = null;
        _packetWriter = null;
    }

    private void HandleNetworkException(Exception ex)
    {
        _networkExcetionCallback(ex, this);
    }
}
