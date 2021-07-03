using UnityEngine;

public class RayProvider : MonoBehaviour
{
    [SerializeField] private LayerMask _contancs;

    public bool MakeRay(Vector3 direction, float distance, out RaycastHit hit)
    {
        var result = Physics.Raycast(transform.position, direction, out hit, distance, _contancs);
        return result;
    }
}
