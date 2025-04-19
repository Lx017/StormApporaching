using MxM;
using System;
using UnityEngine;

namespace GameDesign
{
    public class PlayerJumpState : PlayerMovementState
    {
        public PlayerJumpState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            stateMachine.Player.ReusableData.OnJumping = true;

            Vector3 jumpDirection = (stateMachine.Player.transform.forward + Vector3.up).normalized * 4;
            stateMachine.Player.Rigidbody.AddForce(jumpDirection, ForceMode.Impulse);

        }


        public override void Exit()
        {
            base.Exit();
            stateMachine.Player.ReusableData.OnJumping = false;
        }

        

        public override void HandleInput()
        {
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        #endregion



        #region Input Methods

        protected virtual void OnCombatEventComplete()
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        #endregion
    }
}
