using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class UnitSlotUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI gradeText; 
    public Button actionButton;
    public Image backgroundImage;
    public Image gradeColorImage;

    private TextMeshProUGUI actionButtonText;

    public CharacterData unitData { get; private set; }
    public bool isPartyMemberSlot { get; set; }

    private Color originalColor;

    private static readonly Dictionary<UnitGrade, Color> gradeColors = new Dictionary<UnitGrade, Color>
    {
        { UnitGrade.Normal, Color.white },
        { UnitGrade.Rare, new Color(0.3f, 0.7f, 1f) },   // 파란색
        { UnitGrade.Epic, new Color(0.8f, 0.4f, 1f) },   // 보라색
        { UnitGrade.Legendary, new Color(1f, 0.8f, 0.2f) } // 주황색/금색
    };

    private void Awake()
    {
        if (backgroundImage != null)
        {
            originalColor = backgroundImage.color;
        }
        if(actionButton != null)
        {
            actionButtonText = actionButton.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void Setup(CharacterData unit, Action<UnitSlotUI> onClickAction)
    {
        this.unitData = unit;
        nameText.text = unit.name;

        if (gradeText != null)
        {
            gradeText.text = unit.Grade.ToString();
            gradeText.color = gradeColors[unit.Grade];
        }

        if (gradeColorImage != null)
        {
            gradeColorImage.color = gradeColors[unit.Grade];
        }

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() => onClickAction(this));
    }

    public void SetSelected(bool isSelected)
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = isSelected ? Color.yellow : originalColor;
        }
    }


    /// <summary>
    /// 슬롯을 고용 완료 상태로 변경하는 함수
    /// </summary>
    public void MarkAsHired()
    {
        actionButton.interactable = false; 
        if (actionButtonText != null)
        {
            actionButtonText.text = "고용 완료";
        }
    }
}