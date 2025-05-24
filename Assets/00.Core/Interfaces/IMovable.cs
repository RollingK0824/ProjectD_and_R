// Assets/00.Core/Interfaces/IMovable.cs
using UnityEngine;

public interface IMovable
{
    enum MoveType
    {
        None = 0,
        Ground = 1 << 0,
        Water = 1 << 1,
        Air = 1 << 2,
        Wall = 1 << 3,
        All = Ground | Water | Air | Wall
    }
    /// <summary>
    /// 특정 위치로 이동 함수
    /// </summary>
    /// <param name="targetPosition">타겟 좌표</param>
    void Move(Vector3 targetPosition);

    /// <summary>
    /// 이동 중지 함수
    /// </summary>
    void StopMoving();

    bool bIsMoving { get; } // 현재 이동 중인지 여부
    float CurrentMoveSpeed { get; } // 현재 이동 속도

    MoveType MovableTerrainTypes { get; }   // 이동 가능 지형 타입
}

