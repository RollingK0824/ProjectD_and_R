using ProjectD_and_R.Enums;
using UnityEngine;

public class GridObjectComponent : MonoBehaviour, IGridObject
{
    public Vector2Int CurrentGridPos => _currentGridPos;
    [SerializeField]
    private Vector2Int _currentGridPos;
    private Vector2Int _lastKnownGridPos;

    public ObjectType ObjectType => _objectType;
    [SerializeField]
    private ObjectType _objectType;

    private ICharacterCore _characterCore;

    public GameObject GameObject => gameObject;

    public void Initialize(ICharacterCore characterCore)
    {
        if(GridManager.Instance == null || characterCore == null)
        {
            return;
        }

        _characterCore = characterCore;
        _objectType = characterCore.CharacterStatus.ObjectType;

        _currentGridPos = GridManager.Instance.WorldToGridPos(transform.position);
        _lastKnownGridPos = _currentGridPos;

        GridManager.Instance.RegisterObject(this, CurrentGridPos);
    }

    private void Update()
    {
        UpdateGridPosition();
    }

    public void UpdateGridPosition()
    {
        if (GridManager.Instance == null) return;

        Vector2Int newGridPos = GridManager.Instance.WorldToGridPos(transform.position);

        if (newGridPos != _lastKnownGridPos)
        {
            GridManager.Instance.MoveObject(this, _lastKnownGridPos, newGridPos);
            _currentGridPos = newGridPos;
            _lastKnownGridPos= newGridPos;
        }
    }
}
