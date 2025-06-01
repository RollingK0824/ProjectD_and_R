using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("맵 너비")] public int width = 10;
    [Header("맵 높이")] public int height = 10;
    [Range(0.1f, 1f)]
    [Header("방 생성 정도")] public float roomDensity = 0.4f;
    public GameObject roomPrefab;

    //생성된 방을 저장할 딕셔너리
    Dictionary<Vector2Int, Room> rooms = new();

    //도착 방 여부를 확인할 해쉬셋
    HashSet<Vector2Int> visited = new();

    //방 생성 방향
    Vector2Int[] directions = {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    RoomFactory roomFactory;

    void Start()
    {
        roomFactory = new RoomFactory(roomPrefab, transform);
        GenerateMazePrim();
    }

    /// <summary>
    /// PRIM알고리즘 맵 생성 함수
    /// </summary>
    void GenerateMazePrim()
    {
        rooms.Clear();
        visited.Clear();

        //생성될 방 갯수
        int maxRooms = Mathf.RoundToInt(width * height * roomDensity);

        roomFactory.Clear();
        List<(Vector2Int from, Vector2Int to)> frontier = new();
        Vector2Int start = new(Random.Range(0, width), Random.Range(0, height));
        visited.Add(start);
        PlaceRoom(start);

        foreach (Vector2Int dir in directions)
        {
            Vector2Int next = start + dir;
            if (IsInBounds(next))
                frontier.Add((start, next));
        }

        while (frontier.Count > 0 && visited.Count < maxRooms)
        {
            int randIndex = Random.Range(0, frontier.Count);
            var (from, to) = frontier[randIndex];
            frontier.RemoveAt(randIndex);

            if (visited.Contains(to)) continue;

            visited.Add(to);
            PlaceRoom(to);
            ConnectRooms(from, to);

            foreach (var dir in directions)
            {
                Vector2Int next = to + dir;
                if (IsInBounds(next) && !visited.Contains(next))
                    frontier.Add((to, next));
            }
        }

    }

    /// <summary>
    /// 맵 경계 체크
    /// </summary>
    bool IsInBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < width && pos.y < height;
    }

    /// <summary>
    /// 방 화면 생성 
    /// </summary>

    void PlaceRoom(Vector2Int pos)
    {
        Room room = roomFactory.CreateRoom(pos);
        rooms[pos] = room;
    }


    /// <summary>
    /// 이동 가능 방 연결
    /// </summary>
    void ConnectRooms(Vector2Int from, Vector2Int to)
    {
        rooms[from].ConnectedRooms.Add(rooms[to]);
        rooms[to].ConnectedRooms.Add(rooms[from]);
        DebugDrawConnection(from, to);
    }

    void DebugDrawConnection(Vector2Int a, Vector2Int b)
    {
#if UNITY_EDITOR
        Vector3 start = new Vector3(a.x * 2, 0, a.y * 2);
        Vector3 end = new Vector3(b.x * 2, 0, b.y * 2);
        Debug.DrawLine(start, end, Color.green, 100f);
#endif
    }
}
