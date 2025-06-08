using ProjectD_and_R;
using System.Threading;
using Unity.Behavior;
using UnityEngine;

public class EnemyAiComponent : MonoBehaviour, IEnemyAi
{
    private ICharacterCore _characterCore;

    [SerializeField] private BehaviorGraphAgent _behaviorTreeAgent;

    void OnGraphStart()
    {

    }
    void IEnemyAi.Initialize(ICharacterCore character)
    {
        _characterCore = character;

        if (_characterCore == null || _behaviorTreeAgent == null)
        {
            return;
        }
        else
        {
#if UNITY_EDITOR

#endif
        }

    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveCommand(EnemyAiComponent command)
    {
        throw new System.NotImplementedException();
    }
}
