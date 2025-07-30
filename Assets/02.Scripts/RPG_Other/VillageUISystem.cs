using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VillageUISystem : MonoBehaviour
{
    [Header("연결할 시스템")]
    [SerializeField] GachaSystem gachaSystem;
    [SerializeField] MercenaryGuild mercenaryGuild;
    [SerializeField] ShopSystem shopSystem;

    [Header("UI Panels")]
    public GameObject gachaPanel;
    public GameObject mercenaryGuildPanel;
    public GameObject shopPanel;
    public GameObject partyPanel;

    [Header("NPC")]
    public GameObject npcGroup;

    [Header("용병 고용소")]
    [SerializeField] private List<UnitSlotUI> mercenarySlots;

    [Header("상점 아이템")]
    public Transform shopItemGrid;
    public GameObject itemSlotPrefab;

    [Header("골드 텍스트")]
    [SerializeField] private TextMeshProUGUI shopGoldText;
    [SerializeField] private TextMeshProUGUI gachaGoldText;
    [SerializeField] private TextMeshProUGUI mercenaryGoldText;

    [Header("파티 관리 UI")]
    [SerializeField] private GameObject unitSlotPrefab;
    [SerializeField] private Transform currentPartyPanel;
    [SerializeField] private Transform unlockedUnitsPanel;

    [SerializeField] private Button addMemberButton;
    [SerializeField] private Button removeMemberButton;

    private UnitSlotUI selectedSlot = null;

    private void Awake()
    {
        gachaSystem = GetComponent<GachaSystem>();
        mercenaryGuild = GetComponent<MercenaryGuild>();
        shopSystem = GetComponent<ShopSystem>();
    }
    private void Start()
    {
        CloseAllPanels();
        UpdateAllGoldDisplays();

        if (addMemberButton != null)
        {
            addMemberButton.onClick.AddListener(OnClickAddSelectedMember);
        }
        if (removeMemberButton != null)
        {
            removeMemberButton.onClick.AddListener(OnClickRemoveSelectedMember);
        }
    }

    /// <summary>
    /// 골드량 갱신
    /// </summary>
    public void UpdateAllGoldDisplays()
    {
        if (RpgManager.Instance == null) return;

        string goldString = $"골드: {RpgManager.Instance.inventory.Gold}";

        if (shopGoldText != null) shopGoldText.text = goldString;
        if (gachaGoldText != null) gachaGoldText.text = goldString;
        if (mercenaryGoldText != null) mercenaryGoldText.text = goldString;
    }

    public void OpenGachaPanel()
    {
        CloseAllPanels();
        gachaPanel.SetActive(true);
        npcGroup.SetActive(false);
    }
    public void OpenPartyPanel()
    {
        CloseAllPanels();
        partyPanel.SetActive(true);
        npcGroup.SetActive(false);
        PopulatePartyUI();
    }

    public void OpenMercenaryGuildPanel()
    {
        CloseAllPanels();
        mercenaryGuildPanel.SetActive(true);
        npcGroup.SetActive(false);

        mercenaryGuild.RefreshHirableUnits();
        PopulateMercenaryGuildUI();
    }

    public void OpenShopPanel()
    {
        CloseAllPanels();
        shopPanel.SetActive(true);
        npcGroup.SetActive(false);

        shopSystem.RefreshShopInventory();
        PopulateShopUI();
    }

    public void GoToDungeon()
    {
        GameManager.Instance.GoToScene("04.Dungeon");
    }

    public void OnClick_SinglePull()
    {
        gachaSystem.PerformSinglePull();
        UpdateAllGoldDisplays();
    }

    public void OnClick_TenPull()
    {
        gachaSystem.PerformTenPull();
        UpdateAllGoldDisplays();
    }

    public void OnClick_HireUnit(UnitSlotUI clickedSlot)
    {
        bool success = mercenaryGuild.HireUnit(clickedSlot.unitData);

        if (success)
        {
            UpdateAllGoldDisplays();
            clickedSlot.MarkAsHired();
        }

    }

    public void OnClick_BuyItem(ShopItemSlotUI clickedSlot)
    {
        ItemData itemToBuy = clickedSlot.currentItem;

        bool success = shopSystem.BuyItem(itemToBuy);
        if (success)
        {
            UpdateAllGoldDisplays();
            clickedSlot.MarkAsSoldOut();
        }

    }

    public void CloseAllPanels()
    {
        gachaPanel.SetActive(false);
        mercenaryGuildPanel.SetActive(false);
        shopPanel.SetActive(false);
        partyPanel.SetActive(false);

        npcGroup.SetActive(true);
    }

    /// <summary>
    /// 상점 ui 
    /// </summary>
    private void PopulateShopUI()
    {
        foreach (Transform child in shopItemGrid)
        {
            Destroy(child.gameObject);
        }

        List<ItemData> items = shopSystem.GetItemsForSale();

        foreach (var item in items)
        {
            GameObject slotGO = Instantiate(itemSlotPrefab, shopItemGrid);
            ShopItemSlotUI slotUI = slotGO.GetComponent<ShopItemSlotUI>();

            if (slotUI != null)
            {
                slotUI.Setup(item, OnClick_BuyItem);
            }
        }
    }

    /// <summary>
    /// 용병소 ui
    /// </summary>
    private void PopulateMercenaryGuildUI()
    {
        List<CharacterData> hirableUnits = mercenaryGuild.UnitsForHire;

        for (int i = 0; i < mercenarySlots.Count; i++)
        {
            if (i < hirableUnits.Count)
            {
                mercenarySlots[i].Setup(hirableUnits[i], (slot) => OnClick_HireUnit(slot));
                mercenarySlots[i].gameObject.SetActive(true);
            }
            else
            {
                mercenarySlots[i].gameObject.SetActive(false);
            }
        }
    }


    /// <summary>
    /// 파티 관리 UI
    /// </summary>
    private void PopulatePartyUI()
    {
        foreach (Transform child in currentPartyPanel) Destroy(child.gameObject);
        foreach (Transform child in unlockedUnitsPanel) Destroy(child.gameObject);

        List<CharacterData> party = PartyManager.Instance.currentParty;
        foreach (var unit in party)
        {
            GameObject slotGO = Instantiate(unitSlotPrefab, currentPartyPanel);
            UnitSlotUI slotUI = slotGO.GetComponent<UnitSlotUI>();
            slotUI.isPartyMemberSlot = true;
            slotUI.Setup(unit, OnPartySlotClicked);
        }

        List<CharacterData> unlocked = PartyManager.Instance.GetUnlockedUnits();
        foreach (var unit in unlocked)
        {
            if (party.Contains(unit)) continue;
            GameObject slotGO = Instantiate(unitSlotPrefab, unlockedUnitsPanel);
            UnitSlotUI slotUI = slotGO.GetComponent<UnitSlotUI>();
            slotUI.isPartyMemberSlot = false;
            slotUI.Setup(unit, OnPartySlotClicked);
        }

        selectedSlot = null;
        SetButtonState(false, false);
    }

    /// <summary>
    /// 파티 관리 창의 유닛 슬롯 클릭
    /// </summary>
    private void OnPartySlotClicked(UnitSlotUI clickedSlot)
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
            PopulatePartyUI();
        }
    }

    /// <summary>
    /// 파티에 추가
    /// </summary>
    private void OnClickAddSelectedMember()
    {
        if (selectedSlot != null && !selectedSlot.isPartyMemberSlot)
        {
            PartyManager.Instance.AddPartyMember(selectedSlot.unitData.id);
            PopulatePartyUI();
        }
    }

    /// <summary>
    /// 맴버 제외
    /// </summary>
    private void OnClickRemoveSelectedMember()
    {
        if (selectedSlot != null && selectedSlot.isPartyMemberSlot)
        {
            PartyManager.Instance.RemovePartyMember(selectedSlot.unitData.id);
            PopulatePartyUI(); 
        }
    }

    /// <summary>
    /// 추가/삭제 버튼의 활성화 상태를 한 번에 관리
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