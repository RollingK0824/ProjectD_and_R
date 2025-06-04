using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZoneLayoutManager : MonoBehaviour
{

    [Header("구역 갯수")][SerializeField] private int numberOfZones;
    [Header("구역 사이즈")][SerializeField] private Vector2Int Size;

    [Header("방 생성 밀도")][Range(0.1f, 1f)][SerializeField] private float roomDensity = 0.4f;
    [Header("구역 별 그리그 거리")][SerializeField] private int gapBetweenZones = 0;
    [Header("구역 별 연결 통로 갯수")][SerializeField] private int connectionsBetweenZones = 1;

    [Header("공통 설정")]
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private Color[] zoneColors;

    //모든방
    private Dictionary<Vector2Int, Room> allRoomsGlobal = new Dictionary<Vector2Int, Room>();
    //모든 구역 및 구역의 방
    private List<List<Room>> roomsByZone = new List<List<Room>>();
    private RoomFactory roomFactory;
    private ZoneGenerator zoneGenerator;
    private InterZoneConnector interZoneConnector;
    private RoomTypeAssigner roomTypeAssigner;
    private System.Random rand = new System.Random();
    private static readonly Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };


    void Start()
    {
        InitializeZoneColors();

        roomFactory = new RoomFactory(roomPrefab, transform);
        zoneGenerator = new ZoneGenerator(roomFactory, allRoomsGlobal, zoneColors,
                                          Size.x, Size.y, roomDensity, rand);
        interZoneConnector = new InterZoneConnector(roomsByZone, 0, connectionsBetweenZones);
        roomTypeAssigner = new RoomTypeAssigner(rand);

        GenerateMapLayout();
        interZoneConnector.ConnectAllZones();
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

    /// <summary>
    /// 메인 방을 기준으로 각 방향에 구역 배치
    /// </summary>
    void GenerateMapLayout()
    {
        allRoomsGlobal.Clear();
        roomsByZone.Clear();
        roomFactory.ClearAllRooms();

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
}
