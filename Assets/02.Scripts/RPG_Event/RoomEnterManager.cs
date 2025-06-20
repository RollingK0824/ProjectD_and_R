using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterManager : Singleton<RoomEnterManager>
{
    [SerializeField][Header("던전 UI")] GameObject _RPG_UI_Canvas;

    EventDataLoder eventData;

    protected override async void Awake()
    {
        base.Awake();

        eventData = new EventDataLoder();
        await eventData.LoadAllEventsByLabel();
    }

    /// <summary>
    /// 스테이지 입장
    /// </summary>
    /// <param name="type">방 타입</param>
    public void EnterStage(Room room)
    {
        RoomType type = room.Type;

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
        switch (type)
        {
            case RoomType.Normal:
                Debug.Log("일반");
                break;
            case RoomType.Elite:
                Debug.Log("정예");
                break;
            case RoomType.Boss:
                Debug.Log("보스");
                break;
        }

    }

    void EnterRewardStage()
    {
        Debug.Log("보상방 입장");
    }
    void EnterEventStage()
    {
        Debug.Log("이벤트 입장");
        eventData.ExecuteEvent(eventData.GetRandomWeightedEvent());
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
