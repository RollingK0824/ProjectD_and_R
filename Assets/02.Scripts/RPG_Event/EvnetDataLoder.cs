using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EventDataLoder
{
    string label = "EventData";

    AsyncOperationHandle<IList<EventData>> loadHandle;
    public List<EventData> eventDatas = new List<EventData>();


    public async Task LoadAllEventsByLabel()
    {
        loadHandle = Addressables.LoadAssetsAsync<EventData>(label, null);

        IList<EventData> result = await loadHandle.Task;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            eventDatas.AddRange(result);
        }
    }

    void OnDestroy()
    {
        if (loadHandle.IsValid())
        {
            Addressables.Release(loadHandle);
        }
    }

    /// <summary>
    /// 가중치로 이벤트 반환 함수
    /// </summary>
    public EventData GetRandomWeightedEvent()
    {
        if (eventDatas == null || eventDatas.Count == 0)
            return null;

        float totalWeight = 0;

        foreach (EventData data in eventDatas)
        {
            totalWeight += data.weight;
        }

        if (totalWeight <= 0)
            return null;


        float randomValue = Random.Range(0f, totalWeight);
        float currentWeightSum = 0f;

        foreach (EventData data in eventDatas)
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
                    Debug.Log($"공격력 : {itemReward.Attack}");
                    Debug.Log($"방어력 : {itemReward.Defense}");
                    Debug.Log($"공격속도 : {itemReward.AttackSpeed}");
                    Debug.Log($"체력 : {itemReward.Health}");
                    Debug.Log($"이동속도 : {itemReward.Speed}");
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
