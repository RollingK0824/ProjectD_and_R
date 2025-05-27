using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgManager : Singleton<RpgManager>
{
    //[SerializeField][Header("탐색 유닛들")] protected 유닛[] or List<유닛> ;
    //[SerializeField][Header("맵 이동시 체력감소 수치")] protected float _Move_ = 10; 좀더 회의 필요할듯1 

    [SerializeField][Header("던전 UI")] GameObject _RPG_UI_Canvas;

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
        Debug.Log("배틀 입장");
    }

    void EnterRewardStage()
    {
        Debug.Log("보상방 입장");
    }
    void EnterEventStage()
    {
        Debug.Log("이벤트 입장");
    }
    void EnterShopStage()
    {
        Debug.Log("상점 입장");
    }

}
