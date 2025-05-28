using ProjectD_and_R.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    // --- 그리드 설정 변수 ---
    [Header("Grid Settings")]
    [SerializeField] private int gridSizeX = 20; // 그리드의 X축 크기 (셀 개수)
    [SerializeField] private int gridSizeY = 20; // 그리드의 Y축 크기 (셀 개수)
    [SerializeField] private float cellSize = 1f; // 각 그리드 셀의 크기 (월드 유닛)
    [SerializeField] private Vector3 gridOriginOffset = Vector3.zero; // 그리드 시작점 오프셋 (월드 좌표)

    // --- 그리드 데이터 ---
    private GridCell[,] grid; // 2차원 배열로 그리드 셀 데이터 저장

    // --- 프로퍼티 ---
    public int GridSizeX => gridSizeX;
    public int GridSizeY => gridSizeY;
    public float CellSize => cellSize;

    // --- 유니티 생명주기 ---
    protected override void Awake()
    {
        base.Awake();

        if(Instance == this)
        {
            InitializeGrid();
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Singleton 패턴에 의해 GridManager Instance 파괴, InitializeGrid 스킵");
#endif
        }
    }

    // --- 그리드 초기화 ---
    private void InitializeGrid()
    {
        grid = new GridCell[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                grid[x, y] = new GridCell
                {
                    X = x,
                    Y = y,
                    IsOccupied = false, // 초기에는 비어있음
                    CellType = GridCellType.Buildable // 기본적으로 배치 가능한 지역으로 설정 (조정 가능)
                };
            }
        }
#if UNITY_EDITOR
        Debug.Log($"Grid initialized with {gridSizeX}x{gridSizeY} cells.");
#endif
    }

    // --- 월드 좌표를 그리드 인덱스로 변환 ---
    public Vector2Int GetGridCoordinates(Vector3 worldPosition)
    {
        // 그리드 원점으로부터의 상대적인 위치 계산
        Vector3 relativePos = worldPosition - (transform.position + gridOriginOffset);

        // 그리드 셀 크기로 나누어 인덱스 계산
        int x = Mathf.FloorToInt(relativePos.x / cellSize);
        int y = Mathf.FloorToInt(relativePos.z / cellSize); // Y축 대신 Z축을 사용하는 경우가 많음 (XZ 평면)

        // 인덱스 범위 확인 및 클램프
        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);

        return new Vector2Int(x, y);
    }

    // --- 그리드 인덱스를 월드 좌표로 변환 ---
    public Vector3 GetWorldPosition(int x, int y)
    {
        // 그리드 인덱스의 중앙 월드 좌표 계산
        float worldX = x * cellSize + cellSize * 0.5f;
        float worldY = 0; // 3D 게임이므로 Y축 높이는 필요에 따라 조정
        float worldZ = y * cellSize + cellSize * 0.5f;

        return (transform.position + gridOriginOffset) + new Vector3(worldX, worldY, worldZ);
    }

    // --- 특정 그리드 셀 정보 가져오기 ---
    public GridCell GetGridCell(int x, int y)
    {
        if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
        {
            return grid[x, y];
        }
        Debug.LogWarning($"Grid coordinates ({x}, {y}) are out of bounds.");
        return null;
    }

    public GridCell GetGridCell(Vector3 worldPosition)
    {
        Vector2Int coords = GetGridCoordinates(worldPosition);
        return GetGridCell(coords.x, coords.y);
    }

    // --- 그리드 시각화 (디버깅용) ---
    private void OnDrawGizmos()
    {
        if (grid == null) return;

        Gizmos.color = Color.cyan;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 cellCenter = GetWorldPosition(x, y);
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize)); // 얇은 큐브로 그리드 표시
            }
        }
    }
}