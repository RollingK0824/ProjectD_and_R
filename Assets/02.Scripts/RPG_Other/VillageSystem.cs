using UnityEngine;

public class VillageSystem : MonoBehaviour
{
    [SerializeField][Header("상점 시스템")] public ShopSystem ShopSystem;
    RpgDatabase RPG_DB;

    private void Awake()
    {
        ShopSystem = new ShopSystem();

        if(RpgManager.Instance != null ) 
            RPG_DB = RpgManager.Instance.Database;
    }

    public void EnterShop()
    {

    }

    public void EnterTavern()
    {

    }

    public void EnterDungeon()
    {
        GameManager.Instance.GoToScene("RandomMapGenerator");
    }

    public void ExitPlace()
    {

    }
}
