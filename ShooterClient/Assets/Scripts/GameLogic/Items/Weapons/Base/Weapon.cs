using UnityEngine;

public abstract class Weapon<T> : WeaponBase where T : WeaponData
{
    [SerializeField] protected T weaponData;
    [SerializeField] protected WeaponVisualBehaviour weaponUI;
    [SerializeField] protected RayProvider rayProvider;
    [SerializeField] protected WeaponServerRequiester netRequest;
    protected Timer shootCD;

    public override string Name => weaponData.weaponName;
    public override string AmmoInfo => $"{weaponData.ammoCurrent}/{weaponData.ammoMax}";

    private void Awake()
    {
        weaponData = Instantiate(weaponData);
        shootCD = new Timer(weaponData.shootRate);
    }

    protected virtual void Update()
    {
        shootCD.UpdateTimer(Time.deltaTime);
    }

    public override void Reload()
    {
        weaponData.ammoCurrent = weaponData.ammoMax;
        netRequest?.Reload();
        UpdateStatus();
    }

    public override void Shoot() { }

    public override void Select(int slot)
    {
        base.Select(slot);
        gameObject.SetActive(true);
        netRequest?.OnChangeWeapon(slot); //why where?
    }

    public override void Deselect(int slot)
    {
        base.Deselect(slot);
        gameObject.SetActive(false);
    }
}
