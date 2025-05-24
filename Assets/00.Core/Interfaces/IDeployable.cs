// Assets/00.Core/Interfaces/IDeployable.cs
using UnityEngine;

public interface IDeployable
{
    /// <summary>
    /// 유닛 배치 함수
    /// </summary>
    /// <param name="position">배치 좌표</param>
    /// <param name="rotation">배치 각도</param>
    void Deploy(Vector3 position, Quaternion rotation);
    void Undeploy();


    bool bIsDeployed { get; }    // 배치중인지 여부
}
