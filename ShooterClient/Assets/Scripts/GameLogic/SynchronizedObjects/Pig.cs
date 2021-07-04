using UnityEngine;

[RequireComponent(typeof(SmoothDynamicLevelObject))]
public class Pig : MonoBehaviour, IDamagable
{
    private SmoothDynamicLevelObject _smoothDynamicLevelObject;

    private void Awake()
    {
        _smoothDynamicLevelObject = GetComponent<SmoothDynamicLevelObject>();
    }

    public void DealDamage(int ammout)
    {
        _smoothDynamicLevelObject.synchronizedObjectList.DestroySynchronizedObjectByClient(_smoothDynamicLevelObject.objectID);
    }
}
