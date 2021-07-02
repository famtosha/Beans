using UnityEngine;

[SelectionBase]
public class Window : StaticLevelObject, IDamagable
{
    public ParticleSystem particlePrefub;

    public void DealDamage(int ammout)
    {
        Instantiate(particlePrefub, transform.position, Quaternion.identity);
        _synchronizedObjectList.DestroySynchronizedObjectByClient(objectID);
    }
}