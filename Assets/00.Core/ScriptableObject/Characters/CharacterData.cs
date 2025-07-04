// Assets/00.Core/ScriptableObjects/Characters/CharacterData.cs
using UnityEngine;
using ProjectD_and_R.Enums;

public abstract class CharacterData : ScriptableObject
{
    /// <summary>
    /// 캐릭터 이름
    /// </summary>
    public string CharacterName => _characterName;
    [Header("Basic Info")][SerializeField] private string _characterName = "New Character";

    public CharacterTypeData CharacterType => _characterTypeData;
    [SerializeField] private CharacterTypeData _characterTypeData;

    /// <summary>
    /// 캐릭터 아이콘
    /// </summary>
    public Sprite CharacterIcon => _characterIcon;
    [SerializeField] private Sprite _characterIcon;

    /// <summary>
    /// 캐릭터 설명
    /// </summary>
    public string Description => _description;
    [SerializeField] private string _description = "";

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float MaxHealth => _maxHealth;
    [Header("Status")][SerializeField] private float _maxHealth = 100f;

    /// <summary>
    /// 방어력(물리)
    /// </summary>
    public float PhysicalDefense => _physicalDefense;
    [SerializeField] private float _physicalDefense = 10f;

    /// <summary>
    /// 마법 저항력
    /// </summary>
    public float MagicalResistance => _magicalResistance;
    [SerializeField] private float _magicalResistance = 10f;

    /// <summary>
    /// 공격력
    /// </summary>
    public float AttackDamage => _attackDamage;
    [SerializeField] private float _attackDamage = 10f;

    /// <summary>
    /// 공격 속도
    /// </summary>
    public float AttackSpeed => _attackSpeed;
    [SerializeField] private float _attackSpeed = 0.3f;

    /// <summary>
    /// 공격 사거리
    /// </summary>
    public float AttackRange => _attackedRange;
    [SerializeField] private float _attackedRange;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float MoveSpeed => _moveSpeed;
    [SerializeField] private float _moveSpeed = 5f;

    /// <summary>
    /// 캐릭터 프리팹
    /// </summary>
    public GameObject CharacterPrefab => _characterPrefab;
    [Header("Visuals")][SerializeField] private GameObject _characterPrefab;

    /// <summary>
    /// 이동 가능 지형 타입
    /// </summary>
    public MoveType MovableTerrainTypes => _movableTerrainTypes;
    [SerializeField] private MoveType _movableTerrainTypes;

    /// <summary>
    /// 진영 정보 None, Player, Enemy
    /// </summary>
    public Faction Faction => _faction;
    [SerializeField] private Faction _faction;

    /// <summary>
    /// GridPlace 정보
    /// </summary>
    public ObjectType ObjectType => _objectType;
    [SerializeField] private ObjectType _objectType;

}