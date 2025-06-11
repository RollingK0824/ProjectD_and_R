using System;
using UnityEngine;

public interface IEnemyAi
{
    void Initialize(ICharacterCore character);
    void OnStatusChanged(string valueName, float oldValue, float newValue);
    void OnStatusChanged<T>(string valueName, T oldValue, T newValue);
    T GetVariable<T>(string name);
    void ActionRequest(IActionRequest request);

    event Action<IActionRequest> OnActionRequest;
}
