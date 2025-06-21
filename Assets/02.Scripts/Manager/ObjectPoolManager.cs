using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private Dictionary<string, GameObject> _prefabCache;
    private Dictionary<string, Queue<GameObject>> _availableObjects;
    private Transform _poolParent;

    public ObjectPoolManager(Transform parent)
    {
        _poolParent = parent;
    }
    public void Initialize(Transform parent)
    {
        _poolParent = parent;
        _prefabCache = new Dictionary<string, GameObject>();
        _availableObjects = new Dictionary<string, Queue<GameObject>>();
    }

    // 특정 주소의 프리팹을 미리 로드하여 풀 초기화 (비동기)
    public async void PreloadPrefab(string address, int defaultSize)
    {
        if (!_prefabCache.ContainsKey(address))
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle.Task; // 로딩 완료까지 대기

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _prefabCache[address] = handle.Result;
                _availableObjects[address] = new Queue<GameObject>();
                for (int i = 0; i < defaultSize; i++)
                {
                    CreateNewObject(address); // 미리 생성하여 풀에 추가
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"Failed to load Addressable asset at {address}: {handle.OperationException}");
#endif
            }
        }
    }


    public GameObject Get(string address)
    {
        if (!_availableObjects.ContainsKey(address) || _availableObjects[address].Count == 0)
        {
            // 풀에 오브젝트가 없으면 새로 생성 (프리팹 캐시에 있는지 확인)
            if (_prefabCache.ContainsKey(address))
            {
                CreateNewObject(address);
            }
            else
            {
                // 프리팹 자체가 아직 로드되지 않은 경우, 여기서 비동기 로드 요청
#if UNITY_EDITOR
                Debug.LogError($"Prefab for address {address} is not preloaded in the pool!");
#endif
                return null;
            }
        }

        GameObject obj = _availableObjects[address].Dequeue();
        return obj;
    }

    private GameObject CreateNewObject(string address)
    {
        GameObject prefab = _prefabCache[address];
        if (prefab == null) return null;

        GameObject newObj = Instantiate(prefab);
        if (newObj == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"Prefab {address} does not have component {typeof(GameObject).Name}!");
#endif
            Destroy(newObj);
            return null;
        }
        newObj.SetActive(false); // 생성 시 비활성화
        _availableObjects[address].Enqueue(newObj); // 생성 후 바로 풀에 추가
        return newObj;
    }

    public void Return(GameObject obj, string address)
    {
        obj.gameObject.SetActive(false);
        if (_availableObjects.ContainsKey(address))
        {
            _availableObjects[address].Enqueue(obj);
        }
        else
        {
            // 예외 처리: 존재하지 않는 풀에 반환 시도
#if UNITY_EDITOR
            Debug.LogWarning($"Attempted to return object to non-existent pool for address: {address}. Destroying object.");
#endif
            Object.Destroy(obj.gameObject);
        }
    }

    // 모든 프리팹 리소스 해제 (게임 종료 또는 씬 전환 시)
    public void ReleaseAllPrefabs()
    {
        foreach (var entry in _prefabCache)
        {
            Addressables.Release(entry.Value);
        }
        _prefabCache.Clear();
        _availableObjects.Clear(); // 풀도 비워야 함
    }
}