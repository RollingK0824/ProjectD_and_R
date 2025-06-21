using Unity.Behavior;
using UnityEngine;

public class BBTest : MonoBehaviour
{
    public BehaviorGraphAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.SetVariableValue("EnemyTurnState", EnemyTurnState.Defense);
        agent.SetVariableValue("IsDeployed", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
