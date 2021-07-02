using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData/WeaponData")]
public class WeaponData : ActionItemData
{
    public int damage;
    public float recoil;
    public float dispersion;
    public float shootRate;
    public float range;
    public int ammoMax;
    public int ammoCurrent;
}
