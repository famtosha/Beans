using System;

[Serializable]
public class Health
{
    public int maxHealth;
    public int currentHealth;
    public event Action OnDeath;

    public Health(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) OnDeath?.Invoke();
    }
}
