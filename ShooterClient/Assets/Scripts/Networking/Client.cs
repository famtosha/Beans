using System;
using System.Net;
using System.Net.Sockets;
using PacketManager;
using UnityEngine;

public class Client
{
    public TcpClient client = new TcpClient();
    public PacketReader packetReader;
    public PacketWriter packetWriter;

    public int id = -1;

    private NetworkStream stream;
    private StreamWrapper wrappedStream;

    public void Start(IPEndPoint ip)
    {
        client.Connect(ip);
        stream = client.GetStream();
        wrappedStream = new StreamWrapper(stream);
        packetReader = new PacketReader(wrappedStream, HandleNetworkException);
        packetWriter = new PacketWriter(wrappedStream, HandleNetworkException);
    }

    public void WritePacket(ITCPPacket packet)
    {
        packetWriter.WritePacket(packet);
    }

    private void HandleNetworkException(Exception ex)
    {
        Stop();
        Debug.LogError($"Failed to read/write to sever: {ex}");
        Application.Quit();
    }

    public void Stop()
    {
        packetReader = null;
        packetWriter = null;
        client.Close();
    }
}