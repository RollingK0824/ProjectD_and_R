using System.Collections.Generic;
using UnityEngine;

public interface ISkillComponent
{
    IReadOnlyList<SkillData> Skills { get; }

    void AddSkill(SkillData skillData);
    void RemoveSkill(SkillData skillData);
    bool HasSkill(SkillData skillData);
    void UseSkill(int index);
}
