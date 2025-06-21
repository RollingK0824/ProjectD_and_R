using Unity.Behavior;
using UnityEngine;

public class BlackboardManager : Singleton<BlackboardManager>
{
    [SerializeField] private BehaviorGraphAgent _agent;
    public BehaviorGraphAgent Agnet => _agent;
}
