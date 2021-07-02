using System;

public interface IWeapon
{
    void Shoot();
    void Reload();
    void Select(int ID);
    void Deselect(int ID);
    string Name { get; }
    string AmmoInfo { get; }
    event Action OnStatusChanged;
}
