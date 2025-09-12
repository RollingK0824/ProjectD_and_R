using UnityEngine;
using System.Collections.Generic;
using ProjectD_and_R.Enums;

[CreateAssetMenu(fileName = "NewStageCondition", menuName = "ScriptableObjects/Stage/Stage Condition Data")]
public class StageConditionData : ScriptableObject
{
    [System.Serializable]
    public class ConditionEntry
    {
        public GameEndConditionType type; // 게임 종료 조건 타입
        public int intValue;              // 정수형 값 (예: 처치할 적 수, 남은 시간)
        public float floatValue;          // 실수형 값 (예: 시간 제한)
        public List<GameObject>objectReference; // 오브젝트 참조 (예: 보스 오브젝트, 수비 타겟)
        public bool isClearCondition;     // 이 조건이 클리어 조건인지 (true) 또는 게임 오버 조건인지 (false)
    }

    [Header("스테이지 종료 조건 목록")]
    public List<ConditionEntry> conditions = new List<ConditionEntry>();
}