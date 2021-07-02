using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

[SelectionBase]
public class Pig : MonoBehaviour
{
    [SerializeField] private float _minDestinationDistance;
    [SerializeField] private float _speed;
    [SerializeField] private Timer _moveCD;
    [SerializeField] private float _maxMoveDistance;

    private NavMeshAgent _pathFinder;
    private DynamicLevelObject _levelObject;
    private Queue<Vector3> _pathQueue = new Queue<Vector3>();

    private void Start()
    {
        _pathFinder = GetComponent<NavMeshAgent>();
        _levelObject = GetComponent<DynamicLevelObject>();
    }

    private void Update()
    {
        MoveUpdate();
    }

    private bool TryGetCurrentDistanation(out Vector3 position)
    {
        position = Vector3.zero;
        if (_pathQueue.Count > 0)
        {
            position = _pathQueue.Peek();
            return true;
        }
        return false;
    }

    private bool UpdatePath(Vector3 destination)
    {
        bool result = true;
        _pathQueue.Enqueue(destination);
        return result;
    }

    private void RemoveCurrentDestanation()
    {
        _pathQueue.Dequeue();
    }

    private void Move(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
        transform.forward = direction;
        var temp = transform.rotation.eulerAngles;
        temp.x = 0;
        temp.y -= 90;
        transform.rotation = Quaternion.Euler(temp);
    }

    private bool IsCurrentDistanationRiched(Vector3 destanation)
    {
        float distance = Vector3.Distance(transform.position, destanation);
        bool result = distance < _minDestinationDistance;
        return result;
    }

    //use State machine
    private void MoveUpdate()
    {
        if (TryGetCurrentDistanation(out var destination))
        {
            Move(destination);
            if (IsCurrentDistanationRiched(destination))
            {
                RemoveCurrentDestanation();
            }
        }
        else
        {
            UpdatePath(GetNewDestanation());
        }
    }

    private Vector3 GetNewDestanation()
    {
        var randomVector = MathHelper.RandomVector2(_maxMoveDistance);
        var point = new Vector3(randomVector.x, transform.position.y, randomVector.y);
        if (Physics.Raycast(point, -Vector3.up, out RaycastHit hit, 10))
        {
            return hit.point;
        }
        return point;
    }

    private void OnDrawGizmos()
    {
        if (TryGetCurrentDistanation(out var position))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(position, 0.1f);
            Gizmos.color = Color.yellow;
            foreach (var pos in _pathQueue)
            {
                Gizmos.DrawSphere(pos, 0.1f);
            }
        }
    }
}
