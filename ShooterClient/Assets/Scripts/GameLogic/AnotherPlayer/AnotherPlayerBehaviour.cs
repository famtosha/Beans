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

    private Vector3 lastPlayerPosition;
    private Vector3 nextPlayerPosition;

    private Quaternion lastPlayerRoatation;
    private Quaternion nextPlayerRotation;

    private float t = 0;

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

    public void ForceMove(Vector3 newPostion, Quaternion newRotaion)
    {
        playerBodyPosition.transform.position = newPostion;
        playerBodyRotation.transform.rotation = newRotaion;

        lastPlayerPosition = newPostion;
        nextPlayerPosition = newPostion;

        lastPlayerRoatation = newRotaion;
        nextPlayerRotation = newRotaion;
    }

    public void Move(Vector3 newPosition, Quaternion newRotation)
    {
        playerBodyPosition.transform.position = newPosition;
        playerBodyRotation.transform.rotation = newRotation;

        lastPlayerPosition = nextPlayerPosition;
        nextPlayerPosition = newPosition;

        lastPlayerRoatation = nextPlayerRotation;
        nextPlayerRotation = newRotation;

        t = 0;
    }

    public void ChangeName(string name)
    {
        playerName.text = name;
    }

    public void Update()
    {
        t += Time.deltaTime;
        playerBodyPosition.transform.position = Vector3.Lerp(lastPlayerPosition, nextPlayerPosition, t / sendCD);
        playerBodyRotation.transform.rotation = Quaternion.Slerp(lastPlayerRoatation, nextPlayerRotation, t / sendCD);
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
