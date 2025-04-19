using System;
using UnityEngine;

namespace GameDesign
{
    [Serializable]
    public class PlayerWalkData
    {
        [field: SerializeField][field: Range(0f, 10f)] public float maxSpeed { get; private set; }
    }
}
