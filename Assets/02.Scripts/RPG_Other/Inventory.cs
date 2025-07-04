using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    int _Gold = 0;
    public int Gold
    {
        get { return _Gold; }
        set { _Gold = value; }
    }

    private Dictionary<ItemData, int> _InventoryItems = new Dictionary<ItemData, int>();
    public Dictionary<ItemData, int> InventoryItems
    {
        get { return _InventoryItems; }
        set { _InventoryItems = value; }
    }

    /// <summary>
    /// 골드 추가
    /// </summary>
    public void AddGold(int amount)
    {
        if (amount < 0)
            return;
        _Gold += amount;
    }

    /// <summary>
    /// 골드 사용
    /// </summary>
    public void SpendGold(int amount)
    {
        if (amount < 0 || _Gold < amount)
            return;
        if (_Gold >= amount)
        {
            _Gold -= amount;
        }
    }


    /// <summary>
    /// 인벤토리에 아이템을 추가
    /// </summary>
    public
        void AddItem(ItemData itemData)
    {
        if (itemData == null)
            return;

        if (_InventoryItems.ContainsKey(itemData))
            _InventoryItems[itemData]++;

        else
            _InventoryItems.Add(itemData, 1);
    }

    /// <summary>
    /// 인벤토리에서 아이템을 제거
    /// </summary>
    public void RemoveItem(ItemData itemData)
    {
        if (itemData == null || !_InventoryItems.ContainsKey(itemData))
            return;

        _InventoryItems[itemData]--;

        if (_InventoryItems[itemData] == 0)
        {
            _InventoryItems.Remove(itemData);
        }

    }

    /// <summary>
    /// 인벤토리에서 아이템 확인
    /// </summary>
    /// 
    public bool GetItemQuantity(ItemData itemData)
    {
        if (itemData == null)
            return false;

        return _InventoryItems.ContainsKey(itemData);
    }

    /// <summary>
    /// 지정된 타입의 아이템들만 필터링하여 반환
    /// </summary>
    public IEnumerable<KeyValuePair<ItemData, int>> GetItemsByType(ItemType type)
    {
        return _InventoryItems.Where(pair => pair.Key.Type == type);
    }
}
