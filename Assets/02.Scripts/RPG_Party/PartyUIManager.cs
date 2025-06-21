using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PartyUIManager : MonoBehaviour
{
    public GameObject unitSlotPrefab;
    public Transform currentPartyPanel;
    public Transform unlockedUnitsPanel;

    public Button addMemberButton;
    public Button removeMemberButton;

    private UnitSlotUI selectedSlot = null;

    void Start()
    {
        if (addMemberButton != null)
        {
            addMemberButton.interactable = false;
            addMemberButton.onClick.AddListener(OnClickAddSelectedMember);
        }
        if (removeMemberButton != null)
        {
            removeMemberButton.interactable = false;
            removeMemberButton.onClick.AddListener(OnClickRemoveSelectedMember);
        }

        Invoke("UpdateAllUI", 0.1f);
    }

    public void UpdateAllUI()
    {
        foreach (Transform child in currentPartyPanel) Destroy(child.gameObject);
        foreach (Transform child in unlockedUnitsPanel) Destroy(child.gameObject);

        List<UnitData> party = PartyManager.Instance.currentParty;
        foreach (var unit in party)
        {
            GameObject slotGO = Instantiate(unitSlotPrefab, currentPartyPanel);
            slotGO.name = unit.Name;
            UnitSlotUI slotUI = slotGO.GetComponent<UnitSlotUI>();
            slotUI.isPartyMemberSlot = true;
            slotUI.Setup(unit, OnSlotClicked);
        }

        List<UnitData> unlocked = PartyManager.Instance.GetUnlockedUnits();
        foreach (var unit in unlocked)
        {
            if (party.Contains(unit)) continue;
            GameObject slotGO = Instantiate(unitSlotPrefab, unlockedUnitsPanel);
            slotGO.name = unit.Name;
            UnitSlotUI slotUI = slotGO.GetComponent<UnitSlotUI>();
            slotUI.isPartyMemberSlot = false;
            slotUI.Setup(unit, OnSlotClicked);
        }

        selectedSlot = null;

        SetButtonState(false, false);
    }

    private void OnSlotClicked(UnitSlotUI clickedSlot)
    {
        if (selectedSlot == null)
        {
            selectedSlot = clickedSlot;
            selectedSlot.SetSelected(true);

            SetButtonState(isAdd: !selectedSlot.isPartyMemberSlot, isRemove: selectedSlot.isPartyMemberSlot);
        }
        else if (selectedSlot == clickedSlot)
        {
            selectedSlot.SetSelected(false);
            selectedSlot = null;

            SetButtonState(false, false);
        }
        else
        {
            if (selectedSlot.isPartyMemberSlot && clickedSlot.isPartyMemberSlot)
            {
                int indexA = PartyManager.Instance.currentParty.IndexOf(selectedSlot.unitData);
                int indexB = PartyManager.Instance.currentParty.IndexOf(clickedSlot.unitData);
                PartyManager.Instance.SwapPartyMembers(indexA, indexB);
            }
            else if (selectedSlot.isPartyMemberSlot && !clickedSlot.isPartyMemberSlot)
            {
                PartyManager.Instance.ReplacePartyMember(selectedSlot.unitData.id, clickedSlot.unitData.id);
            }
            else if (!selectedSlot.isPartyMemberSlot && clickedSlot.isPartyMemberSlot)
            {
                PartyManager.Instance.ReplacePartyMember(clickedSlot.unitData.id, selectedSlot.unitData.id);
            }

            UpdateAllUI();
        }
    }

    /// <summary>
    /// '파티에 추가' 버튼을 클릭했을 때 실행되는 함수
    /// </summary>
    private void OnClickAddSelectedMember()
    {
        if (selectedSlot != null && !selectedSlot.isPartyMemberSlot)
        {
            PartyManager.Instance.AddPartyMember(selectedSlot.unitData.id);
            UpdateAllUI();
        }
    }

    /// <summary>
    /// '멤버 제외' 버튼을 클릭했을 때 실행되는 함수
    /// </summary>
    private void OnClickRemoveSelectedMember()
    {
        if (selectedSlot != null && selectedSlot.isPartyMemberSlot)
        {
            PartyManager.Instance.RemovePartyMember(selectedSlot.unitData.id);
            UpdateAllUI();
        }
    }

    /// <summary>
    /// 추가/삭제 버튼의 활성화 상태를 한 번에 관리하는 함수
    /// </summary>
    private void SetButtonState(bool isAdd, bool isRemove)
    {
        if (addMemberButton != null)
        {
            addMemberButton.interactable = isAdd;
        }
        if (removeMemberButton != null)
        {
            removeMemberButton.interactable = isRemove;
        }
    }
}