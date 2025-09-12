// Assets/Editor/InventoryEditor.cs
using UnityEditor;
using UnityEngine;
using System.Collections.Generic; // Dictionary를 사용하기 위해 추가
using System.Linq; // ToList()를 사용하기 위해 추가

// CustomEditor 어트리뷰트로 어떤 타입을 위한 커스텀 인스펙터인지 지정합니다.
// Inventory 클래스를 직접 인스펙터에서 보려면, Inventory를 필드로 가지는 MonoBehaviour에 적용해야 합니다.
// 예를 들어, RpgManager가 Inventory를 가지고 있으므로 RpgManager에 적용하겠습니다.
[CustomEditor(typeof(RpgManager))] // RpgManager 클래스의 인스펙터를 커스터마이즈합니다.
public class RpgManagerEditor : Editor
{
    // RpgManager 인스턴스에 접근
    private RpgManager rpgManager;

    private void OnEnable()
    {
        // 타겟 객체를 RpgManager 타입으로 캐스팅합니다.
        rpgManager = (RpgManager)target;
    }

    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 GUI를 그립니다 (다른 필드들을 위해).
        DrawDefaultInspector();

        // RpgManager의 inventory 필드가 null이 아닌지 확인합니다.
        if (rpgManager.inventory != null)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("--- 인벤토리 상세 정보 ---", EditorStyles.boldLabel);

            // 골드 표시
            EditorGUILayout.IntField("골드", rpgManager.inventory.Gold);

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("아이템 목록:", EditorStyles.boldLabel);

            // InventoryItems 딕셔너리의 내용을 표시합니다.
            // 딕셔너리는 직접 직렬화되지 않으므로, 리스트나 배열로 변환하여 보여줍니다.
            // 여기서는 각 아이템과 수량을 직접 표시합니다.
            if (rpgManager.inventory.InventoryItems != null && rpgManager.inventory.InventoryItems.Count > 0)
            {
                foreach (var pair in rpgManager.inventory.InventoryItems)
                {
                    if (pair.Key != null) // ItemData가 null이 아닌지 확인
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.ObjectField(pair.Key.name, pair.Key, typeof(ItemData), false);
                        EditorGUILayout.IntField("수량", pair.Value);
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("인벤토리가 비어 있습니다.");
            }
        }
        else
        {
            EditorGUILayout.LabelField("인벤토리 인스턴스가 없습니다.");
        }

        // 인스펙터 변경 사항을 적용하여 런타임 중에도 값이 업데이트되도록 합니다.
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target); // RpgManager 인스턴스가 변경되었음을 Unity에 알립니다.
        }
    }
}