using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgManager : Singleton<RpgManager>
{
    //[SerializeField][Header("Ž�� ���ֵ�")] protected ����[] or List<����> ;
    //[SerializeField][Header("�� �̵��� ü�°��� ��ġ")] protected float _Move_ = 10; ���� ȸ�� �ʿ��ҵ�1 

    [SerializeField][Header("���� UI")] GameObject _RPG_UI_Canvas;

    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="type">�� Ÿ��</param>
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
                Debug.Log("�� ������ �����ϴ�.");
                break;
        }
    }
    /// <summary>
    /// ���� �������� ����
    /// </summary>
    void EnterBattleStage(RoomType type)
    {
        Debug.Log("��Ʋ ����");
    }

    void EnterRewardStage()
    {
        Debug.Log("����� ����");
    }
    void EnterEventStage()
    {
        Debug.Log("�̺�Ʈ ����");
    }
    void EnterShopStage()
    {
        Debug.Log("���� ����");
    }

}
