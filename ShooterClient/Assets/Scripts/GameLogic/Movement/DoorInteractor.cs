using UnityEngine;

public class DoorInteractor : MonoBehaviour
{
    [SerializeField] private float _interactionRange;
    [SerializeField] private RayProvider _rayProvider;

    private DoorInteractorUI _ui;

    private void Start()
    {
        _ui = FindObjectOfType<DoorInteractorUI>();
    }

    private void Update()
    {
        if (CanSeeDoor(out var door))
        {
            _ui.ShowInfo();
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.ChangeState();
            }
        }
        else
        {
            _ui.HideInfo();
        }
    }

    private bool CanSeeDoor(out Door door)
    {
        door = null;
        if (_rayProvider.MakeRay(transform.forward, _interactionRange, out var hit))
        {
            door = hit.collider.gameObject.GetComponentInParent<Door>();
        }
        return door != null;
    }
}
