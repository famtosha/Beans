using UnityEngine;

public class AnotherPlayerWeapon : MonoBehaviour, IAnotherPlayerWeapon
{
    [SerializeField] protected WeaponVisualBehaviour weaponUI;

    public void ShowReaload()
    {
        weaponUI.ShowReload();
    }

    public void ShowShoot(Vector3[] hits)
    {
        foreach (var hit in hits)
        {
            weaponUI.ShowHit(hit);
        }
    }
}
