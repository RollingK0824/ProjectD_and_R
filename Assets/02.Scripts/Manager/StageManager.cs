// Assets/Scripts/Managers/StageManager.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using ProjectD_and_R.Enums;
using Unity.Behavior;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private StageData _currentStageInfo;
    private Coroutine _spawnCoroutine;

    [SerializeField] private BehaviorGraphAgent _agent;

    private void OnEnable()
    {
        // GameManager의 상태 변경 이벤트에 구독
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }
    }

    private void OnDisable()
    {
        // GameManager의 상태 변경 이벤트에서 구독 해제
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        }
    }

    private void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.StageStarting:
                // TODO: UI 업데이트, 초기화 애니메이션 등
                break;
            case GameState.StageInProgress:
#if UNITY_EDITOR
                Debug.Log("StageManager: 스테이지 진행 중. 적 스폰 시작.");
#endif
                StartStage(_currentStageInfo);
                break;
            case GameState.StagePaused:
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
    }

    // 외부에서 스테이지 정보를 설정할 수 있도록 (GameManager가 호출)
    public void SetCurrentStage(StageData stageInfo)
    {
        _currentStageInfo = stageInfo;
    }

    // 실제 스테이지 시작 로직
    private void StartStage(StageData stageInfo)
    {
        if (stageInfo == null)
        {
#if UNITY_EDITOR
            Debug.LogError("StageManager: 시작할 StageInfo가 null");
#endif
            return;
        }

#if UNITY_EDITOR
        Debug.Log($"StageManager: '{stageInfo.stageName}' 스테이지 시작");
#endif
        _spawnCoroutine = StartCoroutine(SpawnEnemiesRoutine(stageInfo));
    }

    // 적 스폰 코루틴
    private IEnumerator SpawnEnemiesRoutine(StageData stageInfo)
    {
#if UNITY_EDITOR
        BlackboardManager.Instance.Agnet.SetVariableValue("EndPoint", stageInfo.enemyEndPoints[0]);
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
                    GameObject enemy = ObjectPoolManager.Instance.Get("Assets/03.Prefabs/Characters/TestEnemy.prefab");
                    enemy.GetComponent<ICharacterCore>().DeployableComponent.Deploy(wave.spawnPoint,Quaternion.identity);
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
}