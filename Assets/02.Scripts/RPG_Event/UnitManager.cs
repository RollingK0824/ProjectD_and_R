using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UnitManager : Singleton<UnitManager>
{
    //추후 대훈스크립트랑 합쳐서 charatercore가져올수있게 해야함

    [Header("유닛 데이터")]
    public List<UnitData> allUnitDatas;
    public Task InitializationTask { get; private set; }
    UnitDataLoder unitDataLoder = new UnitDataLoder();

    protected override void Awake()
    {
        base.Awake(); 
        InitializationTask = InitializeAsync();
    }

    async Task InitializeAsync()
    {
        await unitDataLoder.LoadAllUnitsByLabel();

        allUnitDatas = unitDataLoder.unitDatas;
    }

    public UnitData GetUnitDataByID(int unitID)
    {
        return allUnitDatas.Find(unit => unit.id == unitID);
    }
}
