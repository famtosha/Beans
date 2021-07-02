using UnityEngine;

public class BillboardText : MonoBehaviour
{
    private Transform cameraPosition;

    private void Start()
    {
        cameraPosition = FindCamera();
    }

    void Update()
    {
        if (cameraPosition == null)
        {
            cameraPosition = FindCamera();
        }
        else
        {
            transform.rotation = cameraPosition.rotation;
        }
    }

    public Transform FindCamera()
    {
        return Camera.main?.transform;
    }
}
