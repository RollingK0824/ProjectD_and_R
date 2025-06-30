// Assets/Scripts/Managers/GameManager.cs
using UnityEngine;
using System;
using ProjectD_and_R.Enums;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameState _currentGameState = GameState.MainMenu;
    [SerializeField][Header("현재 씬 이름")] string CurrentScene = "Title";

    [SerializeField][Header("데이터 로더들")]public LoaderContainer LoaderContainer;

    public SceneArriveEvent sceneArriveEvent;
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

    [SerializeField]
    private ProjectD_and_R.Enums.TurnState _currentTurnState = ProjectD_and_R.Enums.TurnState.None;
    public ProjectD_and_R.Enums.TurnState CurrentTurnState
    {
        get { return _currentTurnState; }
        private set
        {
            if (_currentTurnState != value)
            {
                _currentTurnState = value;
                OnTurnStateChanged?.Invoke(_currentTurnState);
#if UNITY_EDITOR
                Debug.Log($"턴 상태 변경 : {_currentTurnState}");
#endif
            }
        }
    }

    // 게임 상태 변경 이벤트
    public event Action<GameState> OnGameStateChanged;

    // 턴 상태 변경 이벤트
    public event Action<ProjectD_and_R.Enums.TurnState> OnTurnStateChanged;

    protected override void Awake()
    {
        base.Awake();
        LoaderContainer = new LoaderContainer();
        sceneArriveEvent = new SceneArriveEvent();
        //ObjectPoolManager.Instance.Initialize(this.gameObject.transform);
        //ObjectPoolManager.Instance.PreloadPrefab("Assets/03.Prefabs/Characters/TestEnemy.prefab", 1);
        //StartCoroutine(Test_GameStart());
    }

    // 특정 스테이지 시작 요청
    public void StartGame(StageData stageToLoad)
    {
        if (CurrentGameState != GameState.MainMenu && CurrentGameState != GameState.StageEnded)
            return;

        CurrentGameState = GameState.StageStarting; // 스테이지 시작 중 상태로 변경
        CurrentGameState = GameState.StageInProgress;
    }

    // 게임 일시 중지
    public void PauseGame()
    {
        if (CurrentGameState == GameState.StageInProgress)
        {
            CurrentGameState = GameState.StagePaused;
            Time.timeScale = 0f; // 게임 시간 정지 (유니티 전역)
        }
    }

    // 게임 재개
    public void ResumeGame()
    {
        if (CurrentGameState == GameState.StagePaused)
        {
            CurrentGameState = GameState.StageInProgress;
            Time.timeScale = 1f; // 게임 시간 재개
        }
    }

    // 스테이지 종료 (성공 또는 실패)
    public void EndStage()
    {
        if (CurrentGameState == GameState.StageInProgress || CurrentGameState == GameState.StagePaused)
        {
            CurrentGameState = GameState.StageEnded;
            Time.timeScale = 1f; // 혹시 중지 상태였다면 재개
        }
    }

    // 게임 완전 종료 (어플리케이션 종료)
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 플레이 모드 종료
#endif
    }

    public void GoToScene(string Scene)
    {
        LodingSystem.LoadScene(Scene);
    }

    private void OnDestroy()
    {
        Debug.Log("파괴");
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentScene = scene.name;
        sceneArriveEvent.SceneEvent(CurrentScene);
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}