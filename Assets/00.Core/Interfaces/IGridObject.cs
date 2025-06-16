using ProjectD_and_R.Enums;
using UnityEngine;

public interface IGridObject
{
    Vector2Int CurrentGridPos { get; }
    ObjectType ObjectType { get; }

    void UpdateGridPosition();

    GameObject GatGameObject();

    void Initialize(ICharacterCore characterCore);
}
