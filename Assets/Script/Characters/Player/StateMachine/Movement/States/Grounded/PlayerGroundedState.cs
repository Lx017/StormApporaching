using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDesign
{
    public class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }



        #region IState Methods
        public override void Update()
        {
            base.Update();

            if (stateMachine.Player.stats.onAtk)
            {
            }
            else
            {
            }
            
        }


        public override void Exit()
        {
            base.Exit();
        }



        #endregion




        #region Main Methods



        #endregion



        #region Reusable Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;

            stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;

            //stateMachine.Player.Input.PlayerActions.Sprint.started += OnSprintStarted;

            //stateMachine.Player.Input.PlayerActions.Sprint.canceled += OnSprintCancel;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;

            stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;

            //stateMachine.Player.Input.PlayerActions.Sprint.started -= OnSprintStarted;

            //stateMachine.Player.Input.PlayerActions.Sprint.canceled -= OnSprintCancel;

        }

     

        protected virtual void OnMove()
        {
            if (stateMachine.ReusableData.ShouldWalk)
            {
                stateMachine.ChangeState(stateMachine.WalkingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.RunningState);
        }




        #endregion



        #region Input Methods


        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        protected virtual void OnSprintStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.SprintingState);
        }

        protected virtual void OnSprintCancel(InputAction.CallbackContext context)
        {
            if (stateMachine.ReusableData.ShouldWalk)
            {
                stateMachine.ChangeState(stateMachine.WalkingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.RunningState);
        }


        protected virtual void OnJumpStarted(InputAction.CallbackContext context)
        {
            if (stateMachine.Player.stats.onAtk) return;
            stateMachine.ChangeState(stateMachine.JumpingState);
        }

        #endregion
    }

}