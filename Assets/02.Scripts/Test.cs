using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public NavMeshAgent agent;

    private void Start()
    {
        agent.isStopped = false;
        agent.SetDestination(new Vector3(100, 0, 100));
    }
}
