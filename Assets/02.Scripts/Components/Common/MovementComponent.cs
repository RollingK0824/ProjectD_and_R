using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectD_and_R.Enums;

public class MovementComponent : MonoBehaviour, IMovable
{
    private CharacterCore _characterCore;

    public bool bIsMoving { get; private set; } = false;

    private UnityEngine.AI.NavMeshAgent _navMeshAgent;

    public void Initialize(CharacterCore characterCore)
    {
        if (characterCore == null) return;
        _characterCore = characterCore;
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if( _navMeshAgent == null)
        {
#if UNITY_EDITOR
            Debug.Log($"MovementComponent requires a NavMeshAgent Component");
#endif
        }
        _navMeshAgent.speed = _characterCore.Status.MoveSpeed;
        _navMeshAgent.isStopped = true;
#if UNITY_EDITOR
        Debug.Log($"MovementComponent Initialized: Speed = {_characterCore.Status.MoveSpeed}, MoveTypes = {_characterCore.Status.MovableTerrainTypes} ");
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
        /* 테스트 코드 */
        _characterCore.Attack();
#endif
    }

    public void SetMoveSpeed(float newSpeed)
    {
        _characterCore.Status.SetMoveSpeed(newSpeed);
        if(_navMeshAgent != null)
        {
            _navMeshAgent.speed = _characterCore.Status.MoveSpeed;
        }
    }
}
