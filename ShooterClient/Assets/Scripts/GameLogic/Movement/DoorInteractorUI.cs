using UnityEngine;
using TMPro;

public class DoorInteractorUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _info;

    public void ShowInfo()
    {
        _info.text = "Open";
    }

    public void HideInfo()
    {
        _info.text = "";
    }
}
