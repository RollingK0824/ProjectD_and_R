using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    ItemDataLoder ItemDataLoder;

    public List<ItemData> All_Item;

    [Header("등급")] public ShopGrade grade = ShopGrade.Nomal;
    [Header("감가율")][Range(0, 100)] public float depreciation = 30;
    private async void Start()
    {
        ItemDataLoder = new ItemDataLoder();
        await ItemDataLoder.LoadAllItemsByLabel();

        All_Item = ItemDataLoder.ItemDatas;
    }

    /// <summary>
    /// 아이템 구매
    /// </summary>
    public void BuyItem(ItemData item)
    {
        Inventory inventory = RpgManager.Instance.inventory;

        if (inventory.Gold < item.Price) return;

        inventory.SpendGold(item.Price);;
        inventory.AddItem(item);
    }

    /// <summary>
    /// 아이템 판매
    /// </summary>
    public void SellItem(ItemData item)
    {
        Inventory inventory = RpgManager.Instance.inventory;
        if (!inventory.InventoryItems.ContainsKey(item)) return;

        inventory.RemoveItem(item);
        inventory.AddGold(Mathf.RoundToInt(item.Price * (1f - (depreciation / 100f))));
    }
}
