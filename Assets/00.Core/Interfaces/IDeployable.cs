// Assets/00.Core/Interfaces/IDeployable.cs
using System;
using UnityEngine;

public interface IDeployable
{
    /// <summary>
    /// 유닛 배치 함수
    /// </summary>
    /// <param name="position">배치 좌표</param>
    /// <param name="rotation">배치 각도</param>
    void Deploy(Vector3 position, Quaternion rotation);

    /// <summary>
    /// 배치 해제 함수
    /// </summary>
    void Undeploy();

    /// <summary>
    /// 배치 중인지 여부
    /// </summary>
    bool bIsDeployed { get; }

    /// <summary>
    /// 유닛 배치 시 호출 이벤트
    /// </summary>
    event Action OnDeployed;

    /// <summary>
    /// 유닛 배치 해제 시 호출 이벤트
    /// </summary>
    event Action OnUnDeployed;

    void Initialize(CharacterCore characterCore);

}
