using System;
using System.Collections;
using UnityEngine;
using MxM;

namespace GameDesign
{

    [Serializable]
    public class PlayerMovementEventData
    {
        [field: SerializeField] public MxMEventDefinition jumpEvent { get; private set; }
    }
}