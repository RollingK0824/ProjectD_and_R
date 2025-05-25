using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapRenderer : MonoBehaviour
{
    [SerializeField] Transform _MapParent;         
    [SerializeField] GameObject[] _RoomPrefabs;
    [SerializeField][Header("맵 사이(폭)")] float xSize;
    [SerializeField][Header("맵 사이(높이)")] float ySize;
    
    int _mapHeight;
    int _mapWidth;

    /// <summary>
    /// 맵 크기만큼 생성
    /// </summary>
    public void InitializeMap(int mapHeight, int mapWidth, GameObject[,] map)
    {
        _mapHeight = mapHeight;
        _mapWidth = mapWidth;

        for (int h = 0; h < _mapHeight; h++)
        {
            for (int w = 0; w < _mapWidth; w++)
            {
                float xPos = (w - 2) * xSize;
                float zPos = h * ySize;

                if (map[h, w] == null)
                {
                    GameObject roomObj = Instantiate(_RoomPrefabs[0], _MapParent);
                    roomObj.transform.position = new Vector3(xPos, 0f, zPos);
                    map[h, w] = roomObj;
                }

                map[h, w].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 가상맵에서 활성화된 구역을 찾아서 맵을 활성화
    /// </summary>   
    public void RenderMap(GameObject[,] map, bool[,] _Virtual_Map)
    {
        for (int h = 0; h < _mapHeight; h++)
        {
            for (int w = 0; w < _mapWidth; w++)
            {
                GameObject roomObj = map[h, w];
                if (_Virtual_Map[h,w])
                {
                    roomObj.SetActive(true);

                    Room roomComp = roomObj.GetComponent<Room>();
                    if (roomComp != null)
                    {
                        UpdateRoomVisual(roomObj, roomComp.Type);
                    }
                }
                else
                {
                    roomObj.SetActive(false);
                }
            }
        }
    }

    //3d 맵 만들어지긴전 그냥 보기 편하게
    void UpdateRoomVisual(GameObject roomObj, RoomType type)
    {
        Renderer renderer = roomObj.GetComponent<Renderer>();
        if (renderer != null)
        {
            switch (type)
            {
                case RoomType.Normal:
                    renderer.material.color = Color.white;
                    break;
                case RoomType.Elite:
                    renderer.material.color = Color.red;
                    break;
                case RoomType.Event:
                    renderer.material.color = Color.yellow;
                    break;
                case RoomType.Shop:
                    renderer.material.color = Color.green;
                    break;
                case RoomType.Reward:
                    renderer.material.color = Color.cyan;
                    break;
                default:
                    renderer.material.color = Color.gray;
                    break;
            }
        }
    }
}


