using ProjectD_and_R;
using System;
using System.Threading;
using Unity.Behavior;
using UnityEngine;

public class EnemyAiComponent : MonoBehaviour, IEnemyAi
{
    private ICharacterCore _characterCore;

    [SerializeField] private BehaviorGraphAgent _behaviorAgent;

    public event Action<IActionRequest> OnActionRequest;

    void IEnemyAi.Initialize(ICharacterCore character)
    {
        _characterCore = character;

        if (_characterCore == null || _behaviorAgent == null) return;

        /* Do Nothing */
    }

    public void OnUpdate()
    {
    }

    public void OnStatusChanged(string valueName, float oldValue, float newValue)
    {
        if(_behaviorAgent == null) return;

        _behaviorAgent.SetVariableValue(valueName, newValue);
    }

    public void OnStatusChanged<T>(string valueName, T oldValue, T newValue)
    {
        if (_behaviorAgent == null) return;

        _behaviorAgent.SetVariableValue<T>(valueName, newValue);
    }

    public void ActionRequest(IActionRequest request)
    {
        if (OnActionRequest == null || request == null) return;
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
