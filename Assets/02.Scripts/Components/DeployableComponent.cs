using ProjectD_and_R.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeployableComponent : MonoBehaviour, IDeployable
{
    private ICharacterStatus _characterStatus;

    public event Action OnDeployed;
    public event Action OnUnDeployed;

    public void Deploy(Vector3 position, Quaternion rotation)
    {
        if (_characterStatus.IsDeployed) return;

        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);

#if UNITY_EDITOR
        Debug.Log($"{gameObject.name}:{gameObject.GetInstanceID()}유닛 {position}배치");
#endif
        OnDeployed?.Invoke();
    }

    public void Deploy(Vector2Int position, Quaternion rotation)
    {
        if (_characterStatus.IsDeployed) return;

        transform.position = GridManager.Instance.GridToWorldPos(position);
        transform.rotation = rotation;
        gameObject.SetActive(true);

#if UNITY_EDITOR
        Debug.Log($"{gameObject.name}:{gameObject.GetInstanceID()}유닛 {position}배치");
#endif
        OnDeployed?.Invoke();
    }

    public void Undeploy()
    {
        if(_characterStatus.IsDeployed) return;

        OnUnDeployed?.Invoke();

        gameObject.SetActive(false);
        _characterStatus.SetIsDeployed(false);
#if UNITY_EDITOR
        Debug.Log($"{gameObject.name}:{gameObject.GetInstanceID()}유닛 배치 해제");
#endif
    }


    public void Initialize(ICharacterCore characterCore)
    {
        if(characterCore == null) return;

        _characterStatus = characterCore.CharacterStatus;
    }

}
