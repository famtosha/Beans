using UnityEngine;

public class AnotherPlayerWeaponList : MonoBehaviour
{
    public AnotherPlayerWeapon[] weapons;

    private int _activeWeapon = 0;
    public void ChangeActiveWeapon(int slot)
    {
        weapons[_activeWeapon].gameObject.SetActive(false);
        _activeWeapon = slot;
        weapons[_activeWeapon].gameObject.SetActive(true);
    }

    public void Shoot(Vector3[] hits)
    {
        weapons[_activeWeapon].ShowShoot(hits);
    }

    public void Reload()
    {
        weapons[_activeWeapon].ShowReaload();
    }
}
