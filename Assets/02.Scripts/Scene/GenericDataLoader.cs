using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GenericDataLoader<T> where T : Object
{
    private string label; // Addressables 그룹 레이블

    private AsyncOperationHandle<IList<T>> loadHandle;
    public List<T> LoadedDatas { get; private set; } = new List<T>(); // 로드된 데이터를 저장할 리스트

    /// <summary>
    /// 지정된 레이블을 사용하여 데이터를 로드하는 로더를 생성
    /// </summary>
    public GenericDataLoader(string assetLabel)
    {
        this.label = assetLabel;
    }

    public async Task LoadAllDatasByLabel()
    {
        ReleaseHandle();

        loadHandle = Addressables.LoadAssetsAsync<T>(label, null);

        IList<T> result = await loadHandle.Task;

        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            LoadedDatas.Clear(); 
            LoadedDatas.AddRange(result);
        }

    }

    /// <summary>
    /// Addressables 핸들을 해제하여 메모리에서 로드된 에셋을 언로드합니다.
    /// </summary>
    public void ReleaseHandle()
    {
        if (loadHandle.IsValid())
        {
            Addressables.Release(loadHandle);
        }
    }
}