using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ItemDataLoder
{
    string label = "ItemData";

    AsyncOperationHandle<IList<ItemData>> loadHandle;
    public List<ItemData> ItemDatas = new List<ItemData>();

    public async Task LoadAllItemsByLabel()
    {
        loadHandle = Addressables.LoadAssetsAsync<ItemData>(label, null);

        IList<ItemData> result = await loadHandle.Task;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            ItemDatas.AddRange(result);
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
