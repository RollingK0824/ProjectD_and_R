using System.Collections.Generic;
using UnityEngine;


public class RpgManager : Singleton<RpgManager>
{
    [SerializeField] public Inventory inventory;

    private void Start()
    {
        inventory = new Inventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            if (inventory == null || inventory.InventoryItems.Count <= 0) return;

            foreach (KeyValuePair<ItemData, int> entry in inventory.InventoryItems)
            {
                Debug.Log($"아이템 : {entry.Key.Item_Name}, 개수 : {entry.Value}");
            }
        }
    }
}
