using System.Collections.Generic;
using UnityEngine;

public class SkillComponent : MonoBehaviour, ISkillComponent
{
    private List<SkillData> _skills = new List<SkillData>();
    public IReadOnlyList<SkillData> Skills => _skills;

    private SkillExecutor _executor;
    private ICharacterCore _characterCore;

    public void Initialize(ICharacterCore characterCore)
    {
        _characterCore = characterCore;
        _executor = new SkillExecutor();
    }

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

    public void UseSkill(int index, ICharacterCore target)
    {
        if (!IsValid(index))
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[{this}] / 유효하지 않은 Index");
#endif
            return;
        }
        
        SkillData skill = _skills[index];
        _executor.Execute(skill, _characterCore, target);
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
