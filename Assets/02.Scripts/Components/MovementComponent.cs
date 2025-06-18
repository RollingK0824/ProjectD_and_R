using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectD_and_R.Enums;

public class MovementComponent : MonoBehaviour, IMovable
{
    private ICharacterStatus _status;

    public bool bIsMoving { get; private set; } = false;

    private UnityEngine.AI.NavMeshAgent _navMeshAgent;

    public void Initialize(ICharacterCore characterCore)
    {
        if (characterCore == null) return;
        _status = characterCore.CharacterStatus;
        _navMeshAgent = characterCore.NavMeshAgent;
        if( _navMeshAgent == null)
        {
#if UNITY_EDITOR
            Debug.Log($"MovementComponent requires a NavMeshAgent Component");
#endif
        }
        SetMoveSpeed(_status.MoveSpeed);
        _navMeshAgent.isStopped = true;
#if UNITY_EDITOR
        Debug.Log($"MovementComponent Initialized: Speed = {_status.MoveSpeed}, MoveTypes = {_status.MovableTerrainTypes} ");
#endif
    }

    void Update()
    {
        if (bIsMoving && !_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    StopMoving();
                }
            }
        }
    }

    public void Move(Vector3 targetPosition)
    {
        if (_navMeshAgent == null || !gameObject.activeSelf) return;
        _navMeshAgent.SetDestination(targetPosition);
        _navMeshAgent.isStopped = false;
        bIsMoving = true;
#if UNITY_EDITOR
        Debug.Log($"{gameObject.name} starts moving to {targetPosition}");
#endif
    }

    public void StopMoving()
    {
        if (_navMeshAgent != null)
        {
            _navMeshAgent.isStopped = true;
        }
        bIsMoving = false;
#if UNITY_EDITOR
        Debug.Log($"{gameObject.name} stops moving.");
#endif
    }

    public void SetMoveSpeed(float newSpeed)
    {
        _status.SetMoveSpeed(newSpeed);
        if(_navMeshAgent != null)
        {
            _navMeshAgent.speed = _status.MoveSpeed;
        }
    }
}
