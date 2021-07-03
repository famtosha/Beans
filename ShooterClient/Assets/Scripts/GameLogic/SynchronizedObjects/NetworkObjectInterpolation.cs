using UnityEngine;

public class NetworkObjectInterpolation
{
    private Vector3 _prevPosition1;
    private Quaternion _prevRotation1;

    private Vector3 _prevPosition2;
    private Quaternion _prevRotation2;

    private float _sendCD;
    private float _t;

    public NetworkObjectInterpolation(float sendCD)
    {
        _sendCD = sendCD;
    }

    public void UpdateT()
    {
        _t += Time.deltaTime;
    }

    public void UpdatePosition(Vector3 newPosition, Quaternion newRotation)
    {
        _prevPosition2 = _prevPosition1;
        _prevRotation2 = _prevRotation1;

        _prevPosition1 = newPosition;
        _prevRotation1 = newRotation;

        _t = 0;
    }

    public Vector3 GetCurrentPosition()
    {
        return Vector3.Lerp(_prevPosition2, _prevPosition1, _t / _sendCD);
    }

    public Quaternion GetCurrentRotation()
    {
        return Quaternion.Lerp(_prevRotation2, _prevRotation1, _t / _sendCD);
    }
}
