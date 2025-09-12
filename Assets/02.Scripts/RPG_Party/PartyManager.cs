using System.Collections.Generic;
using UnityEngine;

public class PartyManager : Singleton<PartyManager>
{
    [Header("파티 설정")]
    public int maxPartySize = 5;

    [Header("현재 파티 정보")]
    public List<EnemyCharacterData> currentParty;

    [Header("해금 유닛")]
    HashSet<int> UnLockedUnitIDs = new HashSet<int>();

    private void Start()
    {
        GetUnlockedUnitIDs();
        // 해금된 유닛을 기반으로 초기 파티 생성
        CreateInitialParty();
    }

    /// <summary>
    /// 해금 유닛 아이디 반환 함수
    /// </summary>
    void GetUnlockedUnitIDs()
    {
        List<EnemyCharacterData> unitDatas = RpgManager.Instance.Database.Units;

        foreach (EnemyCharacterData data in unitDatas)
        {
            if (!data.Lock)
                UnLockedUnitIDs.Add(data.id);
        }
    }

    /// <summary>
    /// 해금 유닛 리스트 반환 함수
    /// </summary>
    public List<EnemyCharacterData> GetUnlockedUnits()
    {
        List<EnemyCharacterData> unlockedUnits = new List<EnemyCharacterData>();
        List<EnemyCharacterData> unitDatas = RpgManager.Instance.Database.Units;
        if (unitDatas == null)
        {
            return unlockedUnits;
        }

        foreach (EnemyCharacterData data in unitDatas)
        {
            if (UnLockedUnitIDs.Contains(data.id))
            {
                unlockedUnits.Add(data);
            }
        }
        return unlockedUnits;
    }

    /// <summary>
    /// 기본 파티 생성 함수
    /// </summary>
    public void CreateInitialParty()
    {
        currentParty.Clear();
        List<EnemyCharacterData> unlockedList = GetUnlockedUnits();
        for (int i = 0; i < unlockedList.Count && i < maxPartySize; i++)
        {
            currentParty.Add(unlockedList[i]);
        }
    }

    /// <summary>
    /// 유닛 해금 함수
    /// </summary>
    /// <param name="id"></param>
    public void UnlockUnit(int id)
    {
        if (!UnLockedUnitIDs.Contains(id))
        {
            UnLockedUnitIDs.Add(id);
        }
    }

    /// <summary>
    /// 유닛 해금 여부 확인 함수
    /// </summary>
    public bool IsUnitUnlocked(int id)
    {
        return UnLockedUnitIDs.Contains(id);
    }

    /// <summary>
    /// 파티 멤버 추가
    /// </summary>
    public void AddPartyMember(int id)
    {
        if (currentParty.Count >= maxPartySize) return;
        if (!IsUnitUnlocked(id)) return;
        if (currentParty.Exists(member => member.id == id)) return;

        EnemyCharacterData Member = RpgManager.Instance.UnitSystem.GetUnitDataByID(id);

        if (Member != null)
            currentParty.Add(Member);
    }

    /// <summary>
    /// 파티 멤버 삭제
    /// </summary>
    public void RemovePartyMember(int id)
    {
        EnemyCharacterData Member = currentParty.Find(member => member.id == id);

        if (Member != null)
            currentParty.Remove(Member);
    }

    /// <summary>
    /// 파티 멤버를 새로운 멤버로 교체
    /// </summary>
    public void ReplacePartyMember(int idToReplace, int idToAdd)
    {
        int memberIndex = currentParty.FindIndex(member => member.id == idToReplace);
        if (memberIndex == -1) return;

        if (!IsUnitUnlocked(idToAdd)) return;

        if (currentParty.Exists(member => member.id == idToAdd)) return;

        EnemyCharacterData newMember = RpgManager.Instance.UnitSystem.GetUnitDataByID(idToAdd);
        if (newMember != null)
        {
            EnemyCharacterData oldMember = currentParty[memberIndex];
            currentParty[memberIndex] = newMember;
        }
    }

    /// <summary>
    /// 파티 내 두 멤버의 위치변경
    /// </summary>
    public void SwapPartyMembers(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= currentParty.Count ||
            indexB < 0 || indexB >= currentParty.Count)
            return;

        EnemyCharacterData temp = currentParty[indexA];
        currentParty[indexA] = currentParty[indexB];
        currentParty[indexB] = temp;
    }
}
