using UnityEngine;

public class GranadeLaucher : Weapon<GranadeLaucherData>
{
    public override void Shoot()
    {
        if (weaponData.ammoCurrent > 0 && shootCD.isReady)
        {
            netRequest?.SpawnObject(weaponData.missilePrefub.assetID, rayProvider.transform.position, weaponData.missilePrefub.transform.localScale, rayProvider.transform.rotation);
            weaponData.ammoCurrent--;
            UpdateStatus();
            shootCD.Reset();
        }
    }
}
