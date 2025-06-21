using UnityEngine;

[CreateAssetMenu(fileName = "NewEventData", menuName = "ScriptableObjects/Events")]
public class EventData : ScriptableObject
{
    [Header("아이디")] public int Event_ID;
    [Header("이름")] public string Event_Name;

    [TextArea][Header("설명")] public string Event_Description;

    [Header("확률")]public float weight;

    [Header("보상")] public RewardData rewardData;
}
