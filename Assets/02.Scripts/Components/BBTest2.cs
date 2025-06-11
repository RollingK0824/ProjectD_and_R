using Unity.Behavior;
using UnityEngine;

public class BBTest2 : MonoBehaviour
{
    public BehaviorGraphAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{agent.GetVariable<EnemyTurnState>("EnemyTurnState", out var temp)}{temp.Value}");
    }
}
