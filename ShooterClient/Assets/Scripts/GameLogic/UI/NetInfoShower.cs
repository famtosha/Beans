using UnityEngine;
using TMPro;

public class NetInfoShower : MonoBehaviour
{
    public TextMeshProUGUI lastUpdatePacketText;
    public TextMeshProUGUI pingText;

    public void SetUpdatePacketText(string text)
    {
        lastUpdatePacketText.text = "UPPS:" + text;
    }

    public void SetPing(float ping)
    {
        ping *= 1000;
        ping = Mathf.RoundToInt(ping);
        pingText.text = $"ping:{ping}(ms)";
    }
}
