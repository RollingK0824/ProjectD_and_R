using System.Collections.Generic;
using UnityEngine;

public class SkillComponent : MonoBehaviour, ISkillComponent
{
    private List<SkillData> _skills = new List<SkillData>();
    public IReadOnlyList<SkillData> Skills => _skills;

    public void AddSkill(SkillData skillData)
    {
        _skills.Add(skillData);
    }

    public void RemoveSkill(SkillData skillData)
    {
        _skills.Remove(skillData);
    }

    public bool HasSkill(SkillData skillData)
    {
        return _skills.Contains(skillData);
    }

    public void UseSkill(int index)
    {
        if (!IsValid(index))
        {
#if UNITY_EDITOR
            Debug.LogWarning($"유효하지 않은 Index");
#endif
        }
        
        SkillData skill = _skills[index];
#if UNITY_EDITOR
        Debug.Log($"{skill.skillName}사용");
#endif

    }

    private bool IsValid(int index)
    {
        if (0 > index || _skills.Count <= index)
        {
            return false;
        }
        return true;
    }
}
