using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopSystem :MonoBehaviour
{
    [Header("상점 설정")]
    [Tooltip("상점에 진열할 최대 아이템 개수")]
    [SerializeField] private int maxSaleItemCount = 10;
    [Tooltip("판매 시 구매 가격의 배율")]
    [SerializeField] private float sellPriceMultiplier = 0.5f;

    private List<ItemData> itemsForSale = new List<ItemData>();
    private Inventory playerInventory;

    void Awake()
    {
        if (RpgManager.Instance != null)
        {
            playerInventory = RpgManager.Instance.inventory;
        }
    }

    /// <summary>
    /// 아이템 목록 설정
    /// </summary>
    public void RefreshShopInventory()
    {
        itemsForSale.Clear();

        List<ItemData> allItemsInDB = RpgManager.Instance.Database.Items;

        if (allItemsInDB == null || allItemsInDB.Count == 0)
            return;

        var randomItems = allItemsInDB.OrderBy(item => Random.value).Take(maxSaleItemCount);

        itemsForSale.AddRange(randomItems);
    }

    public List<ItemData> GetItemsForSale()
    {
        return itemsForSale;
    }

    /// <summary>
    /// 아이템 구매
    /// </summary>
    public bool BuyItem(ItemData item)
    {
        if (item == null || playerInventory == null) return false;

        if (playerInventory.Gold < item.Price)
            return false;

        playerInventory.SpendGold(item.Price);

        playerInventory.AddItem(item);
        return true;
    }

    /// <summary>
    /// 아이템 판매 
    /// </summary>
    public void SellItem(ItemData item)
    {
        if (item == null || playerInventory == null) return;

        playerInventory.RemoveItem(item);

        int sellPrice = Mathf.FloorToInt(item.Price * sellPriceMultiplier);
        playerInventory.AddGold(sellPrice);

    }
}
