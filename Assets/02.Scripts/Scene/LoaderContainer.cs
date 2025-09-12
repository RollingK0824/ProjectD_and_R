using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

[System.Serializable]
public class LoaderContainer
{
    readonly Dictionary<string,Task> _loadingTasks = new Dictionary<string, Task>();

    public GenericDataLoader<EventData> eventDataLoader;
    public GenericDataLoader<ItemData> itemDataLoader;
    public GenericDataLoader<RoomEnemySpawnData> roomEnemySpawnDataLoader;
    public GenericDataLoader<EnemyCharacterData> unitDataLoader;

    public bool LoadComplete = false;
    public LoaderContainer()
    {
        eventDataLoader = new GenericDataLoader<EventData>("EventData");
        itemDataLoader = new GenericDataLoader<ItemData>("ItemData");
        roomEnemySpawnDataLoader = new GenericDataLoader<RoomEnemySpawnData>("RoomEnemySpawnData");
        unitDataLoader = new GenericDataLoader<EnemyCharacterData>("UnitData");
    }

    //혹여나 씬마다 로딩을 따로 할수있으니 제한두기
    public async Task StartLoadingSingleLoder<T>(GenericDataLoader<T> loder) where T : Object
    {
        await loder.LoadAllDatasByLabel();
    }

    /// <summary>
    /// 모든 비동기 데이터 로드
    /// </summary>
    public async Task LoadAllDataLoaders()
    {
        if (LoadComplete) return;

        _loadingTasks.Clear();

        _loadingTasks.Add("EventData", eventDataLoader.LoadAllDatasByLabel());
        _loadingTasks.Add("ItemData", itemDataLoader.LoadAllDatasByLabel());
        _loadingTasks.Add("RoomEnemySpawnData", roomEnemySpawnDataLoader.LoadAllDatasByLabel());
        _loadingTasks.Add("UnitData", unitDataLoader.LoadAllDatasByLabel());

        await Task.WhenAll(_loadingTasks.Values);

        if (RpgManager.Instance != null)
        {
            RpgManager.Instance.Database.events = eventDataLoader.LoadedDatas;
            RpgManager.Instance.Database.Items = itemDataLoader.LoadedDatas;
            RpgManager.Instance.Database.StageData = roomEnemySpawnDataLoader.LoadedDatas;
            RpgManager.Instance.Database.Units = unitDataLoader.LoadedDatas;
        }
        else
            Debug.LogWarning("RPG매니저 존재하지않음");

        LoadComplete = true;
    }

    public void Release()
    {
        eventDataLoader?.ReleaseHandle();
        itemDataLoader?.ReleaseHandle();
        roomEnemySpawnDataLoader?.ReleaseHandle();
        unitDataLoader?.ReleaseHandle();

        _loadingTasks.Clear();
        LoadComplete = false;
    }
}
