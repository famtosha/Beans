using UnityEngine;

public class DeadBodyRagdoll : MonoBehaviour
{
    public GameObject[] BodyParts;
    public float force;
    public Timer despawnTimer = new Timer(10);

    private MeshRenderer meshRenderer;

    public void Awake()
    {
        despawnTimer.Reset();
        foreach (var bodyPart in BodyParts)
        {
            var direction = (bodyPart.transform.position - transform.position).normalized;
            bodyPart?.GetComponent<Rigidbody>().AddForce(direction * force);
        }
    }

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        despawnTimer.UpdateTimer(Time.deltaTime);
        if (despawnTimer.isReady)
        {
            TryDespawn();
        }
    }

    private void TryDespawn()
    {
        if (!meshRenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
