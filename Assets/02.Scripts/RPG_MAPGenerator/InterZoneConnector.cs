using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterZoneConnector
{
    //모든 구역
    List<List<Room>> ZoneByRooms;
    //모든 구역의 길을 담당할 메인구역
    int hubZoneIndex;
    //구역별로 이동가능한 통로 개수
    int connectionsPerZonePair;
    //구역별로 이동가능한 방이 겹치지않게 설정
    HashSet<Room> allGatewayRooms = new HashSet<Room>();


    public InterZoneConnector(List<List<Room>> roomsByZone, int hubIdx, int connsPerPair)
    {
        ZoneByRooms = roomsByZone;
        hubZoneIndex = hubIdx;
        connectionsPerZonePair = connsPerPair;
    }

    /// <summary>
    /// 생성된 구역들 연결
    /// </summary>
    public void ConnectAllZones()
    {
        allGatewayRooms.Clear();

        if (ZoneByRooms.Count < 2)
            return;

        if (hubZoneIndex < 0 || hubZoneIndex >= ZoneByRooms.Count)
            return;

        List<Room> hubZoneRooms = ZoneByRooms[hubZoneIndex];

        if (hubZoneRooms == null || hubZoneRooms.Count == 0)
            return;


        for (int i = 0; i < ZoneByRooms.Count; i++)
        {
            if (i == hubZoneIndex) continue;

            if (i < 0 || i >= ZoneByRooms.Count)
                continue;

            List<Room> currentZoneRooms = ZoneByRooms[i];

            if (currentZoneRooms == null || currentZoneRooms.Count == 0)
                continue;


            int connectionsMadeForThisPair = 0;
            for (int k = 0; k < connectionsPerZonePair; k++)
            {
                Room bestRoomFromCurrentZone = null;
                Room bestRoomFromHubZone = null;
                float minDistanceSq = float.MaxValue;

                foreach (Room r1 in currentZoneRooms)
                {
                    if (allGatewayRooms.Contains(r1)) continue;
                    foreach (Room r2 in hubZoneRooms)
                    {
                        if (allGatewayRooms.Contains(r2)) continue;

                        float distSq = Vector2.SqrMagnitude(r1.Position - r2.Position);
                        if (distSq < minDistanceSq)
                        {
                            minDistanceSq = distSq;
                            bestRoomFromCurrentZone = r1;
                            bestRoomFromHubZone = r2;
                        }
                    }
                }

                if (bestRoomFromCurrentZone != null && bestRoomFromHubZone != null)
                {
                    EstablishGatewayConnection(bestRoomFromCurrentZone, bestRoomFromHubZone);
                    allGatewayRooms.Add(bestRoomFromCurrentZone);
                    allGatewayRooms.Add(bestRoomFromHubZone);
                    connectionsMadeForThisPair++;
                }
                else
                {
                    break;
                }
            }
        }
    }
    
    /// <summary>
    /// 구역끼리 이어진 방들끼리 연결
    /// </summary>
    private void EstablishGatewayConnection(Room roomA, Room roomB)
    {
        if (roomA == null || roomB == null) return;

        if (!roomA.ConnectedRooms.Contains(roomB))
            roomA.ConnectedRooms.Add(roomB);
        if (!roomB.ConnectedRooms.Contains(roomA))
            roomB.ConnectedRooms.Add(roomA);

        MapDebugVisualizer.DrawConnection(roomA.Position, roomB.Position, Color.yellow);
    }
}
