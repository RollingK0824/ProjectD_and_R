using UnityEngine;

[System.Serializable]
public class RoomEnterSystem
{
    [SerializeField][Header("던전 UI")] GameObject _RPG_UI_Canvas;
    public EventEnter eventEnter = new EventEnter();
    public BattleEnter battleEnter = new BattleEnter();

    /// <summary>
    /// 스테이지 입장
    /// </summary>
    /// <param name="type">방 타입</param>
    public void EnterStage(Room room)
    {
        if (room.visite) return;

        RoomType type = room.Type;
        room.visite = true;

        RpgManager.Instance.UpdateCurrentMapState();

        switch (type)
        {
            case RoomType.Normal:
            case RoomType.Elite:
            case RoomType.Boss:
                EnterBattleStage(type);
                break;
            case RoomType.Event:
                EnterEventStage();
                break;
            case RoomType.Shop:
                EnterShopStage();
                break;
            case RoomType.Reward:
                EnterRewardStage();
                break;
            case RoomType.Door:
                EnterDoor();
                break;
            default:
                Debug.Log("방 정보가 없습니다.");
                break;
        }
    }
    /// <summary>
    /// 전투 스테이지 입장
    /// </summary>
    void EnterBattleStage(RoomType type)
    {
        //switch (type)
        //{
        //    case RoomType.Normal:
        //        Debug.Log("일반");
        //        break;
        //    case RoomType.Elite:
        //        Debug.Log("정예");
        //        break;
        //    case RoomType.Boss:
        //        Debug.Log("보스");
        //        break;
        //}
        GameManager.Instance.GoToScene("DungeonBattle");
    }

    void EnterRewardStage()
    {
        Debug.Log("보상방 입장");
    }
    void EnterEventStage()
    {
        Debug.Log("이벤트 입장");
        eventEnter.ExecuteEvent(eventEnter.GetRandomWeightedEvent());
    }
    void EnterShopStage()
    {
        Debug.Log("상점 입장");
    }
    void EnterDoor()
    {
        Debug.Log("문 입장");
    }
}
