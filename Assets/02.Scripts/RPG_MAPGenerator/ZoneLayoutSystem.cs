using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ZoneLayoutSystem
{
    int numberOfZones;
    Vector2Int Size;
    float roomDensity;
    int gapBetweenZones;
    int connectionsBetweenZones;
    System.Random rand;

    [Header("공통 설정")]
    Color[] zoneColors;

    //모든방
    Dictionary<Vector2Int, Room> allRoomsGlobal = new Dictionary<Vector2Int, Room>();

    //모든 구역 및 구역의 방
    List<List<Room>> roomsByZone = new List<List<Room>>();
    RoomFactory roomFactory;
    ZoneGenerator zoneGenerator;
    InterZoneConnector interZoneConnector;
    RoomTypeAssigner roomTypeAssigner;

    static readonly Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    public ZoneLayoutSystem(int numberOfzones,Vector2Int size, float density, int gap, int connect)
    {
        numberOfZones = numberOfzones;
        Size = size;
        roomDensity = density;
        gapBetweenZones = gap;
        connectionsBetweenZones = connect;
        rand = new System.Random();
    }
    public MapBlueprint GenerateNewMap(GameObject roomPrefab, GameObject MapParent)
    {
        if (RpgManager.Instance.isCreateMap)
            return null;

        int newSeed = rand.Next(); 
        rand = new System.Random(newSeed);

        allRoomsGlobal.Clear();
        roomsByZone.Clear();
        if (roomFactory != null) roomFactory.ClearAllRooms();

        InitializeZoneColors();

        roomFactory = new RoomFactory(roomPrefab, MapParent.transform);
        zoneGenerator = new ZoneGenerator(roomFactory, allRoomsGlobal, zoneColors,
                                            Size.x, Size.y, roomDensity, rand); 
        interZoneConnector = new InterZoneConnector(roomsByZone, 0, connectionsBetweenZones);
        roomTypeAssigner = new RoomTypeAssigner(rand);

        GenerateMapLayoutInternal(); 

        interZoneConnector.ConnectAllZones();

        MapBlueprint blueprint = new MapBlueprint(newSeed, numberOfZones, Size, roomDensity, gapBetweenZones, connectionsBetweenZones);
        foreach (Room room in allRoomsGlobal.Values)
        {
            blueprint.AllRoomBlueprints.Add(new MapBlueprint.RoomBlueprint(room));
        }

        RpgManager.Instance.isCreateMap = true;

        return blueprint; 
    }

    public void LoadMapFromBlueprint(MapBlueprint blueprint, GameObject roomPrefab, GameObject MapParent)
    {
        numberOfZones = blueprint.NumberOfZones;
        Size = blueprint.Size;
        roomDensity = blueprint.RoomDensity;
        gapBetweenZones = blueprint.GapBetweenZones;
        connectionsBetweenZones = blueprint.ConnectionsBetweenZones;
        rand = new System.Random(blueprint.Seed); 

        allRoomsGlobal.Clear();
        roomsByZone.Clear();
        if (roomFactory != null) roomFactory.ClearAllRooms(); 

        InitializeZoneColors();

        roomFactory = new RoomFactory(roomPrefab, MapParent.transform);
        
        Dictionary<Vector2Int, Room> reconstructedRooms = new Dictionary<Vector2Int, Room>();
        List<List<Room>> reconstructedRoomsByZone = new List<List<Room>>();

        foreach (var roomBlueprint in blueprint.AllRoomBlueprints)
        {
            Room newRoom = roomFactory.CreateRoom(roomBlueprint.GridPosition, Vector2Int.zero, roomBlueprint.ZoneId);

            newRoom.Position = roomBlueprint.GridPosition;
            newRoom.Type = roomBlueprint.Type;
            newRoom.ZoneID = roomBlueprint.ZoneId;
            newRoom.ConnectedRooms = new List<Room>();
            newRoom.visite = roomBlueprint.visite;

            if (newRoom.TryGetComponent<Renderer>(out Renderer roomRenderer))
            {
                roomRenderer.material.color = GetZoneColor(roomBlueprint.ZoneId);
            }
            reconstructedRooms.Add(newRoom.Position, newRoom);

            while (reconstructedRoomsByZone.Count <= roomBlueprint.ZoneId)
            {
                reconstructedRoomsByZone.Add(new List<Room>());
            }
            reconstructedRoomsByZone[roomBlueprint.ZoneId].Add(newRoom);
        }

        foreach (var roomBlueprint in blueprint.AllRoomBlueprints)
        {
            Room currentRoom = reconstructedRooms[roomBlueprint.GridPosition];
            foreach (var connectedPos in roomBlueprint.ConnectedRoomsPositions)
            {
                if (reconstructedRooms.TryGetValue(connectedPos, out Room connectedRoom))
                {
                    currentRoom.ConnectedRooms.Add(connectedRoom);
                }
            }
        }

        allRoomsGlobal = reconstructedRooms; 
        roomsByZone = reconstructedRoomsByZone;
    }


    /// <summary>
    /// 메인 방을 기준으로 각 방향에 구역 배치
    /// </summary>
    void GenerateMapLayoutInternal() 
    {
        if (numberOfZones <= 0)
            return;

        Vector2Int hubOffset = Vector2Int.zero;
        List<Room> hubRooms = zoneGenerator.Generate(0, hubOffset);
        if (hubRooms.Count > 0 && roomTypeAssigner != null)
        {
            roomTypeAssigner.AssignTypesToZoneRooms(hubRooms, 0);
        }
        roomsByZone.Add(hubRooms);

        if (numberOfZones == 1)
            return;

        List<Vector2Int> potentialSurroundingOffsets = new List<Vector2Int>();
        int effectiveOffsetX = Size.x + gapBetweenZones;
        int effectiveOffsetY = Size.y + gapBetweenZones;

        potentialSurroundingOffsets.Add(new Vector2Int(effectiveOffsetX, 0));
        potentialSurroundingOffsets.Add(new Vector2Int(-effectiveOffsetX, 0));
        potentialSurroundingOffsets.Add(new Vector2Int(0, effectiveOffsetY));
        potentialSurroundingOffsets.Add(new Vector2Int(0, -effectiveOffsetY));
        potentialSurroundingOffsets.Add(new Vector2Int(effectiveOffsetX, effectiveOffsetY));
        potentialSurroundingOffsets.Add(new Vector2Int(-effectiveOffsetX, effectiveOffsetY));
        potentialSurroundingOffsets.Add(new Vector2Int(effectiveOffsetX, -effectiveOffsetY));
        potentialSurroundingOffsets.Add(new Vector2Int(-effectiveOffsetX, -effectiveOffsetY));

        potentialSurroundingOffsets = potentialSurroundingOffsets.OrderBy(pos => rand.Next()).ToList();

        int numberOfSurroundingZonesToPlace = Mathf.Min(numberOfZones - 1, 8);

        for (int i = 0; i < numberOfSurroundingZonesToPlace; i++)
        {
            int currentZoneId = i + 1;
            if (i >= potentialSurroundingOffsets.Count) break;

            Vector2Int chosenOffset = potentialSurroundingOffsets[i];
            List<Room> surroundingZoneRooms = zoneGenerator.Generate(currentZoneId, chosenOffset);
            if (surroundingZoneRooms.Count > 0 && roomTypeAssigner != null)
            {
                roomTypeAssigner.AssignTypesToZoneRooms(surroundingZoneRooms, currentZoneId);
            }
            roomsByZone.Add(surroundingZoneRooms);
        }
    }

    /// <summary>
    /// 구역별 색상 할당 테스트용 
    /// </summary>
    void InitializeZoneColors()
    {
        int totalZonesToColor = Mathf.Min(numberOfZones, 9);
        if (zoneColors == null || zoneColors.Length < totalZonesToColor)
        {
            zoneColors = new Color[totalZonesToColor];
            if (totalZonesToColor > 0)
            {
                for (int i = 0; i < totalZonesToColor; i++)
                {
                    zoneColors[i] = Color.HSVToRGB((float)i / totalZonesToColor, 0.8f, 0.95f);
                }
            }
        }
    }

    private Color GetZoneColor(int zoneId)
    {
        if (zoneColors != null && zoneId >= 0 && zoneId < zoneColors.Length)
        {
            return zoneColors[zoneId];
        }
        return Color.white; 
    }

    public MapBlueprint CreateBlueprintFromCurrentState()
    {
        MapBlueprint currentBlueprint = new MapBlueprint(
            rand.Next(),
            numberOfZones,
            Size,
            roomDensity,
            gapBetweenZones,
            connectionsBetweenZones
        );

        foreach (Room room in allRoomsGlobal.Values)
        {
            currentBlueprint.AllRoomBlueprints.Add(new MapBlueprint.RoomBlueprint(room));
        }
        return currentBlueprint;
    }
}
