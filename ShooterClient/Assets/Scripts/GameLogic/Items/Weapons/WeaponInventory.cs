using System;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public WeaponBase activeWeapon => weapons[activeWeaponSlot];
    public WeaponBase[] weapons;

    public event Action<string, string> WeaponInfoChanged;

    private int _activeWeapon;
    public int activeWeaponSlot
    {
        get => _activeWeapon;
        set
        {
            activeWeapon.OnStatusChanged -= UpdateUI;
            DisableWeapon(_activeWeapon);
            _activeWeapon = value;
            if (_activeWeapon > weapons.Length - 1) _activeWeapon = 0;
            if (_activeWeapon < 0) _activeWeapon = weapons.Length - 1;
            EnableWeapon(_activeWeapon);
            activeWeapon.OnStatusChanged += UpdateUI;
            UpdateUI();
        }
    }

    private void Start()
    {
        activeWeaponSlot = activeWeaponSlot;
    }

    private void UpdateUI()
    {
        WeaponInfoChanged?.Invoke(activeWeapon.Name, activeWeapon.AmmoInfo);
    }

    public void Shoot() => activeWeapon.Shoot();
    public void Reload() => activeWeapon.Reload();
    private void EnableWeapon(int slot) => weapons[slot].Select(slot);
    private void DisableWeapon(int slot) => weapons[slot].Deselect(slot);
}
