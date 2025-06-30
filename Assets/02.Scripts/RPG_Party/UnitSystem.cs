using System.Collections.Generic;

[System.Serializable]
public class UnitSystem 
{
    /// <summary>
    /// 유닛 아이디 가져오기
    /// </summary>
    /// <param name="unitID"></param>
    /// <returns></returns>
    public EnemyCharacterData GetUnitDataByID(int unitID)
    {
        return RpgManager.Instance.Database.Units.Find(unit => unit.id == unitID);
    }

    /// <summary>
    /// 특정 종족 유닛들 반환
    /// </summary>
    public List<EnemyCharacterData> GetRaceToUnits(EnemyType race)
    {
        List<EnemyCharacterData> retunUnits = new List<EnemyCharacterData>();
        List<EnemyCharacterData> allUnitDatas = RpgManager.Instance.Database.Units;

        EnemyType currentType = EnemyType.Default;
        for (int i = 0; i < allUnitDatas.Count; i++)
        {
            if (allUnitDatas[i].Race == race)
                retunUnits.Add(allUnitDatas[i]);

            currentType = allUnitDatas[i].Race;
        }
        return retunUnits;
    }
}
