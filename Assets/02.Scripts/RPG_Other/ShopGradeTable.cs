using System.Collections.Generic;

public class ShopGradeTable
{
    public readonly Dictionary<ShopGrade, Dictionary<ItemGrade, float>> probabilityTable = new Dictionary<ShopGrade, Dictionary<ItemGrade, float>>
    {
        { ShopGrade.Nomal, new Dictionary<ItemGrade, float>
            {
                { ItemGrade.Common, 0.6f },
                { ItemGrade.Uncommon, 0.3f },
                { ItemGrade.Rare, 0.1f }
            }
        },
        { ShopGrade.Rare, new Dictionary<ItemGrade, float>
            {
                { ItemGrade.Common, 0.2f },
                { ItemGrade.Uncommon, 0.5f },
                { ItemGrade.Rare, 0.2f },
                { ItemGrade.Unique, 0.1f }
            }
        },
        { ShopGrade.Special, new Dictionary<ItemGrade, float>
            {
                { ItemGrade.Uncommon, 0.5f },
                { ItemGrade.Rare, 0.3f },
                { ItemGrade.Unique, 0.15f },
                { ItemGrade.Epic, 0.05f }
            }
        },
        { ShopGrade.Legendary, new Dictionary<ItemGrade, float>
            {
                { ItemGrade.Rare, 0.45f },
                { ItemGrade.Unique, 0.3f },
                { ItemGrade.Epic, 0.2f },
                { ItemGrade.Legendary, 0.05f }
            }
        }
    };
}