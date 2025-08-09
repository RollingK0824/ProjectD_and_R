using System;
using Unity.Behavior;

[BlackboardEnum]
public enum EnemyTurnState
{
    Defense,
    Dungeon
}

[BlackboardEnum]
public enum UnitType
{
    Default,
    Goblin,
    Orc,
    Spider,
    Human,
}
