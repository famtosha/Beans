using UnityEngine;
using TMPro;

public class FpsMeter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private void Update()
    {
        fpsText.text = "Fps:"+ (Mathf.RoundToInt(1f / Time.deltaTime)).ToString();
    }
}
