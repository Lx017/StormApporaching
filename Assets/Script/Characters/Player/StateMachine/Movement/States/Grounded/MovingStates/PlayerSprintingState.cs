using UnityEngine;
using UnityEngine.InputSystem;



namespace GameDesign
{
    public class PlayerSprintingState : PlayerGroundedState
    {
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.ReusableData.maxSpeed = movementData.SprintData.maxSpeed;

        }



        #endregion


        #region Reusable Methods
  


        #endregion



        #region Input Methods




        #endregion


    }


}