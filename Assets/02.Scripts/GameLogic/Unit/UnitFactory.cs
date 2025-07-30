using UnityEngine;
using ProjectD_and_R.Enums;
using ProjectD_and_R.Constants;
using Unity.Behavior;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using System;

public class UnitFactory
{
    private BehaviorGraph graph;

    public void Initialize()
    {
    }

    public IEnumerator LoadBehaviorGraph()
    {
        AsyncOperationHandle<BehaviorGraph> handle = Addressables.LoadAssetAsync<BehaviorGraph>(StringConstants.MainEnemyBehaviorGraphAddress);
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            graph = handle.Result;
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("BehaviorGraph 로딩 실패: " + StringConstants.MainEnemyBehaviorGraphAddress);
#endif
        }
    }

    public ICharacterCore GetUnit(CharacterData characterData)
    {
        GameObject unit = ObjectPoolManager.Instance.Get(StringConstants.DefaultUnitAddress);
        CharacterCore unitCore;
        unit.TryGetComponent<CharacterCore>(out unitCore);

        return ObjectPoolManager.Instance.Get(StringConstants.DefaultUnitAddress).GetComponent<CharacterCore>();
    }

    public ICharacterCore GetEnemyUnit(CharacterData characterData)
    {
        GameObject unit = ObjectPoolManager.Instance.Get(StringConstants.DefaultEnemyUnitAddress);

        unit.TryGetComponent<ICharacterCore>(out ICharacterCore unitCore);

        unitCore.SetData(characterData);

        return unitCore;
    }
}
