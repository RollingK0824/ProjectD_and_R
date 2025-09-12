using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class ShopSystem
{
    [Header("등급")] public ShopGrade grade = ShopGrade.Nomal;
    [Header("판매 아이템 개수")] public int itemCount = 10;
    [Header("감가율")][Range(0, 100)] public float depreciation = 30;

    [HideInInspector] public List<ItemData> merchandise = new List<ItemData>();
    ShopGradeTable gradeTable;

    public ShopSystem()
    {
        gradeTable = new ShopGradeTable();
    }

    /// <summary>
    /// 상점 판매 아이템 추가
    /// </summary>
    public void GenerateShopItems()
    {
        merchandise.Clear();

        List<ItemData> allItems = RpgManager.Instance.Database.Items;
        List<ItemData> itemCandidates = new List<ItemData>(allItems.Where(item => item.Grade != ItemGrade.Mythic));
        Dictionary<ItemGrade, float> currentProbabilities = gradeTable.probabilityTable[grade];
        int itemsToGenerate = Mathf.Min(itemCount, itemCandidates.Count);

        for (int i = 0; i < itemsToGenerate; i++)
        {
            ItemGrade selectedGrade = GetRandomGrade(currentProbabilities);
            List<ItemData> itemsOfSelectedGrade = itemCandidates.Where(item => item.Grade == selectedGrade).ToList();

            int attempts = 0;
            while (itemsOfSelectedGrade.Count == 0 && attempts < 10)
            {
                selectedGrade = GetRandomGrade(currentProbabilities);
                itemsOfSelectedGrade = itemCandidates.Where(item => item.Grade == selectedGrade).ToList();
                attempts++;
            }

            if (itemsOfSelectedGrade.Count > 0)
            {
                ItemData randomItem = itemsOfSelectedGrade[Random.Range(0, itemsOfSelectedGrade.Count)];
                merchandise.Add(randomItem);
                itemCandidates.Remove(randomItem);
            }
        }
    }

    /// <summary>
    /// 상점 등급에서 나올수 있는 랜덤 아이템 반환
    /// </summary>
    private ItemGrade GetRandomGrade(Dictionary<ItemGrade, float> probabilities)
    {
        float randomPoint = Random.value;
        float cumulative = 0.0f;

        foreach (var pair in probabilities)
        {
            cumulative += pair.Value;
            if (randomPoint < cumulative)
            {
                return pair.Key;
            }
        }
        return probabilities.Keys.Last();
    }

    /// <summary>
    /// 아이템 구매
    /// </summary>
    public void Buy(ItemData item)
    {
        Inventory inventory = RpgManager.Instance.inventory;
        inventory.SpendGold(item.Price); ;
        inventory.AddItem(item);
    }

    /// <summary>
    /// 아이템 판매
    /// </summary>
    public void Sell(ItemData item)
    {
        Inventory inventory = RpgManager.Instance.inventory;
        if (!inventory.InventoryItems.ContainsKey(item)) return;

        inventory.RemoveItem(item);
        inventory.AddGold(Mathf.RoundToInt(item.Price * (1f - (depreciation / 100f))));
    }

    public bool CheckBuy(ItemData item)
    {
        Inventory inventory = RpgManager.Instance.inventory;

        if (inventory.Gold < item.Price) return false;
        return true;
    }
}
