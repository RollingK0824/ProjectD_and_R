using UnityEngine;

public interface IEnemyAi
{
    void Initialize(ICharacterCore character);
    void OnUpdate();
    void ReceiveCommand(EnemyAiComponent command);
}
