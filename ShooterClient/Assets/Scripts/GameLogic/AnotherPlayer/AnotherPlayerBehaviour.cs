using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AnotherPlayerBehaviour : MonoBehaviour
{
    public int id;
    public float sendCD = 0.05f;

    public Transform playerBodyPosition;
    public Transform playerBodyRotation;
    public TextMeshPro playerName;
    public AnotherPlayerWeaponList guns;
    public GameObject damageSign;
    public MeshRenderer mesh;

    private NetworkObjectInterpolation _interpolation = new NetworkObjectInterpolation(0.1f);

    public void Shoot(Vector3[] hits)
    {
        guns.Shoot(hits);
    }

    public void Reload()
    {
        guns.Reload();
    }

    public void ChangeWeapon(int weaponID)
    {
        guns.ChangeActiveWeapon(weaponID);
    }

    public void Move(Vector3 newPosition, Quaternion newRotation)
    {
        _interpolation.UpdatePosition(newPosition, newRotation);
    }

    public void ChangeName(string name)
    {
        playerName.text = name;
    }

    public void Update()
    {
        _interpolation.UpdateT();
        playerBodyPosition.transform.position = _interpolation.GetCurrentPosition();
        playerBodyRotation.transform.rotation = _interpolation.GetCurrentRotation();
    }

    public void ShowDamage(int damage)
    {
        var s = Instantiate(damageSign, transform.position, Quaternion.identity);
        s.GetComponentInChildren<Sign>().Draw(damage.ToString(), 1);
    }

    public void DisableVisual()
    {
        mesh.enabled = false;
    }

    public void EnableVisual()
    {
        mesh.enabled = true;
    }
}
