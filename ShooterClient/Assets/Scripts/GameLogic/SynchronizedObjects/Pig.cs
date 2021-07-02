public class Pig : SmoothDynamicLevelObject, IDamagable
{
    public void DealDamage(int ammout)
    {
        _synchronizedObjectList.DestroySynchronizedObjectByClient(objectID);
    }
}