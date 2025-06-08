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
    public void ClearAllRooms()
    {
        foreach (Transform child in parent)
            GameObject.Destroy(child.gameObject);
    }

    /// <summary>
    /// 월드 그리드 좌표를 기준으로 구역들을 그려줌
    /// </summary>
    public Room CreateRoom(Vector2Int pos, Vector2Int localGridPosInZone, int zoneId)
    {
        Vector3 worldPos3D = new Vector3(pos.x * 1.5f, 0, pos.y * 1.5f);
        GameObject roomGO = GameObject.Instantiate(prefab, worldPos3D, Quaternion.identity, parent);
        roomGO.name = $"Zone{zoneId}_Room({localGridPosInZone.x},{localGridPosInZone.y})";

        Room roomComp = roomGO.GetComponent<Room>();
        if (roomComp == null)
        {
            roomComp = roomGO.AddComponent<Room>();
        }

        roomComp.Position = pos;
        roomComp.ZoneID = zoneId;
        roomComp.Type = RoomType.Normal;

        return roomComp;
    }
}