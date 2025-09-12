using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public abstract class TurnComponent : MonoBehaviour, ITurnComponent
{
    protected ICharacterCore _characterCore;

    protected bool _isActionFinished = false;

    public bool IsMyTurn { get; private set; }

    public void Initialize(ICharacterCore characterCore)
    {
        _characterCore = characterCore;
    }

    public abstract IEnumerator StartTurn();

    public virtual void EndTurn()
    {
        IsMyTurn = false;
    }

    public void NotifyActionFinished()
    {
#if UNITY_EDITOR
        Debug.Log($"[{this.name}] / Action Finished");
#endif
        _isActionFinished = true;
    }
}
