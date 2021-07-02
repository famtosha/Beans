using UnityEngine;

public class Pistol : Weapon<PistolData>
{
    public override void Shoot()
    {
        if (weaponData.ammoCurrent > 0 && shootCD.isReady)
        {
            var dispertion = MathHelper.RandomVector3(weaponData.dispersion);
            Vector3 hitPoint;
            if (rayProvider.MakeRay(transform.forward + dispertion, weaponData.range, out var hit))
            {
                hitPoint = hit.point;
                netRequest.DealDamage(hit.collider.gameObject, weaponData.damage);
                if (hit.collider.gameObject.TryGetComponent(out IDamagable target)) { target.DealDamage(weaponData.damage); }
            }
            else
            {
                hitPoint = rayProvider.transform.position + (rayProvider.transform.forward + dispertion) * weaponData.range;
            }
            netRequest.ShotShoot(hitPoint);
            weaponUI.ShowHit(hitPoint);

            weaponData.ammoCurrent--;
            UpdateStatus();
            shootCD.Reset();
        }
    }
}
