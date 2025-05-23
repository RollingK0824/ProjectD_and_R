using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField][Header("�� ����")] protected int _Map_Width = 5;
    [SerializeField][Header("�� ����")] protected int _Map_Height = 10;
    [SerializeField][Header("���� �� ����Ȯ��")] protected int _Floor_Percent = 50;
    [SerializeField][Header("���� �� Ÿ�� Ȯ��")] RoomSpawnData[] _RoomSpawnData;

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
    /// 2���� �迭 �� �����ϰ� �� ������ ����
    /// </summary>
    void VirtualMapGenerator()
    {
        VirtualMapInit();
        VurtualMapGenerate();
    }

    /// <summary>
    /// �� ������ �ʱ�ȭ
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
    /// �� ������ ����
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
    /// �� ������ ����
    /// </summary>
    /// <param name="floor">���� ���� ���� �� ����</param>
    /// <param name="height">���� X ��ǥ</param>
    /// <param name="width">���� Y ��ǥ</param>
    /// <returns>Ȯ���� ������ ���� �� ������</returns>
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
