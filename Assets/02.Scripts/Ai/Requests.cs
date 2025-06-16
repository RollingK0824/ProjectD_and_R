using ProjectD_and_R.Enums;
using UnityEngine;

// 배치 액션 요청
public class DeployActionRequest : IActionRequest
{
    public ActionType Type => ActionType.Deploy;
    public Vector3 DeployPosition { get; }

    public DeployActionRequest(Vector3 deployPosition)
    {
        DeployPosition = deployPosition;
    }
}

public class MoveActionRequest : IActionRequest
{
    public ActionType Type => ActionType.Move;
    public Vector3 Destination { get; }

    public MoveActionRequest(Vector3 destination)
    {
        Destination = destination;
    }
}

public class MoveStopActionRequest : IActionRequest
{
    public ActionType Type => ActionType.Move;

}

// 공격 액션 요청
public class AttackActionRequest : IActionRequest
{
    public ActionType Type => ActionType.Attack;
    public GameObject Target { get; }

    public AttackActionRequest(GameObject target)
    {
        Target = target;
    }

    public AttackActionRequest()
    {
    }
}

// 스킬 사용 액션 요청
public class UseSkillActionRequest : IActionRequest
{
    public ActionType Type => ActionType.UseSkill;
    public ScriptableObject SkillData { get; }
    public Vector3 TargetPosition { get; }

    public UseSkillActionRequest(ScriptableObject skillData, Vector3 targetPosition)
    {
        SkillData = skillData;
        TargetPosition = targetPosition;
    }
}

// 피격 액션 요청
public class HitActionRequest : IActionRequest
{
    public ActionType Type => ActionType.Hit;
    public GameObject Attacker { get; }
    public float Damage { get; }

    public HitActionRequest(GameObject attacker, float damage)
    {
        Attacker = attacker;
        Damage = damage;
    }
}

public class DieActionRequest : IActionRequest
{
    public ActionType Type => ActionType.Die;
    
    public DieActionRequest()
    {
        /* Do Nothing */
    }
}
