using MxM;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace GameDesign
{
    [Serializable]
    public class PlayerATKDefinetionData
    {
        [field: SerializeField] public int AtkListSize; // 可在 Inspector 中手动调整列表大小

        [field: SerializeField] public List<MxMEventDefinition> AttackList = new List<MxMEventDefinition>();

        [field: SerializeField] public int CombatOneListSize; // 可在 Inspector 中手动调整列表大小

        [field: SerializeField] public List<MxMEventDefinition> CombatOneList = new List<MxMEventDefinition>();

        [field: SerializeField] public int CombatTwoListSize; // 可在 Inspector 中手动调整列表大小

        [field: SerializeField] public List<MxMEventDefinition> CombatTwoList = new List<MxMEventDefinition>();


        [field: SerializeField] public MxMEventDefinition equip { get; private set; }
        [field: SerializeField] public MxMEventDefinition unEquip { get; private set; }


    }
}
