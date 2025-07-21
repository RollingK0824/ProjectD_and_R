using ProjectD_and_R.Constants;
using System.Collections;
using Unity.Behavior;
using UnityEngine;

public class BlackboardManager : Singleton<BlackboardManager>
{
    [SerializeField] private BehaviorGraphAgent _agent;
    public BehaviorGraphAgent Agnet => _agent;

    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.OnTurnStateChanged += TurnStateChangedHandle;
    }

    private void Start()
    {
        //GameManager.Instance.OnTurnStateChanged += TurnStateChangedHandle;
    }

    public void TurnStateChangedHandle(ProjectD_and_R.Enums.TurnState turnState)
    {
#if UNITY_EDITOR
        Debug.Log($"Input Turn State : {turnState} / StringConstatnts : {StringConstants.BB_TurnState}");
#endif
        _agent.SetVariableValue(StringConstants.BB_TurnState, turnState);


#if UNITY_EDITOR
        Debug.Log($"{this.name} / Current Turn : {_agent.GetVariable(StringConstants.BB_TurnState, out BlackboardVariable<ProjectD_and_R.Enums.TurnState> temp)},{temp.Value}");
#endif

    }
}
