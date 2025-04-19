using System;
using System.Collections;
using UnityEngine;

namespace GameDesign.Assets.Script.Characters.Player.Data.Movement.States.Grounded.Moving
{
    [Serializable]
    public class PlayerSprintData
    {

        [field: SerializeField][field: Range(1f, 10f)] public float maxSpeed { get; private set; }
    }
}