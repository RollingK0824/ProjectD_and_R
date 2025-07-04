using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("UI 요소")]
    [SerializeField][Header("상점 패널")] private GameObject shopPanel; 
    [SerializeField][Header("아이템 슬롯 패널")] private Transform itemSlotContainer; 
    [SerializeField][Header("아이템 슬롯 프리팹")] private GameObject itemSlotPrefab;
    [SerializeField][Header("플레이어 골드 표기")] private TextMeshProUGUI playerGoldText; 

    [Header("상점 시스템")]
    public ShopSystem shopSystem; 
    private List<GameObject> spawnedSlots = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) OpenShop();
    }
    /// <summary>
    /// 상점 UI를 엽니다.
    /// </summary>
    public void OpenShop()
    {
        shopPanel.SetActive(true);
        // 상점을 열 때마다 새로운 아이템 목록을 생성합니다.
        shopSystem.GenerateShopItems();
        UpdateShopUI();
        UpdatePlayerGoldUI();
    }

    /// <summary>
    /// 상점 UI를 닫습니다.
    /// </summary>
    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    /// <summary>
    /// 상점 UI를 현재 판매 목록에 맞게 업데이트합니다.
    /// </summary>
    private void UpdateShopUI()
    {
        // 기존에 생성된 슬롯들을 모두 제거합니다.
        foreach (GameObject slot in spawnedSlots)
        {
            Destroy(slot);
        }
        spawnedSlots.Clear();

        // 판매 목록에 있는 각 아이템에 대해 슬롯을 생성합니다.
        if (shopSystem.merchandise == null || shopSystem.merchandise.Count == 0) return;

        foreach (ItemData item in shopSystem.merchandise)
        {
            GameObject slotGO = Instantiate(itemSlotPrefab, itemSlotContainer);
            ShopItemSlotUI slotUI = slotGO.GetComponent<ShopItemSlotUI>();
            if (slotUI != null)
            {
                slotUI.Setup(item, this);
                spawnedSlots.Add(slotGO);
            }
        }
    }

    /// <summary>
    /// 플레이어의 골드 UI를 업데이트합니다.
    /// </summary>
    public void UpdatePlayerGoldUI()
    {
        if (playerGoldText != null)
        {
            // RpgManager에서 플레이어 인벤토리의 골드 정보를 가져옵니다.
            playerGoldText.text = $"소지 골드: {RpgManager.Instance.inventory.Gold} G";
        }
    }

    /// <summary>
    /// 아이템 구매를 시도하고 결과를 UI에 반영합니다.
    /// </summary>
    public void AttemptToBuyItem(ItemData item)
    {
        bool success = shopSystem.CheckBuy(item);
        if (success)
        {
            shopSystem.Buy(item);
            // 구매에 성공하면 UI를 업데이트합니다.
            UpdatePlayerGoldUI();
            // 선택: 구매한 아이템을 상점 목록에서 제거하거나, 품절로 표시할 수 있습니다.
            // 여기서는 간단하게 목록을 새로고침합니다.
            shopSystem.merchandise.Remove(item);
            UpdateShopUI();
        }
    }
}
