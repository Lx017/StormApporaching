using UnityEngine;

namespace GameDesign
{
    public class PlayerMovementStateReusableData
    {
        public Vector2 MovementInput { get; set; }




        public float MovementSpeedModifier { get; set; } = 1f;
        public float maxSpeed { get; set; } = 5f;

        public bool ShouldWalk { get; set; }
        public bool CombatSwitch { get; set; }



        private Vector3 currentTargetRotation;
        private Vector3 timeToReachTargetRotation;
        private Vector3 dampedTargetRotationCurrentVelocity;
        private Vector3 dampedTargetRotationPassedTime;

        public ref Vector3 CurrentTargetRotation
        {
            get
            {
                return ref currentTargetRotation;
            }
        }

        public ref Vector3 TimeToReachTargetRotation
        {
            get
            {
                return ref timeToReachTargetRotation;
            }
        }

        public ref Vector3 DampedTargetRotationCurrentVelocity
        {
            get
            {
                return ref dampedTargetRotationCurrentVelocity;
            }
        }

        public ref Vector3 DampedTargetRotationPassedTime
        {
            get
            {
                return ref dampedTargetRotationPassedTime;
            }
        }
    }
}
