using System.Collections;

public interface ITurnComponent
{
    bool IsMyTurn { get; }
    IEnumerator StartTurn();
    void EndTurn();

    void Initialize(ICharacterCore characterCore);
    void NotifyActionFinished();
}