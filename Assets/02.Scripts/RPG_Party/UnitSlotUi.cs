using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; // Action 사용

public class UnitSlotUI : MonoBehaviour
{
    // 인스펙터에서 연결
    public TextMeshProUGUI nameText;
    public Button actionButton;
    public Image backgroundImage; // 선택 시 색상을 바꿀 배경 이미지

    // 슬롯이 가진 정보
    public EnemyCharacterData unitData { get; private set; }
    public bool isPartyMemberSlot { get; set; } // 이 슬롯이 파티 멤버 슬롯인지, 해금 유닛 슬롯인지 구분

    private Color originalColor;

    private void Awake()
    {
        if (backgroundImage != null)
        {
            originalColor = backgroundImage.color;
        }
    }

    // 이 슬롯에 특정 유닛의 정보를 설정하는 함수
    public void Setup(EnemyCharacterData unit, Action<UnitSlotUI> onClickAction)
    {
        this.unitData = unit;
        nameText.text = unit.Name;

        actionButton.onClick.RemoveAllListeners();
        // 버튼이 클릭되면, 자기 자신의 UnitSlotUI 컴포넌트를 인자로 넘겨줍니다.
        actionButton.onClick.AddListener(() => onClickAction(this));
    }

    // 선택 상태를 시각적으로 표시하는 함수
    public void SetSelected(bool isSelected)
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = isSelected ? Color.yellow : originalColor;
        }
    }
}