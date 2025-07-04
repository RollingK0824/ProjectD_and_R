using System.Collections.Generic;
using UnityEngine;

public class ShopGradeTable
{
    public readonly Dictionary<ShopGrade, Dictionary<ItemGrade, float>> probabilityTable = new Dictionary<ShopGrade, Dictionary<ItemGrade, float>>
    {
        // 노말 상점: Common, Uncommon, Rare 등급 아이템 등장
        { ShopGrade.Nomal, new Dictionary<ItemGrade, float>
            {
                { ItemGrade.Common, 0.6f },    // 60%
                { ItemGrade.Uncommon, 0.3f },  // 30%
                { ItemGrade.Rare, 0.1f }       // 10%
            }
        },
        // 레어 상점: Common, Uncommon, Rare, Unique 등급 아이템 등장
        { ShopGrade.Rare, new Dictionary<ItemGrade, float>
            {
                { ItemGrade.Common, 0.2f },    // 20%
                { ItemGrade.Uncommon, 0.5f },  // 50%
                { ItemGrade.Rare, 0.2f },      // 20%
                { ItemGrade.Unique, 0.1f }     // 10%
            }
        },
        // 스페셜 상점: Uncommon, Rare, Unique, Epic 등급 아이템 등장
        { ShopGrade.Special, new Dictionary<ItemGrade, float>
            {
                { ItemGrade.Uncommon, 0.3f },  // 30%
                { ItemGrade.Rare, 0.4f },      // 40%
                { ItemGrade.Unique, 0.2f },    // 20%
                { ItemGrade.Epic, 0.1f }       // 10%
            }
        },
        // 레전더리 상점: Rare, Unique, Epic, Legendary 등급 아이템 등장
        { ShopGrade.Legendary, new Dictionary<ItemGrade, float>
            {
                { ItemGrade.Rare, 0.4f },      // 40%
                { ItemGrade.Unique, 0.3f },    // 30%
                { ItemGrade.Epic, 0.2f },      // 20%
                { ItemGrade.Legendary, 0.1f }  // 10%
            }
        }
    };
}
