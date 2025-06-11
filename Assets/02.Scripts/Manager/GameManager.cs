// Assets/Scripts/Managers/GameManager.cs
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using ProjectD_and_R.Enums;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameState _currentGameState = GameState.MainMenu;
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set
        {
            if (_currentGameState != value)
            {
                _currentGameState = value;
                OnGameStateChanged?.Invoke(_currentGameState); // 상태 변경 알림
#if UNITY_EDITOR
                Debug.Log($"게임 상태 변경: {_currentGameState}");
#endif
            }
        }
    }

    // 게임 상태 변경 이벤트
    public event Action<GameState> OnGameStateChanged;

    protected override void Awake()
    {
        base.Awake();
        ObjectPoolManager.Instance.Initialize(this.gameObject.transform);
        ObjectPoolManager.Instance.PreloadPrefab("Assets/03.Prefabs/Characters/TestEnemy.prefab", 5);
        StartCoroutine(Test_GameStart());
    }

    IEnumerator Test_GameStart()
    {
        yield return new WaitForSeconds(5);
        StartGame(null);
    }

    // 특정 스테이지 시작 요청
    public void StartGame(StageData stageToLoad)
    {
        if (CurrentGameState != GameState.MainMenu && CurrentGameState != GameState.StageEnded)
        {
#if UNITY_EDITOR
            Debug.LogWarning("게임 시작 불가: 현재 게임 상태가 메인 메뉴 또는 스테이지 종료가 아닙니다.");
#endif
            return;
        }

#if UNITY_EDITOR
        //Debug.Log($"스테이지 시작 요청: {stageToLoad.stageName}");
#endif
        CurrentGameState = GameState.StageStarting; // 스테이지 시작 중 상태로 변경
        // TODO: SceneManager를 사용하여 해당 스테이지 씬 로드 로직 추가
        // LoadSceneCompleted 등 씬 로드 완료 후 StageInProgress로 변경
        // For prototype, we'll immediately move to InProgress
        CurrentGameState = GameState.StageInProgress;
    }

    // 게임 일시 중지
    public void PauseGame()
    {
        if (CurrentGameState == GameState.StageInProgress)
        {
            CurrentGameState = GameState.StagePaused;
            Time.timeScale = 0f; // 게임 시간 정지 (유니티 전역)
#if UNITY_EDITOR
            Debug.Log("게임 일시 중지");
#endif
        }
    }

    // 게임 재개
    public void ResumeGame()
    {
        if (CurrentGameState == GameState.StagePaused)
        {
            CurrentGameState = GameState.StageInProgress;
            Time.timeScale = 1f; // 게임 시간 재개
#if UNITY_EDITOR
            Debug.Log("게임 재개");
#endif
        }
    }

    // 스테이지 종료 (성공 또는 실패)
    public void EndStage()
    {
        if (CurrentGameState == GameState.StageInProgress || CurrentGameState == GameState.StagePaused)
        {
            CurrentGameState = GameState.StageEnded;
            Time.timeScale = 1f; // 혹시 중지 상태였다면 재개
#if UNITY_EDITOR
            Debug.Log("스테이지 종료");
#endif
            // TODO: 스테이지 결과 화면 또는 메인 메뉴 로드 로직 추가
        }
    }

    // 게임 완전 종료 (어플리케이션 종료)
    public void QuitGame()
    {
        Debug.Log("게임 종료 요청");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 플레이 모드 종료
#endif
    }
}