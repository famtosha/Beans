using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CubeCreator : Weapon<WeaponData>
{
    [SerializeField] private SynchronizedObjectsFacotry _soFactory;
    [SerializeField] private Material _projectionMaterial;

    private GameObject _objectProjection;
    private Transform _buildOrigin;

    private int _selectedAssetID;
    public int selctedAsset
    {
        get => _selectedAssetID;
        set
        {
            DestroyProjection();
            _selectedAssetID = Mathf.Clamp(value, 0, _soFactory.assetsCount - 1);
            CreateProjection(_selectedAssetID);
        }
    }

    public override string Name => _objectProjection?.name;

    private void Start()
    {
        selctedAsset = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.M)) selctedAsset++;
        if (Input.GetKeyDown(KeyCode.N)) selctedAsset--;
        Vector3 hitPoint;
        if (rayProvider.MakeRay(transform.forward, weaponData.range, out var hit))
        {
            hitPoint = hit.point;
            _objectProjection.SetActive(true);

            _objectProjection.transform.position = GetBuildOriginPosition(hit.point);

            var tempRotation = _objectProjection.transform.rotation;
            var newRotation = Quaternion.Euler(tempRotation.eulerAngles.x, transform.eulerAngles.y + 90, tempRotation.eulerAngles.z);
            _objectProjection.transform.rotation = newRotation;
        }
        else
        {
            _objectProjection.SetActive(false);
        }
    }

    public override void Deselect(int slot)
    {
        base.Deselect(slot);
        _objectProjection.SetActive(false);
    }

    public override void Shoot()
    {
        if (shootCD.isReady)
        {
            if (rayProvider.MakeRay(transform.forward, weaponData.range, out var hit))
            {
                Vector3 hitPoint = GetBuildOriginPosition(hit.point);
                netRequest.SpawnObject(_selectedAssetID, hitPoint, _objectProjection.transform.localScale, _objectProjection.transform.rotation);
                netRequest.ShotShoot(hitPoint);
                weaponUI.ShowHit(hitPoint);
            }
            weaponData.ammoCurrent--;
            UpdateStatus();
            shootCD.Reset();
        }
    }

    private Vector3 GetBuildOriginPosition(Vector3 buildPosition)
    {
        return buildPosition + (_objectProjection.transform.position - _buildOrigin.position);
    }

    private void DestroyProjection()
    {
        Destroy(_objectProjection);
        _objectProjection = null;
    }

    private void CreateProjection(int assetID)
    {
        _objectProjection = Instantiate(_soFactory.assets[assetID]).gameObject;
        PrepareSpawnableObject(_objectProjection);

        void PrepareSpawnableObject(GameObject gameObject)
        {
            _buildOrigin = _objectProjection.GetComponentInChildren<ObjectBuildingOrigin>().transform;

            foreach (var mat in _objectProjection.GetComponentsInChildren<MeshRenderer>())
            {
                mat.material = _projectionMaterial;
            }
            if (_objectProjection.TryGetComponent<NetworkObjectDestoryAction>(out var p)) Destroy(p);
            var components = _objectProjection.GetComponentsInChildren<Component>().ToList();
            components = components.Where(x => !(x is MeshFilter || x is MeshRenderer || x is Transform)).ToList();
            components.ForEach(x => Destroy(x));
        }
    }
}
