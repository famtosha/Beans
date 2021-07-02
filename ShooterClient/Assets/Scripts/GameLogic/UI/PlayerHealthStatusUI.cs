using UnityEngine;

public class PlayerHealthStatusUI : MonoBehaviour
{
    public Animator animator;

    public void Bind(PlayerHealthBehaviour playerHealth)
    {
        playerHealth.OnGetDamage += OnHealthChanged;
    }

    public void UnBind(PlayerHealthBehaviour playerHealth)
    {
        playerHealth.OnGetDamage -= OnHealthChanged;
    }

    public void OnHealthChanged(int damageAmount)
    {
        animator.SetTrigger("DamageGetted");
    }
}
