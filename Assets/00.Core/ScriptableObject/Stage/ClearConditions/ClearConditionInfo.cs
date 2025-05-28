using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClearConditionInfo : ScriptableObject
{
    [Header("클리어 기본 정보")]
    public string conditionName = "New Clear Condition";
    public string description = "Clear Condition desciption";

    public abstract System.Type GetConditionType();

}
