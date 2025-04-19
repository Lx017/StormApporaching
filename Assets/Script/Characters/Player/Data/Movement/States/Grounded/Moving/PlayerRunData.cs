using System;
using UnityEngine;

namespace GameDesign
{
    [Serializable]
    public class PlayerRunData
    {
        [field: SerializeField][field: Range(1f, 10f)] public float maxSpeed { get; private set; }
    }
}
