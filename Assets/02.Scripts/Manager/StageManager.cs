// Assets/Scripts/Managers/StageManager.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Unity.Behavior;
using ProjectD_and_R.Enums;
using ProjectD_and_R.Constants;
using System.Linq;

public class StageManager : Singleton<StageManager>
{
    [SerializeField]
    private StageData _currentStageInfo;
    private ProjectD_and_R.Enums.TurnState _currentTurnState;
    private GameState _currentGameState;
    private Coroutine _spawnCoroutine;

    [SerializeField] private BehaviorGraphAgent _agent;
    private UnitFactory _unitFactory;

    public List<string> BB_variableKeys = new List<string>();

    private int _defenseObjectCount;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Initialize(UnitFactory unitFactory)
    {
        _unitFactory = unitFactory;

        InitializeBlackboard();
    }

    private void OnEnable()
    {
        // GameManager의 상태 변경 이벤트에 구독
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
            GameManager.Instance.OnTurnStateChanged += HandleTurnStateChanged;
        }
    }

    private void OnDisable()
    {
        // GameManager의 상태 변경 이벤트에서 구독 해제
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
            GameManager.Instance.OnTurnStateChanged -= HandleTurnStateChanged;
        }
    }

    private void InitializeBlackboard()
    {
        BB_variableKeys.Add(StringConstants.BB_EnemyKillCount);
        BB_variableKeys.Add(StringConstants.BB_MaxEnemyCount);
        BB_variableKeys.Add(StringConstants.BB_PlayerDeathCount);
        BB_variableKeys.Add(StringConstants.BB_MaxPlayerCount);

        BB_variableKeys.Add(StringConstants.BB_IsAllEnemiesDefeated);
        BB_variableKeys.Add(StringConstants.BB_IsTimeLimitReached_Clear);
        BB_variableKeys.Add(StringConstants.BB_IsBossDefeated);

        BB_variableKeys.Add(StringConstants.BB_IsAllPlayerUnitsDefeated);
        BB_variableKeys.Add(StringConstants.BB_IsTimeLimitReached_GameOver);
        BB_variableKeys.Add(StringConstants.BB_IsDefenseTargetDestroyed);
    }

    private void ResetBlackboard()
    {
        foreach (var key in BB_variableKeys)
        {
            BlackboardVariable temp = GetVariableValue(key);

            if (temp == null || temp.ObjectValue == null)
            {
#if UNITY_EDITOR
                Debug.Log($"{this.name} / BlackboardVariable Is NULL");
                continue;
#endif
            }

            switch (temp.ObjectValue)
            {
                case int intValue:
                    _agent.SetVariableValue<int>(key, 0);
                    break;
                case float floatValue:
                    _agent.SetVariableValue<float>(key, 0f);
                    break;
                case bool boolValue:
                    _agent.SetVariableValue<bool>(key, false);
                    break;
                default:
                    break;
            }
        }
    }

    private void ResetBlackboardForStage(StageData stageData)
    {
        int count = 0;

        foreach (var entry in stageData.spawnSequence)
        {
            count += entry.count;
        }

        SetVariableValue(StringConstants.BB_MaxEnemyCount, count);

        // Player Count는 이후 파티 시스템에서 파티 정보를 받아서 처리

        foreach (var condition in stageData.stageConditionData.conditions)
        {
            switch (condition.type)
            {
                case GameEndConditionType.AllEnemiesDefeated:
                    SetVariableValue(StringConstants.BB_IsAllEnemiesDefeated, true);
                    break;

                case GameEndConditionType.TimeLimitReached_Clear:
                    SetVariableValue(StringConstants.BB_IsTimeLimitReached_Clear, true);
                    break;

                case GameEndConditionType.BossDefeated:
                    SetVariableValue(StringConstants.BB_IsBossDefeated, true);
                    break;

                case GameEndConditionType.AllPlayerUnitsDefeated:
                    SetVariableValue(StringConstants.BB_IsAllPlayerUnitsDefeated, true);
                    break;

                case GameEndConditionType.DefenseTargetDestroyed:
                    SetVariableValue(StringConstants.BB_IsDefenseTargetDestroyed, true);
                    break;

                case GameEndConditionType.TimeLimitReached_GameOver:
                    SetVariableValue(StringConstants.BB_IsTimeLimitReached_GameOver, true);
                    break;

                default:
                    break;
            }
        }
    }

    public GameObject defenseObjectPrefab;

    // 실제 스테이지 시작 로직
    private void StartStage(StageData stageData)
    {
#if UNITY_EDITOR
        // 테스트 코드 (DefenseObject 배치)
        Instantiate(defenseObjectPrefab).GetComponent<CharacterCore>().DeployableComponent.Deploy(new Vector2Int(3, 3), Quaternion.identity);
#endif 

        if (stageData == null)
        {
#if UNITY_EDITOR
            Debug.LogError("StageManager: 시작할 StageInfo가 null");
#endif
            return;
        }

#if UNITY_EDITOR
        Debug.Log($"StageManager: '{stageData.stageName}' 스테이지 시작");
#endif
        
        if (_currentTurnState == ProjectD_and_R.Enums.TurnState.DefenseTurn)
        {
            _spawnCoroutine = StartCoroutine(SpawnEnemiesRoutine(stageData));
        }
        else if (_currentTurnState == ProjectD_and_R.Enums.TurnState.DungeonBattleTurn)
        {
            BlackboardManager.Instance.Agnet.SetVariableValue("EnemyTurnState", ProjectD_and_R.Enums.TurnState.DungeonBattleTurn);
            TurnBattleManager.Instance.StartNewRound();
        }
    }

    public void StageClear()
    {
#if UNITY_EDITOR
        Debug.Log($"StageClear");
#endif
    }

    public void StageFailed()
    {
#if UNITY_EDITOR
        Debug.Log($"StageFailed");
#endif
    }

    public void RegisterStageUnit(ICharacterCore characterCore)
    {
        switch (characterCore.Data.ObjectType)
        {
            case ObjectType.None:
                break;
            case ObjectType.Player:
                break;
            case ObjectType.Enemy:
                break;
            case ObjectType.Boss:
                break;
            case ObjectType.Obstacle:
                break;
            case ObjectType.DefenseTarget:
                _defenseObjectCount++;
                SetVariableValue(StringConstants.BB_MaxDefenseObjectCount, _defenseObjectCount);
                break;
            default:
                break;
        }
    }

    // 적 스폰 코루틴
    private IEnumerator SpawnEnemiesRoutine(StageData stageInfo)
    {
#if UNITY_EDITOR
        //BlackboardManager.Instance.Agnet.SetVariableValue("EndPoint", stageInfo.endPoint);
        Debug.Log($"테스트 코드 에너미 endPoint 설정");
#endif
        yield return new WaitForSeconds(stageInfo.stageStartTimeOffset);

        foreach (var wave in stageInfo.spawnSequence)
        {
#if UNITY_EDITOR
            Debug.Log($"웨이브 시작: {wave.enemyType} {wave.count}마리");
#endif
            for (int i = 0; i < wave.count; i++)
            {
                if (wave.enemyType != null && wave.spawnPoint != null)
                {
                    //Instantiate(wave.enemyType, enemySpawnPoint.position, Quaternion.identity);
                    //_unitFactory.GetUnit();
                    ICharacterCore enemy = _unitFactory.GetEnemyUnit(wave.characterData);

                    enemy.DeployableComponent.Deploy(wave.spawnPoint,Quaternion.identity);

                    enemy.EnemyAiComponent.StatusChanged<Vector2Int>(StringConstants.BB_EndPoint,wave.spawnPoint,wave.endPoint);
#if UNITY_EDITOR
                    Debug.Log($"적 스폰: {wave.enemyType}");
#endif
                }
                yield return new WaitForSeconds(wave.spawnDelay);
            }

            if (wave.spawnDelay > 0)
            {
#if UNITY_EDITOR
                Debug.Log($"다음 웨이브까지 대기: {wave.spawnDelay}초");
#endif
                yield return new WaitForSeconds(wave.spawnDelay);
            }
        }
#if UNITY_EDITOR
        Debug.Log("모든 웨이브 스폰 완료.");
#endif
        // TODO: 모든 적이 죽었는지 확인하는 로직 등 추가 필요
        // 스테이지 완료 시 GameManager.Instance.EndStage() 호출
    }

    public void HandleCharacterDeath(ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.None:
                break;
            case ObjectType.Player:
                int currentPlayerDeathCount = GetVariableValue<int>(StringConstants.BB_PlayerDeathCount).Value;
                SetVariableValue(StringConstants.BB_PlayerDeathCount, currentPlayerDeathCount+1);
#if UNITY_EDITOR
                Debug.Log($"Player Death Count : {currentPlayerDeathCount + 1}");
#endif
                break;
            case ObjectType.Enemy:
                int currentEnemyKillCount = GetVariableValue<int>(StringConstants.BB_EnemyKillCount).Value;
                SetVariableValue(StringConstants.BB_EnemyKillCount, currentEnemyKillCount + 1);
#if UNITY_EDITOR
                Debug.Log($"Enemy Kill Count : {currentEnemyKillCount + 1}");
#endif
                break;
            case ObjectType.Boss:
                break;
            case ObjectType.Obstacle:
                break;
            case ObjectType.DefenseTarget:
                int destroyedDefenseObjectCount = GetVariableValue<int>(StringConstants.BB_DestroyedDefenseObjectCount).Value;
                SetVariableValue<int>(StringConstants.BB_DestroyedDefenseObjectCount, destroyedDefenseObjectCount + 1);
#if UNITY_EDITOR
                Debug.Log($"[{this.name}] / Destroyed DefenseObject");
#endif
                break;
            default:
                break;
        }
    }

    private void HandleGameStateChanged(GameState newState)
    {
        if (_currentTurnState != ProjectD_and_R.Enums.TurnState.DefenseTurn && _currentTurnState != ProjectD_and_R.Enums.TurnState.DungeonBattleTurn)
        {
            return;
        }

        switch (newState)
        {
            case GameState.SceneStarting:
                // TODO: UI 업데이트, 초기화 애니메이션 등
                ResetBlackboard();
                ResetBlackboardForStage(_currentStageInfo);
                StartStage(_currentStageInfo);
                break;
            case GameState.SceneInProgress:
#if UNITY_EDITOR
                Debug.Log("StageManager: 스테이지 진행 중. 적 스폰 시작.");
#endif
                if(_currentGameState != GameState.SceneInProgress)
                {
                    StartStage(_currentStageInfo);
                }
                break;
            case GameState.ScenePaused:
#if UNITY_EDITOR
                Debug.Log("StageManager: 스테이지 일시 중지.");
#endif
                if (_spawnCoroutine != null)
                {
                    StopCoroutine(_spawnCoroutine); // 스폰 코루틴 정지
                }
                break;
            case GameState.StageEnded:
#if UNITY_EDITOR
                Debug.Log("StageManager: 스테이지 종료.");
#endif
                if (_spawnCoroutine != null)
                {
                    StopCoroutine(_spawnCoroutine); // 스폰 코루틴 정지
                }
                // TODO: 남은 적 제거, 스테이지 결과 처리, UI 업데이트 등
                break;
            case GameState.MainMenu:
            case GameState.GameOver:
                // 해당 상태에 대한 StageManager 로직
                break;
        }
        _currentGameState = newState;
    }

    private void HandleTurnStateChanged(ProjectD_and_R.Enums.TurnState newState)
    {
        _currentTurnState = newState;
    }

    // 외부에서 스테이지 정보를 설정할 수 있도록 (GameManager가 호출)
    public void SetCurrentStage(StageData stageInfo)
    {
        _currentStageInfo = stageInfo;
    }

    // ----- Blackboard Getter Setter ----- //

    private bool SetVariableValue(string key, object value)
    {
        if (_agent.SetVariableValue(key, value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool SetVariableValue<T>(string key, T value)
    {
        if (_agent.SetVariableValue<T>(key, value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private BlackboardVariable GetVariableValue(string key)
    {
        if (_agent.GetVariable(key, out var value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }
    private BlackboardVariable<T> GetVariableValue<T>(string key)
    {
        if (_agent.GetVariable<T>(key, out var value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }
}