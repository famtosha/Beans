using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shotgun : Weapon<ShotgunData>
{
    public override void Shoot()
    {
        if (weaponData.ammoCurrent > 0 && shootCD.isReady)
        {
            List<GameObject> hittedObjects = new List<GameObject>();
            Vector3[] hits = new Vector3[weaponData.shellCount];
            for (int i = 0; i < weaponData.shellCount; i++)
            {
                var dispertion = MathHelper.RandomVector3(weaponData.dispersion);
                if (rayProvider.MakeRay(transform.forward + dispertion, weaponData.range, out var hit))
                {
                    hittedObjects.Add(hit.collider.gameObject);
                    hits[i] = hit.point;
                }
                else
                {
                    hits[i] = rayProvider.transform.position + (rayProvider.transform.forward + dispertion) * weaponData.range;
                }
            }

            netRequest.DealDamage(hittedObjects.ToArray(), weaponData.damage);
            netRequest.ShotShoot(hits);

            foreach (var item in hittedObjects.Distinct())
            {
                if (item.TryGetComponent(out IDamagable target)) { target.DealDamage(weaponData.damage); }
            }
            foreach (var hit in hits)
            {
                weaponUI.ShowHit(hit);
            }

            weaponData.ammoCurrent--;
            UpdateStatus();
            shootCD.Reset();
        }
    }
}
