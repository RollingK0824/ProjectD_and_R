using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFactory
{
    GameObject prefab;
    Transform parent;

    public RoomFactory(GameObject prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
    }

    /// <summary>
    /// 현재 만든 방 정리
    /// </summary>
    public void Clear()
    {
        foreach (Transform child in parent)
            GameObject.Destroy(child.gameObject);
    }

    /// <summary>
    /// 방 화면 생성
    /// </summary>
    public Room CreateRoom(Vector2Int pos)
    {
        Vector3 worldPos = new Vector3(pos.x * 2, 0, pos.y * 2);
        GameObject roomGO = GameObject.Instantiate(prefab, worldPos, Quaternion.identity, parent);
        roomGO.name = $"Room {pos.x},{pos.y}";
        Room roomComp = roomGO.GetComponent<Room>();
        if (roomComp != null)
        {
            roomComp.Position = pos;
            roomComp.Type = RoomType.Normal;
        }
        return roomComp;
    }
}