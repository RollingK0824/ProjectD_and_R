using UnityEngine;

public class VillageNpc : MonoBehaviour,ITouchble
{
    public enum NpcType { Gacha, MercenaryGuild, DungeonEntrance, Shop, Party }
    public NpcType type;

    [SerializeField] private VillageUISystem uiManager;

    public void OnTouch()
    {
        if (uiManager == null) return;

        switch (type)
        {
            case NpcType.Gacha:
                uiManager.OpenGachaPanel();
                break;
            case NpcType.MercenaryGuild:
                uiManager.OpenMercenaryGuildPanel();
                break;
            case NpcType.DungeonEntrance:
                uiManager.GoToDungeon();
                break;
            case NpcType.Shop:
                uiManager.OpenShopPanel();
                break;
            case NpcType.Party:
                uiManager.OpenPartyPanel();
                break;
        }
    }

    public void OnEmptyTouch() { }
    public void OnOtherTouch() { }
}
