using System;
using UnityEngine;

public interface IEnemyAi
{
    void Initialize(ICharacterCore character);
    void StatusChanged(string valueName, float oldValue, float newValue);
    void StatusChanged<T>(string valueName, T oldValue, T newValue);
    T GetVariable<T>(string name);
    void ActionRequest(IActionRequest request);

    event Action<IActionRequest> OnActionRequest;
}
