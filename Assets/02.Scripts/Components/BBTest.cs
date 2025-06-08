using Unity.Behavior;
using UnityEngine;

public class BBTest : MonoBehaviour
{
    public BehaviorGraphAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.SetVariableValue("New EnemyTurnState", EnemyTurnState.Dungeon);
        agent.SetVariableValue("Test", EnemyTurnState.Dungeon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
