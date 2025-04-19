using GameDesign.Assets.Script.Characters.Player.Data.Movement.States.Grounded.Moving;
using System;
using UnityEngine;

namespace GameDesign
{
    [Serializable]
    public class PlayerGroundedData
    {
        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
        [field: SerializeField] public PlayerRunData RunData { get; private set; }
        [field: SerializeField] public PlayerIdleData IdleData { get; private set; }
        [field: SerializeField] public PlayerSprintData SprintData { get; private set; }
        [field: SerializeField] public PlayerMovementEventData MovementEventData { get; private set; }


    }
}
