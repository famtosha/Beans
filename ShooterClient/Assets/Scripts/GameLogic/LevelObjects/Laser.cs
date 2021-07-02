using UnityEngine;

public class Laser : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;
    private float t = 0;
    private float moveSpeed;
    private float distance;

    public void StartMove(Vector3 start, Vector3 end, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.start = start;
        this.end = end;
        this.distance = Vector3.Distance(start, end);
    }

    private void Update()
    {
        t += moveSpeed * Time.deltaTime / distance;
        transform.position = Vector3.Lerp(start, end, t);
        if (t >= 1) Destroy(gameObject);
    }
}
