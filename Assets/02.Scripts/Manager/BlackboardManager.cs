using Unity.Behavior;
using UnityEngine;

public class BlackboardManager : Singleton<BlackboardManager>
{
    [SerializeField] private BehaviorGraphAgent _agent;
    public BehaviorGraphAgent Agnet => _agent;

    private void Start()
    {
        _agent.SetVariableValue("EnemyTurnState", ProjectD_and_R.Enums.TurnState.DungeonTurn);
    }
}
