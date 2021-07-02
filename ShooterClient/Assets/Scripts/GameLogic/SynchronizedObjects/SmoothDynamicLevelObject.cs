using UnityEngine;

public class SmoothDynamicLevelObject : DynamicLevelObject
{
    private float _t;
    private float _sendCD = 0.1f;

    private Vector3 _prevPosition1;
    private Quaternion _prevRotation1;

    private Vector3 _prevPosition2;
    private Quaternion _prevRotation2;

    private void Update()
    {
        _t += Time.deltaTime;

        transform.position = Vector3.Lerp(_prevPosition2, _prevPosition1, _t / _sendCD);
        transform.rotation = Quaternion.Lerp(_prevRotation2, _prevRotation1, _t / _sendCD);
    }

    protected override void SetTransformData(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        base.SetTransformData(position, rotation, scale);

        _prevPosition2 = _prevPosition1;
        _prevRotation2 = _prevRotation1;

        _prevPosition1 = position;
        _prevRotation1 = rotation;
        _t = 0;
    }
}
