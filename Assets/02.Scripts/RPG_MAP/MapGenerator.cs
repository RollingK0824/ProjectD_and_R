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

    Room[,] _Virtual_Map;
    RoomSpawnDataLoder _Loder;
    MapRenderer _MapRenderer;

    void Start()
    {
        _MapRenderer = GetComponent<MapRenderer>();

        _Loder = gameObject.AddComponent<RoomSpawnDataLoder>();
        _Loder.LoadRoomSpawnData((loadedData) =>
        {
            _RoomSpawnData = loadedData;
            VirtualMapGenerator();
        });
    }

    /// <summary>
    /// 2차원 배열 상에 랜덤하게 맵 데이터 생성
    /// </summary>
    void VirtualMapGenerator()
    {
        VirtualMapInit();
        VurtualMapGenerate();
    }

    /// <summary>
    /// 맵 데이터 초기화
    /// </summary>
    void VirtualMapInit()
    {
        if (_Virtual_Map == null)
            _Virtual_Map = new Room[_Map_Height, _Map_Width];

        else
        {
            for (int i = 0; i < _Map_Height; i++)
            {
                for (int j = 0; j < _Map_Width; j++)
                    _Virtual_Map[i, j] = null;
            }
        }
    }

    /// <summary>
    /// 맵 데이터 생성
    /// </summary>
    void VurtualMapGenerate()
    {
        for (int height = 0; height < _Map_Height; height++)
        {
            for (int width = 0; width < _Map_Width; width++)
            {
                int RandomValue = Random.Range(0, 100);
                if (RandomValue < _Floor_Percent)
                {
                    _Virtual_Map[height, width] = RoomGenerate(height + 1, height, width);
                    _MapRenderer.DrawMap(_Virtual_Map[height, width]);
                }

            }
        }
    }

    /// <summary>
    /// 방 데이터 생성
    /// </summary>
    /// <param name="floor">현재 생성 중인 층 정보</param>
    /// <param name="height">방의 X 좌표</param>
    /// <param name="width">방의 Y 좌표</param>
    /// <returns>확률로 생성된 랜덤 방 데이터</returns>
    Room RoomGenerate(int floor, int height, int width)
    {
        RoomSpawnData data = _RoomSpawnData.FirstOrDefault(d => d._Floor == floor);
        Room retunRoom = new Room();
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
                retunRoom.Init((RoomType)i, height, width);
                return retunRoom;
            }
        }

        retunRoom.Init(RoomType.Normal, height, width);
        return retunRoom;
    }

}
