using UnityEngine;

public class WeaponVisualBehaviour : MonoBehaviour
{
    public GameObject laserPrefub;
    public Transform shootOrigin;

    public void ShowReload()
    {

    }

    public void ShowHit(Vector3 hit)
    {
        DrawLaser(shootOrigin.position, hit);
    }

    private void DrawLaser(Vector3 origin, Vector3 end)
    {
        var laser = Instantiate(laserPrefub, origin, Quaternion.identity);
        laser.transform.forward = end - origin;
        laser.GetComponent<Laser>().StartMove(origin, end, 50);
    }

}
