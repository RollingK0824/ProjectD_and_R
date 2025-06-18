using ProjectD_and_R.Enums;
using UnityEngine;

public interface IGridObject
{
    Vector2Int CurrentGridPos { get; }
    ObjectType ObjectType { get; }
    GameObject GameObject { get; }

    void UpdateGridPosition();

    void Initialize(ICharacterCore characterCore);
}
