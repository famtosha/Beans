using PacketManager;
using UnityEngine;

public class NetInfoRecorder : MonoBehaviour
{
    public const float PING_UPDATE_RATE = 0.25f;

    private Timer pingSendCD = new Timer(PING_UPDATE_RATE);

    private float lastPacketSended = 0;
    private NetInfoShower netInfo;
    private ClientBehaviour client;

    private void Start()
    {
        netInfo = FindObjectOfType<NetInfoShower>();
        client = FindObjectOfType<ClientBehaviour>();
        client.packetHandlers[Packet.PingPacket].PacketHandled += OnPingRecived;
    }

    private void OnDisable()
    {
        client.packetHandlers[Packet.PingPacket].PacketHandled -= OnPingRecived;
    }

    private void Update()
    {
        lastPacketSended += Time.deltaTime;
        pingSendCD.UpdateTimer(Time.deltaTime);
        if (pingSendCD.isReady) SendPingPacket();
    }

    private void OnPingRecived(ITCPPacket packet)
    {
        netInfo.SetPing(lastPacketSended);
    }

    private void SendPingPacket()
    {
        client.client.WritePacket(new PingPacket());
        lastPacketSended = 0;
        pingSendCD.Reset();
    }
}
