using MxM;
using System;
using System.Collections;
using UnityEngine;

namespace GameDesign
{
    [Serializable]
    public class PlayerAtkSettingData
    {
        [field: SerializeField] public float atkCD { get; private set; } // As seconds
    }
}