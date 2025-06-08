using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        if (roomsInZone == null || roomsInZone.Count < 2)
        {
            if (roomsInZone?.Count == 1)
            {
                roomsInZone[0].Type = RoomType.Boss;
            }
            return;
        }

        List<Room> assignableRooms = new List<Room>(roomsInZone);

        Room doorRoom = AssignFixedRoomType(assignableRooms, RoomType.Door, zoneId, "DOOR");
        if (doorRoom == null)
            return;

        Room bossRoom = null;
        if (assignableRooms.Any())
        {
            List<Room> farthestRooms = new List<Room>();
            float maxDistanceSq = -1f;

            foreach (Room candidateRoom in assignableRooms)
            {
                float distanceSq = (candidateRoom.Position - doorRoom.Position).sqrMagnitude;

                if (distanceSq > maxDistanceSq)
                {
                    maxDistanceSq = distanceSq;
                    farthestRooms.Clear();
                    farthestRooms.Add(candidateRoom);
                }
                else if (distanceSq == maxDistanceSq)
                {
                    farthestRooms.Add(candidateRoom);
                }
            }

            if (farthestRooms.Any())
            {
                bossRoom = farthestRooms[rand.Next(0, farthestRooms.Count)];
                bossRoom.Type = RoomType.Boss;
                assignableRooms.Remove(bossRoom);
            }
        }


        Room shopRoom = AssignFixedRoomType(assignableRooms, RoomType.Shop, zoneId, "SHOP");

        foreach (Room room in roomsInZone)
        {
            if (room == bossRoom || room == doorRoom || room == shopRoom)
                continue;

            room.Type = GetRandomProbabilisticType();
        }
    }

    /// <summary>
    /// 일반 랜덤방 타입 지정함수
    /// </summary>
    private Room AssignFixedRoomType(List<Room> availableRooms, RoomType typeToAssign, int zoneId, string typeNameForLog)
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
    /// 확률별 타입 전환
    /// </summary>
    private RoomType GetRandomProbabilisticType()
    {

        double probNormal = 0.50; // 일반 
        double probElite = 0.30; // 엘리트
        double probEvent = 0.10; // 이벤트 
        double probReward = 0.10; // 보상 

        double randomValue = rand.NextDouble();

        if (randomValue < probNormal) return RoomType.Normal;
        randomValue -= probNormal;

        if (randomValue < probElite) return RoomType.Elite;
        randomValue -= probElite;

        if (randomValue < probEvent) return RoomType.Event;

        return RoomType.Reward;
    }
}