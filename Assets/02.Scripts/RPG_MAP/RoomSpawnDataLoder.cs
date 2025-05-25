using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RoomSpawnDataLoder : MonoBehaviour
{
    string _Label = "RoomSpawnData";
    RoomSpawnData[] _SpawnData;

    /// <summary>
    /// 에디터의 있는 맵 생성확률 데이터를 가져온다
    /// </summary>
    /// <returns>맵 생성확률 데이터</returns>
    public void LoadRoomSpawnData(System.Action<RoomSpawnData[]> onLoaded)
    {
        Addressables.LoadAssetsAsync<RoomSpawnData>(_Label, null).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                List<RoomSpawnData> loadedList = new List<RoomSpawnData>(handle.Result);

                loadedList.Sort((a, b) =>
                {
                    int numA = ExtractNumberFromName(a.name);
                    int numB = ExtractNumberFromName(b.name);
                    return numA.CompareTo(numB);
                });

                _SpawnData = loadedList.ToArray();
                onLoaded?.Invoke(_SpawnData);
            }
            else
            {
                Debug.LogError("데이터 찾지 못함");
            }
        };
    }


    /// <summary>
    /// 객체의 이름의 숫자를 추출해준다
    /// 데이터를 가져오고 순서대로 정렬하기 위해서 사용
    /// </summary>
    int ExtractNumberFromName(string name)
    {
        string number = new string(name.TakeWhile(char.IsDigit).ToArray());
        return int.TryParse(number, out int result) ? result : -1;
    }
}
