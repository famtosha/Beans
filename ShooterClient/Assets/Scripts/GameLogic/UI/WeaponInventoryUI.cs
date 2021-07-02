using UnityEngine;
using TMPro;

public class WeaponInventoryUI : MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponAmmo;

    public void UpdateAmmoInfo(string ammo)
    {
        weaponAmmo.text = ammo;
    }

    public void UpdateNameInfo(string name)
    {
        weaponAmmo.text = name;
    }

    public void Bind(WeaponInventory weapon)
    {
        weapon.WeaponInfoChanged += OnInfoUpdated;
    }

    public void UnBind(WeaponInventory weapon)
    {
        weapon.WeaponInfoChanged -= OnInfoUpdated;
    }

    private void OnInfoUpdated(string name, string ammo)
    {
        UpdateNameInfo(name);
        UpdateAmmoInfo(ammo);
    }
}
