using UnityEngine;
using PacketManager;

public class Arms : MonoBehaviour
{
    public WeaponInventory guns;

    private void OnEnable() => FindObjectOfType<WeaponInventoryUI>()?.Bind(guns);
    private void OnDisable() => FindObjectOfType<WeaponInventoryUI>()?.UnBind(guns);

    private void Update()
    {
        if (Input.GetMouseButton(0)) Shoot();
        if (Input.mouseScrollDelta.y > 0) ChoseNextWeapon();
        if (Input.mouseScrollDelta.y < 0) ChosePrevWeapon();
        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    private void Shoot()
    {
        guns.Shoot();
    }

    private void ChoseNextWeapon()
    {
        guns.activeWeaponSlot++;
    }

    private void ChosePrevWeapon()
    {
        guns.activeWeaponSlot--;
    }

    private void Reload()
    {
        guns.Reload();
    }
}
