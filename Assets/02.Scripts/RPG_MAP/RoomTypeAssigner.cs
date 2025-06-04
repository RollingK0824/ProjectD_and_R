using System.Collections.Generic;
using UnityEngine;

public class RoomTypeAssigner
{
    private System.Random rand;

    public RoomTypeAssigner(System.Random randomGenerator)
    {
        rand = randomGenerator;
    }

    /// <summary>
    /// 특정 구역 내 방들의 타입을 할당
    /// </summary>
    public void AssignTypesToZoneRooms(List<Room> roomsInZone, int zoneId)
    {
        if (roomsInZone == null || roomsInZone.Count == 0)
            return;

        List<Room> assignableRooms = new List<Room>(roomsInZone);

        Room bossRoom = AssignFixedRoomType(assignableRooms, RoomType.Boss, zoneId, "BOSS");

        Room doorRoom = AssignFixedRoomType(assignableRooms, RoomType.Door, zoneId, "DOOR");

        Room shopRoom = AssignFixedRoomType(assignableRooms, RoomType.Shop, zoneId, "SHOP");

        int otherTypesAssignedCount = 0;
        foreach (Room room in roomsInZone)
        {
            if (room == bossRoom || room == doorRoom || room == shopRoom)
                continue;

            room.Type = GetRandomProbabilisticType();
            otherTypesAssignedCount++;
        }
    }

    /// <summary>
    /// 상점,문,보스방중 할당되지 않은 랜덤방 할당
    /// </summary>
    Room AssignFixedRoomType(List<Room> availableRooms, RoomType typeToAssign, int zoneId, string typeNameForLog)
    {
        if (availableRooms.Count > 0)
        {
            int roomIndex = rand.Next(0, availableRooms.Count);
            Room selectedRoom = availableRooms[roomIndex];
            selectedRoom.Type = typeToAssign;
            availableRooms.RemoveAt(roomIndex); 
            return selectedRoom;
        }
        else
            return null;
    }

    /// <summary>
    ///확률별 랜덤 방 할당
    /// </summary>
    RoomType GetRandomProbabilisticType()
    {
        double probNormal = 0.60;
        double probElite = 0.20;
        double probEvent = 0.10;
        double probReward = 0.10;

        double randomValue = rand.NextDouble(); 

        if (randomValue < probNormal)
        {
            return RoomType.Normal;
        }
        randomValue -= probNormal; 

        if (randomValue < probElite)
        {
            return RoomType.Elite;
        }
        randomValue -= probElite;

        if (randomValue < probEvent)
        {
            return RoomType.Event;
        }
        return RoomType.Reward;
    }
}