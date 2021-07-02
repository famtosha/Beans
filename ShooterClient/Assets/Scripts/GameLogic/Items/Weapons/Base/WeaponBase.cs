using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    public virtual string Name { get; }
    public virtual string AmmoInfo { get; }
    public event Action OnStatusChanged;

    protected void UpdateStatus() => OnStatusChanged?.Invoke();

    public virtual void Deselect(int ID) { }
    public virtual void Select(int ID) { }

    public abstract void Reload();

    public abstract void Shoot();
}