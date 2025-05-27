using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField][Header("맵 넓이")] protected int _Map_Width = 5;
    [SerializeField][Header("맵 높이")] protected int _Map_Height = 10;
    [SerializeField][Header("층별 방 생성확률")] protected int _Floor_Percent = 50;
    [SerializeField][Header("층별 방 타입 확률")] RoomSpawnData[] _RoomSpawnData;

    bool[,] _Virtual_Map;
    GameObject[,] _Real_Map;
    RoomSpawnDataLoder _Loder;
    MapRenderer _MapRenderer;

    void Start()
    {
        _Virtual_Map = new bool[_Map_Height, _Map_Width];
        _Real_Map = new GameObject[_Map_Height, _Map_Width];

        _MapRenderer = GetComponent<MapRenderer>();
        _Loder = gameObject.AddComponent<RoomSpawnDataLoder>();
        _Loder.LoadRoomSpawnData((loadedData) =>
        {
            _RoomSpawnData = loadedData;
            VirtualMapGenerator();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            VirtualMapGenerator();
        }
    }

    /// <summary>
    /// 2차원 배열 상에 랜덤하게 맵 데이터 생성
    /// </summary>
    void VirtualMapGenerator()
    {
        _MapRenderer.InitializeMap(_Map_Height, _Map_Width, _Real_Map);
        VurtualMapGenerate();
        _MapRenderer.RenderMap(_Real_Map, _Virtual_Map);
    }

    /// <summary>
    /// 맵 데이터 생성
    /// </summary>
    void VurtualMapGenerate()
    {
        bool hasRoom = false;

        for (int height = 0; height < _Map_Height; height++)
        {
            for (int width = 0; width < _Map_Width; width++)
            {
                _Virtual_Map[height, width] = false;

                int RandomValue = Random.Range(0, 100);
                if (RandomValue < _Floor_Percent)
                {
                    hasRoom = true;
                    _Virtual_Map[height, width] = true;

                    RoomGenerate(_Real_Map[height, width].GetComponent<Room>(), height + 1, height, width);
                }
            }
            if (!hasRoom)
            {
                int width = Random.Range(0, _Map_Width);
                _Virtual_Map[height, width] = true;

                RoomGenerate(_Real_Map[height, width].GetComponent<Room>(), height + 1, height, width);
            }
            hasRoom = false;
        }

        ConnectedRoomsSet();
        EnsureAllRoomsReachable();
    }

    /// <summary>
    /// 방 데이터 생성
    /// </summary>
    /// <returns>확률로 생성된 랜덤 방 데이터</returns>
    void RoomGenerate(Room room, int floor, int height, int width)
    {
        RoomSpawnData data = _RoomSpawnData.FirstOrDefault(d => d._Floor == floor);

        float[] weights = new float[]
        {
        data._Nomal,
        data._Elite,
        data._Event,
        data._Shop,
        data._Reward
        };

        float total = weights.Sum();
        float rand = Random.Range(0, total);
        float cumulative = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            cumulative += weights[i];
            if (rand < cumulative)
            {
                room.Init((RoomType)i, height, width);
                return;
            }
        }
    }

    /// <summary>
    /// 생성된 방들이 진행할 수 있는 방을 찾아서 저장
    /// </summary>
    void ConnectedRoomsSet()
    {
        for (int height = 0; height < _Map_Height - 1; height++)
        {
            for (int width = 0; width < _Map_Width; width++)
            {
                if (_Virtual_Map[height, width])
                {
                    CheckNextFloorRooms(height, width, _Real_Map[height, width].GetComponent<Room>().Next);
                }
            }
        }
    }

    /// <summary>
    /// 위층 방 체크 함수
    /// </summary>
    void CheckNextFloorRooms(int h, int w, List<Room> rooms)
    {
        int nextH = h + 1;

        if (nextH >= _Map_Height) return;

        rooms.Clear();

        for (int i = -1; i <= 1; i++)
        {
            int checkW = w + i;
            if (checkW >= 0 && checkW < _Map_Width && _Virtual_Map[nextH, checkW])
            {
                rooms.Add(_Real_Map[nextH, checkW].GetComponent<Room>());
            }
        }

        if (rooms.Count == 0)
        {
            int minDistance = _Map_Width;
            Room room = null;

            for (int i = 0; i < _Map_Width; i++)
            {
                if (_Virtual_Map[nextH, i])
                {
                    int Distance = Mathf.Abs(i - w);
                    if (Distance < minDistance)
                    {
                        minDistance = Distance;
                        room = _Real_Map[nextH, i].GetComponent<Room>();
                    }
                }
            }

            if (room != null)
                rooms.Add(room);
        }
    }

    /// <summary>
    /// 연결된 방 없는지 체크 후 연결
    /// </summary>
    void EnsureAllRoomsReachable()
    {
        for (int height = 1; height < _Map_Height; height++)
        {
            for (int width = 0; width < _Map_Width; width++)
            {
                if (!_Virtual_Map[height, width]) continue;

                bool hasEntry = false;

                for (int i = -1; i <= 1; i++)
                {
                    int checkW = width + i;
                    if (checkW >= 0 && checkW < _Map_Width && _Virtual_Map[height - 1, checkW])
                    {
                        Room lowerRoom = _Real_Map[height - 1, checkW].GetComponent<Room>();
                        if (lowerRoom.Next.Contains(_Real_Map[height, width].GetComponent<Room>()))
                        {
                            hasEntry = true;
                            break;
                        }
                    }
                }

                if (!hasEntry)
                {
                    int minDistance = _Map_Width;
                    Room closestRoom = null;

                    for (int i = 0; i < _Map_Width; i++)
                    {
                        if (_Virtual_Map[height - 1, i])
                        {
                            int distance = Mathf.Abs(i - width);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                closestRoom = _Real_Map[height - 1, i].GetComponent<Room>();
                            }
                        }
                    }

                    if (closestRoom != null)
                    {
                        closestRoom.Next.Add(_Real_Map[height, width].GetComponent<Room>());
                    }
                }
            }
        }
    }

}
