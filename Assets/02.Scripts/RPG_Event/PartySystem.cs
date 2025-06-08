using System.Collections.Generic;
using UnityEngine;

public class PartySystem : MonoBehaviour
{
    [SerializeField][Header("편성")] List<CharacterCore> Party = new List<CharacterCore>();
    [SerializeField][Header("편성 최대 인원")] int MaxMamberCount = 5;

    /// <summary>
    /// 맴버 추가 함수
    /// </summary>
    public void AddMember(CharacterCore character)
    {
        if (Party.Count < MaxMamberCount && !Party.Contains(character))
            Party.Add(character);
    }

    /// <summary>
    /// 맴버 삭제 함수
    /// </summary>
    public void RemoveMember(CharacterCore character)
    {
        if(Party.Contains(character))
            Party.Remove(character);
    }

    /// <summary>
    /// 파티 리셋 함수
    /// </summary>
    public void ResetMember()
    {
        Party.Clear();
    }

    /// <summary>
    /// 맴버 위치 변경 함수
    /// </summary>
    public void SwapMember(int indexA, int indexB)
    {
        CharacterCore temp = Party[indexA];
        Party[indexA] = Party[indexB];
        Party[indexB] = temp;
    }
}
