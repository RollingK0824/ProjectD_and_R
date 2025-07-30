using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopItemSlotUI : MonoBehaviour
{
    [Header("슬롯 UI 요소")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Image itemGradeBorder;

    public ItemData currentItem { get; private set; }
    private TextMeshProUGUI buyButtonText;

    private void Awake()
    {
        buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
    }

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
    public void Setup(ItemData item, Action<ShopItemSlotUI> onBuyAction)
    {
        currentItem = item;

        // UI 텍스트 설정
        itemNameText.text = currentItem.Item_Name;
        itemPriceText.text = $"{currentItem.Price} G";

        // 아이템 등급에 따라 테두리 색상 변경
        if (itemGradeBorder != null && gradeColors.ContainsKey(currentItem.Grade))
        {
            itemGradeBorder.color = gradeColors[currentItem.Grade];
        }

        // 구매 버튼 리스너 설정
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => onBuyAction(this));
    }

    /// <summary>
    /// 물품 구매 후 상태변화
    /// </summary>
    public void MarkAsSoldOut()
    {
        buyButton.interactable = false; 
        if (buyButtonText != null)
        {
            buyButtonText.text = "품절";
        }
    }

}
