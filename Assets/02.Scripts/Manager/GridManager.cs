using NUnit.Framework;
using ProjectD_and_R.Enums;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
            Initialize();
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Singleton 패턴에 의해 GridManager Instance 파괴, InitializeGrid 스킵");
#endif
        }
    }

    // --- 그리드 초기화 ---
    private void Initialize()
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
                    CellType = GridCellType.Buildable // 기본적으로 배치 가능한 지역으로 설정 (조정 가능)
                };
            }
        }
#if UNITY_EDITOR
        Debug.Log($"Grid initialized with {gridSizeX}x{gridSizeY} cells.");
#endif
    }

    private bool IsValidGridPos(int x, int y)
    {
        return x >= 0 && x< gridSizeX && y >= 0 && y< gridSizeY;
    }

    public void RegisterObject(IGridObject gridObject, int x, int y)
    {
        if (!IsValidGridPos(x, y)) return;
        grid[x, y].PlacedObject = gridObject.GameObject;
    }

    public void RegisterObject(IGridObject gridObject, Vector2Int pos)
    {
        if (!IsValidGridPos(pos.x, pos.y)) return;
        grid[pos.x, pos.y].PlacedObject = gridObject.GameObject;
    }

    public void UnRegisterObject(int x, int y)
    {
        if (!IsValidGridPos(x, y)) return;
        grid[x, y].PlacedObject = null;
    }

    public void UnRegisterObject(Vector2Int pos)
    {
        if (!IsValidGridPos(pos.x, pos.y)) return;
        grid[pos.x, pos.y].PlacedObject = null;
    }

    public void MoveObject(IGridObject gridObject, int oldX, int oldY, int newX, int newY)
    {
        if(!IsValidGridPos(oldX, oldY) || !IsValidGridPos(newX,newY)) return;
        grid[oldX, oldY].PlacedObject = null;
        grid[newX,newY].PlacedObject = gridObject.GameObject;
    }

    public void MoveObject(IGridObject gridObject, Vector2Int oldPos, Vector2Int newPos)
    {
        if (!IsValidGridPos(oldPos.x, oldPos.y) || !IsValidGridPos(newPos.x, newPos.y)) return;
        grid[oldPos.x, oldPos.y].PlacedObject = null;
        grid[newPos.y, newPos.y].PlacedObject = gridObject.GameObject;
    }

    /// <summary>
    /// 중심 좌표에서 반지름 내에 있는 모든 IGridObjcet를 찾아 반환
    /// </summary>
    /// <param name="centerGridPos">중심 좌표</param>
    /// <param name="radius">반지름</param>
    /// <returns></returns>
    public List<IGridObject> GetObjectsInRadius(Vector2Int centerGridPos, float radius)
    {
        List<IGridObject> foundObjects = new List<IGridObject>();
        HashSet<IGridObject> uniqueObjects = new HashSet<IGridObject>(); // 중복 방지를 위한 HashSet

        // 사각형 바운딩 박스 계산 (최소 및 최대 그리드 인덱스)
        int minX = Mathf.FloorToInt(centerGridPos.x - radius);
        int maxX = Mathf.CeilToInt(centerGridPos.x + radius);
        int minY = Mathf.FloorToInt(centerGridPos.y - radius);
        int maxY = Mathf.CeilToInt(centerGridPos.y + radius);

        // 그리드 경계에 맞춰 범위 클램프
        minX = Mathf.Max(0, minX);
        maxX = Mathf.Min(gridSizeX - 1, maxX);
        minY = Mathf.Max(0, minY);
        maxY = Mathf.Min(gridSizeY - 1, maxY);

        // 바운딩 박스 내의 모든 그리드 셀 순회
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                // 현재 순회 중인 그리드 셀
                Vector2Int currentCellPos = new Vector2Int(x, y);

                if (Vector2.Distance(centerGridPos, currentCellPos) <= radius)
                {
                    // 해당 셀에 오브젝트가 배치되어 있다면
                    if (grid[x, y].PlacedObject != null)
                    {
                        IGridObject foundGridObject = grid[x, y].PlacedObject.GetComponent<IGridObject>();
                        if (foundGridObject != null)
                        {
                            // HashSet을 사용하여 중복 추가 방지 (하나의 오브젝트가 여러 셀에 걸쳐있을 경우 등)
                            if (uniqueObjects.Add(foundGridObject))
                            {
                                foundObjects.Add(foundGridObject);
                            }
                        }
                    }
                }
            }
        }
        return foundObjects;
    }


    /// <summary>
    /// 월드 좌표를 그리드 좌표로 변환
    /// </summary>
    /// <param name="worldPosition">월드 좌표</param>
    /// <returns></returns>
    public Vector2Int WorldToGridPos(Vector3 worldPosition)
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

    /// <summary>
    /// 그리드 좌표를 월드 좌표로 변환
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GridToWorldPos(int x, int y)
    {
        // 그리드 인덱스의 중앙 월드 좌표 계산
        float worldX = x * cellSize + cellSize * 0.5f;
        float worldY = 0; // 3D 게임이므로 Y축 높이는 필요에 따라 조정
        float worldZ = y * cellSize + cellSize * 0.5f;

        return (transform.position + gridOriginOffset) + new Vector3(worldX, worldY, worldZ);
    }

    /// <summary>
    /// 그리드 좌표를 월드 좌표로 변환
    /// </summary>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    public Vector3 GridToWorldPos(Vector2 gridPos)
    {
        // 그리드 인덱스의 중앙 월드 좌표 계산
        float worldX = gridPos.x * cellSize + cellSize * 0.5f;
        float worldY = 0; // 3D 게임이므로 Y축 높이는 필요에 따라 조정
        float worldZ = gridPos.y * cellSize + cellSize * 0.5f;

        return (transform.position + gridOriginOffset) + new Vector3(worldX, worldY, worldZ);
    }

    /// <summary>
    /// 특정 그리드 셀 정보 가져오기
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public GridCell GetGridCell(int x, int y)
    {
        if (IsValidGridPos(x,y))
        {
            return grid[x, y];
        }
        Debug.LogWarning($"Grid coordinates ({x}, {y}) are out of bounds.");
        return null;
    }

    /// <summary>
    /// 특정 그리드 셀 정보 가져오기
    /// </summary>
    /// <param name="worldPosition">월드 좌표</param>
    /// <returns></returns>
    public GridCell GetGridCell(Vector3 worldPosition)
    {
        Vector2Int gridPos = WorldToGridPos(worldPosition);
        return GetGridCell(gridPos.x, gridPos.y);
    }


    public void SetGridCell(int x, int y)
    {
        /* Do Nothing */
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
                Vector3 cellCenter = GridToWorldPos(x, y);
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize)); // 얇은 큐브로 그리드 표시
            }
        }
    }
}