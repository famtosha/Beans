using UnityEngine;

public class RayProvider : MonoBehaviour
{
    public LayerMask contancs;

    public bool MakeRay(Vector3 direction, float distance, out RaycastHit hit)
    {
        var result = Physics.Raycast(transform.position, direction, out hit, distance, contancs);
        return result;
    }
}
