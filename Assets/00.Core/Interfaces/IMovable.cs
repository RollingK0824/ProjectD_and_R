// Assets/00.Core/Interfaces/IMovable.cs
using UnityEngine;

public interface IMovable
{

    /// <summary>
    /// 특정 위치로 이동 함수
    /// </summary>
    /// <param name="targetPosition">타겟 좌표</param>
    void Move(Vector3 targetPosition);

    /// <summary>
    /// 이동 중지 함수
    /// </summary>
    void StopMoving();

    /// <summary>
    /// 현재 이동 중인지 여부
    /// </summary>
    bool bIsMoving { get; }

    /// <summary>
    /// 초기 스탯 초기화 함수
    /// </summary>
    /// <param name="characterCore">캐릭터 코어</param>
    void Initialize(CharacterCore characterCore);
}

