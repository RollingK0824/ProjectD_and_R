using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGenerator
{
    RoomFactory roomFactory;
    //생성되는 모든방
    Dictionary<Vector2Int, Room> allRooms;
    //방 색상 테스트용
    Color[] zoneColors;
    //구역의 크기
    Vector2Int size;
    //방 생성 밀도
    float roomDensity;
    //방 생성을 정해줄 방향
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    //방 생성 랜덤 시드
    System.Random rand;

    public ZoneGenerator(RoomFactory factory, Dictionary<Vector2Int, Room> globalRooms,
                         Color[] colors,int zWidth, int zHeight, float rDensity, 
                         System.Random randomGenerator)
    {
        this.roomFactory = factory;
        this.allRooms = globalRooms;
        this.zoneColors = colors;
        this.size.x = zWidth;
        this.size.y = zHeight;
        this.roomDensity = rDensity;
        this.rand = randomGenerator;
    }

    /// <summary>
    /// 구역 생성
    /// </summary>
    /// <param name="zoneId">구역의 ID</param>
    /// <param name="zoneOffset">구역의 월드 좌표</param>
    /// <returns></returns>
    public List<Room> Generate(int zoneId, Vector2Int zoneOffset)
    {
        List<Room> currentZoneRooms = new List<Room>();
        HashSet<Vector2Int> visitedInZoneLocal = new HashSet<Vector2Int>();

        int maxRoomsInZone = Mathf.RoundToInt(size.x * size.y * roomDensity);
        if (maxRoomsInZone == 0)
        {
            return currentZoneRooms;
        }

        List<(Vector2Int fromLocal, Vector2Int toLocal)> frontier = new List<(Vector2Int, Vector2Int)>();
        Vector2Int startLocalPos = new Vector2Int(rand.Next(0, size.x), rand.Next(0, size.y));

        Room startRoom = PlaceRoomInZone(startLocalPos, zoneOffset, zoneId, currentZoneRooms);
        if (startRoom == null)
        {
            return currentZoneRooms;
        }
        visitedInZoneLocal.Add(startLocalPos);

        foreach (Vector2Int dir in directions)
        {
            Vector2Int nextLocalPos = startLocalPos + dir;
            if (IsInZoneBounds(nextLocalPos))
                frontier.Add((startLocalPos, nextLocalPos));
        }

        int roomsPlacedInZone = 1;
        while (frontier.Count > 0 && roomsPlacedInZone < maxRoomsInZone)
        {
            int randIndex = rand.Next(0, frontier.Count);
            var (fromLocal, toLocal) = frontier[randIndex];
            frontier.RemoveAt(randIndex);

            if (visitedInZoneLocal.Contains(toLocal)) continue;

            Room newRoom = PlaceRoomInZone(toLocal, zoneOffset, zoneId, currentZoneRooms);
            if (newRoom == null) continue;

            visitedInZoneLocal.Add(toLocal);
            roomsPlacedInZone++;

            Vector2Int fromWorldPos = fromLocal + zoneOffset;
            if (allRooms.TryGetValue(fromWorldPos, out Room fromRoom))
            {
                ConnectRoomsWithinZone(fromRoom, newRoom);
            }
            else
            {
                Debug.LogError($"[ZoneGenerator] From room at local {fromLocal} (world {fromWorldPos}) not found in allRoomsGlobalRef for Zone {zoneId}.");
            }


            foreach (var dir in directions)
            {
                Vector2Int nextLocalPosCandidate = toLocal + dir;
                if (IsInZoneBounds(nextLocalPosCandidate) && !visitedInZoneLocal.Contains(nextLocalPosCandidate))
                {
                    frontier.Add((toLocal, nextLocalPosCandidate));
                }
            }
        }
        return currentZoneRooms;
    }

    /// <summary>
    /// 구역안에 방을 생성 및 배치
    /// </summary>
    /// <param name="localPos"></param>
    /// <param name="zoneOffset"></param>
    /// <param name="zoneId"></param>
    /// <param name="zoneRoomList"></param>
    /// <returns></returns>
    private Room PlaceRoomInZone(Vector2Int localPos, Vector2Int zoneOffset, int zoneId, List<Room> zoneRoomList)
    {
        Vector2Int worldPos = localPos + zoneOffset;
        if (allRooms.ContainsKey(worldPos))
        {
            return allRooms[worldPos];
        }

        Room room = roomFactory.CreateRoom(worldPos, localPos, zoneId);
        allRooms[worldPos] = room;
        zoneRoomList.Add(room);

        Renderer roomRenderer = room.GetComponent<Renderer>();
        if (roomRenderer != null)
        {
            if (zoneId >= 0 && zoneId < zoneColors.Length)
            {
                roomRenderer.material.color = zoneColors[zoneId];
            }
        }
        return room;
    }

    /// <summary>
    /// 위치가 구역에 있는지 확인
    /// </summary>
    private bool IsInZoneBounds(Vector2Int localPos)
    {
        return localPos.x >= 0 && localPos.y >= 0 && localPos.x < size.x && localPos.y < size.y;
    }

    /// <summary>
    /// 길이 이어진 방들 연결
    /// </summary>
    private void ConnectRoomsWithinZone(Room roomA, Room roomB)
    {
        if (roomA == null || roomB == null) return;

        if (!roomA.ConnectedRooms.Contains(roomB))
            roomA.ConnectedRooms.Add(roomB);
        if (!roomB.ConnectedRooms.Contains(roomA))
            roomB.ConnectedRooms.Add(roomA);

        MapDebugVisualizer.DrawConnection(roomA.Position, roomB.Position, Color.green); // 구역 내부 연결선
    }
}
