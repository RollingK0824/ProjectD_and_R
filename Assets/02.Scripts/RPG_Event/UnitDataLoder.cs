using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UnitDataLoder
{
    string label = "UnitData";

    AsyncOperationHandle<IList<UnitData>> loadHandle;
    public List<UnitData> unitDatas = new List<UnitData>();


    public async Task LoadAllUnitsByLabel()
    {
        loadHandle = Addressables.LoadAssetsAsync<UnitData>(label, null);

        IList<UnitData> result = await loadHandle.Task;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            unitDatas.AddRange(result);
        }
    }

    void OnDestroy()
    {
        if (loadHandle.IsValid())
        {
            Addressables.Release(loadHandle);
        }
    }
}
