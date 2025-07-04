using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemSlotUI : MonoBehaviour
{
    [Header("슬롯 UI 요소")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Image itemGradeBorder;

    private ItemData currentItem;
    private ShopUI shopUIController;

    // 아이템 등급별 색상
    private readonly Dictionary<ItemGrade, Color> gradeColors = new Dictionary<ItemGrade, Color>
    {
        { ItemGrade.Common, Color.white },
        { ItemGrade.Uncommon, Color.green },
        { ItemGrade.Rare, Color.blue },
        { ItemGrade.Unique, new Color(0.5f, 0f, 1f) }, // Purple
        { ItemGrade.Epic, Color.magenta },
        { ItemGrade.Legendary, Color.yellow },
        { ItemGrade.Mythic, Color.red }
    };

    /// <summary>
    /// 슬롯의 정보를 설정합니다.
    /// </summary>
    /// <param name="item">표시할 아이템 데이터</param>
    /// <param name="shopUI">상위 ShopUI 컨트롤러</param>
    public void Setup(ItemData item, ShopUI shopUI)
    {
        currentItem = item;
        shopUIController = shopUI;

        // UI 텍스트 설정
        itemNameText.text = currentItem.Item_Name;
        itemDescriptionText.text = currentItem.Item_Description;
        itemPriceText.text = $"{currentItem.Price} G";

        // 아이템 등급에 따라 테두리 색상 변경
        if (itemGradeBorder != null && gradeColors.ContainsKey(currentItem.Grade))
        {
            itemGradeBorder.color = gradeColors[currentItem.Grade];
        }

        // 구매 버튼 리스너 설정
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    /// <summary>
    /// 구매 버튼 클릭 시 호출될 함수입니다.
    /// </summary>
    private void OnBuyButtonClicked()
    {
        if (shopUIController != null)
        {
            shopUIController.AttemptToBuyItem(currentItem);
        }
    }
}
