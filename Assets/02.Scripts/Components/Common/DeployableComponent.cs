using ProjectD_and_R.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeployableComponent : MonoBehaviour, IDeployable
{
    public bool bIsDeployed { get; private set; } = false;

    public event Action OnDeployed;
    public event Action OnUnDeployed;

    public void Initialize()
    {
    }
    
    public void Deploy(Vector3 position, Quaternion rotation)
    {
        if (bIsDeployed) return;

        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);

        bIsDeployed = true;
#if UNITY_EDITOR
        Debug.Log($"{gameObject.name}:{gameObject.GetInstanceID()}유닛 {position}배치");
#endif
        OnDeployed?.Invoke();
    }

    public void Undeploy()
    {
        if(!bIsDeployed) return;

        gameObject.SetActive(false);
        bIsDeployed = false;
#if UNITY_EDITOR
        Debug.Log($"{gameObject.name}:{gameObject.GetInstanceID()}유닛 배치 해제");
#endif
        OnUnDeployed?.Invoke();
    }


    public void Initialize(CharacterCore characterCore)
    {
    }

}
