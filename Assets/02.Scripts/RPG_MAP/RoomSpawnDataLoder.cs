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
    /// �������� �ִ� �� ����Ȯ�� �����͸� �����´�
    /// </summary>
    /// <returns>�� ����Ȯ�� ������</returns>
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
                Debug.LogError("������ ã�� ����");
            }
        };
    }


    /// <summary>
    /// ��ü�� �̸��� ���ڸ� �������ش�
    /// �����͸� �������� ������� �����ϱ� ���ؼ� ���
    /// </summary>
    int ExtractNumberFromName(string name)
    {
        string number = new string(name.TakeWhile(char.IsDigit).ToArray());
        return int.TryParse(number, out int result) ? result : -1;
    }
}
