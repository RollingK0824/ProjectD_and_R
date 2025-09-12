using ProjectD_and_R;
using System;
using System.Threading;
using Unity.Behavior;
using UnityEngine;

public class EnemyAiComponent : MonoBehaviour, IEnemyAi
{
    private ICharacterCore _characterCore;

    private BehaviorGraphAgent _behaviorAgent;

    public event Action<IActionRequest> OnActionRequest;

    void IEnemyAi.Initialize(ICharacterCore character)
    {
        _characterCore = character;
        _behaviorAgent = character.BehaviorGraphAgent;
        if (_characterCore == null || _behaviorAgent == null) return;

        /* Do Nothing */
    }

    public void OnUpdate()
    {
    }

    public void StatusChanged(string valueName, float oldValue, float newValue)
    {
        if(_behaviorAgent == null) return;

        _behaviorAgent.SetVariableValue(valueName, newValue);
    }

    public void StatusChanged<T>(string valueName, T oldValue, T newValue)
    {
        if (_behaviorAgent == null) return;

        _behaviorAgent.SetVariableValue<T>(valueName, newValue);
    }

    public void ActionRequest(IActionRequest request)
    {
        if (request == null) return;
        OnActionRequest?.Invoke(request);
    }

    public T GetVariable<T>(string name)
    {
        if (_behaviorAgent == null) throw new NotImplementedException();

        _behaviorAgent.GetVariable<T>(name, out var temp);
        if (temp != null) return temp;
        else throw new NotImplementedException();
    }
}
