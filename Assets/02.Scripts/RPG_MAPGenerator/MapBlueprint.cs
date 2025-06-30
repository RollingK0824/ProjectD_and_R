using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MapBlueprint
{
    public int Seed; 
    public int NumberOfZones;
    public Vector2Int Size;
    public float RoomDensity;
    public int GapBetweenZones;
    public int ConnectionsBetweenZones;

    public List<RoomBlueprint> AllRoomBlueprints = new List<RoomBlueprint>();

    [System.Serializable]
    public class RoomBlueprint
    {
        public Vector2Int GridPosition;
        public RoomType Type;
        public int ZoneId; 
        public List<Vector2Int> ConnectedRoomsPositions = new List<Vector2Int>();
        public bool visite;
        public RoomBlueprint(Room room)
        {
            GridPosition = room.Position;
            Type = room.Type;
            ZoneId = room.ZoneID;
            visite = room.visite;

            if (room.ConnectedRooms != null)
            {
                foreach (var connectedRoom in room.ConnectedRooms)
                {
                    if (connectedRoom != null)
                        ConnectedRoomsPositions.Add(connectedRoom.Position);
                }
            }
        }
    }

    public MapBlueprint(int seed, int numberOfZones, Vector2Int size, float roomDensity, int gapBetweenZones, int connectionsBetweenZones)
    {
        Seed = seed;
        NumberOfZones = numberOfZones;
        Size = size;
        RoomDensity = roomDensity;
        GapBetweenZones = gapBetweenZones;
        ConnectionsBetweenZones = connectionsBetweenZones;
    }
}