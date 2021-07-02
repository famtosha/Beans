using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public void Bind(PlayerHealthBehaviour playerHeath)
    {
        playerHeath.OnHealthChanged += UpdateText;
    }

    public void UnBind(PlayerHealthBehaviour playerHeath)
    {
        playerHeath.OnHealthChanged -= UpdateText;
    }

    private void UpdateText(int health)
    {
        healthText.text = health.ToString();
    }
}
