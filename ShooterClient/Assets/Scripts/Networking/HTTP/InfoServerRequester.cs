using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public static class InfoServerRequester
{
    public static bool TryGetServerInfo(IPEndPoint server, out ServerInfo info)
    {
        try
        {
            TcpClient client = new TcpClient();
            client.Connect(server);

            var stream = new StreamReader(client.GetStream());

            var jsonString = stream.ReadToEnd();
            info = JsonUtility.FromJson<ServerInfo>(jsonString);
            client.Close();
            return true;
        }
        catch
        {
            info = null;
            return false;
        }
    }
}
