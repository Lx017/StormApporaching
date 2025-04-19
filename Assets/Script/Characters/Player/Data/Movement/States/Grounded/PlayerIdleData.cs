using System;
using System.Collections;
using UnityEngine;

namespace GameDesign
{
    [Serializable]
    public class PlayerIdleData
    {

        [field: SerializeField][field: Range(0f, 10f)] public float maxSpeed { get; private set; } = 0f;
    }
}