using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventEnter
{
    /// <summary>
    /// 가중치로 이벤트 반환 함수
    /// </summary>
    public EventData GetRandomWeightedEvent()
    {
        List<EventData> events = RpgManager.Instance.Database.events;

        if (events == null || events.Count == 0)
            return null;

        float totalWeight = 0;

        foreach (EventData data in events)
        {
            totalWeight += data.weight;
        }

        if (totalWeight <= 0)
            return null;


        float randomValue = Random.Range(0f, totalWeight);
        float currentWeightSum = 0f;

        foreach (EventData data in events)
        {
            currentWeightSum += data.weight;
            if (randomValue <= currentWeightSum)
            {
                Debug.Log($"선택된 이벤트: {data.Event_Name} (가중치: {data.weight})");
                return data;
            }
        }

        return null;
    }

    /// <summary>
    /// 이벤트 발생 함수
    /// </summary>
    public void ExecuteEvent(EventData data)
    {
        if (data == null)
            return;

        Debug.Log("이벤트 진입");
        Debug.Log($"이벤트 : {data.Event_Name}");
        Debug.Log($"설명 : {data.Event_Description}");

        ApplyRewards(data.rewardData);
    }

    private void ApplyRewards(RewardData rewards)
    {
        if (rewards == null) return;

        if (rewards.itemRewards != null && rewards.itemRewards.Count > 0)
        {
            foreach (ItemData itemReward in rewards.itemRewards)
            {
                if (itemReward.Item_Description != null)
                {
                    RpgManager.Instance.inventory.AddItem(itemReward);

                    Debug.Log("획득");
                    Debug.Log($"아이템 : {itemReward.Item_Name}");
                    Debug.Log($"설명 : {itemReward.Item_Description}");
                }
            }
        }

        if (rewards.Gold > 0)
        {
            RpgManager.Instance.inventory.AddGold(rewards.Gold);
            Debug.Log($"골드 획득: {rewards.Gold}");
        }

    }
}
