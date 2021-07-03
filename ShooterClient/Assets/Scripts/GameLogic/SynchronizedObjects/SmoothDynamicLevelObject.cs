using UnityEngine;

public class SmoothDynamicLevelObject : DynamicLevelObject
{
    private NetworkObjectInterpolation _interpolation = new NetworkObjectInterpolation(0.1f);

    private void Update()
    {
        _interpolation.UpdateT();
        transform.position = _interpolation.GetCurrentPosition();
        transform.rotation = _interpolation.GetCurrentRotation();
    }

    protected override void SetTransformData(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        base.SetTransformData(position, rotation, scale);
        _interpolation.UpdatePosition(position, rotation);
    }
}
