using UnityEngine;

[SelectionBase]
public class Window : StaticLevelObject, IDamagable
{
    public void DealDamage(int ammout)
    {
        _synchronizedObjectList.DestroySynchronizedObjectByClient(objectID);
    }
}