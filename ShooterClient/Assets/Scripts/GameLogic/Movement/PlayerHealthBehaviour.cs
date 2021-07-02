using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthBehaviour : MonoBehaviour
{
    public Health health = new Health(3);

    public event Action<int> OnHealthChanged;
    public event Action<int> OnGetDamage;

    private void OnEnable() => BindUI();
    private void OnDisable() => UnBindUI();

    public void DealDamage(int damage)
    {
        health.DealDamage(damage);
        OnHealthChanged?.Invoke(health.currentHealth);
        OnGetDamage?.Invoke(damage);
    }

    public void ResetHealth()
    {
        health.currentHealth = health.maxHealth;
        OnHealthChanged?.Invoke(health.currentHealth);
    }

    private void BindUI()
    {
        FindObjectOfType<HealthUI>().Bind(this);
        FindObjectOfType<PlayerHealthStatusUI>().Bind(this);
        OnHealthChanged?.Invoke(health.currentHealth);
    }

    private void UnBindUI()
    {
        //sometimes cannot find healthui => nullref
        FindObjectOfType<HealthUI>()?.UnBind(this);
        FindObjectOfType<PlayerHealthStatusUI>()?.UnBind(this);
    }
}
