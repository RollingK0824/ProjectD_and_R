using System.Collections.Generic;
using UnityEngine;

public class RpgManager : Singleton<RpgManager>
{
    [SerializeField][Header("RPG턴 최대 턴")] public int Max_Turn;
    [SerializeField][Header("남은 턴")] public int LeftTurn;
    
    [SerializeField][Header("맵 생성 관련")] public GameObject roomPrefab;
    [SerializeField] public string MapParent = "Map";
    [SerializeField] int numberOfZones;
    [SerializeField] Vector2Int Size;
    [Range(0.1f, 1f)][SerializeField] float roomDensity = 0.4f;
    [SerializeField] int gapBetweenZones = 0;
    [SerializeField] int connectionsBetweenZones = 1;
    [SerializeField] public bool isCreateMap = false;

    [SerializeField][Header("방 입장 관련")] public RoomEnterSystem RoomEnterSystem;
    [SerializeField][Header("RPG관련 DB")] public RpgDatabase Database;

    [HideInInspector] public Inventory inventory;
    [HideInInspector] public UnitSystem UnitSystem;
    [HideInInspector] public ZoneLayoutSystem zoneLayoutSystem;
    [HideInInspector] public MapBlueprint mapBlueprint;
    protected override void Awake()
    {
        base.Awake();

        inventory = new Inventory();
        RoomEnterSystem = new RoomEnterSystem();
        UnitSystem = new UnitSystem();

        zoneLayoutSystem = new ZoneLayoutSystem(numberOfZones, Size, roomDensity, gapBetweenZones, connectionsBetweenZones);
        Database = new RpgDatabase();
        LeftTurn = Max_Turn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            RoomEnterSystem.battleEnter.endBattle();
        }
    }

    public void UpdateCurrentMapState() 
    {
        MapBlueprint updatedBlueprint = zoneLayoutSystem.CreateBlueprintFromCurrentState();
        mapBlueprint = updatedBlueprint; 
    }

    public void UseTrun(int value)
    {

        LeftTurn -= value;
        if (LeftTurn < 0) LeftTurn = 0;
    }

    public bool CheckEndOfRPG()
    {
        if (LeftTurn > 0) return false;
        return true;
    }

    public void EndRPG()
    {
        GameManager.Instance.GoToScene("UnitTestScene");
    }

    public void StartRPG()
    {
        LeftTurn = Max_Turn;  
    }
}
