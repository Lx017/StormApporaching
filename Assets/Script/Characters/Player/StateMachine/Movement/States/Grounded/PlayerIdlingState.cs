using System;
using Unity.VisualScripting;
using UnityEngine;


namespace GameDesign
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            //stateMachine.ReusableData.MovementInput = Vector2.zero;
            stateMachine.ReusableData.maxSpeed = movementData.IdleData.maxSpeed;
        }

        public override void Update()
        {
            base.Update();


            if (stateMachine.ReusableData.MovementInput == Vector2.zero) { return; }


            OnMove();
        }

        
        #endregion




        #region Main Methods



        #endregion
    }

}