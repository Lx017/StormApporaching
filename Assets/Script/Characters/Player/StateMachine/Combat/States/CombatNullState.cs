using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDesign
{
    public class CombatNullState : CombatStateBase
    {

        private bool hasSwitched = false;
        public CombatNullState(PlayerCombatStateMachine stateMachine) : base(stateMachine)
        {
        }
        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            hasSwitched = false;
        }


        public override void Exit() { 
            base.Exit();
        }




        #endregion

        #region Main Methods
        private void OnEquipEventComplete()
        {
            //stateMachine.Player.ToggleWeapon();
            if (stateMachine.Player.ReusableData.OnJumping) return;

                stateMachine.ChangeState(stateMachine.AttackReadyState);
        }

        private void OnEventContact(int contactId)
        {
            switch (contactId)
            {
                case 0:
                    if (stateMachine.Player.ReusableData.OnJumping) break;

                    Debug.Log("Hit");
                    stateMachine.Player.ToggleWeapon();
                    break;
            }
        }

        #endregion




        #region Reusable Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.AttackSwitch.started += OnAttackSwitch;
        }



        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.AttackSwitch.started -= OnAttackSwitch;
        }


        #endregion




        #region Input Methods

        private void OnAttackSwitch(InputAction.CallbackContext context)
        {
            //stateMachine.ReusableData.CombatSwitch = !stateMachine.ReusableData.CombatSwitch;
            //if (stateMachine.ReusableData.CombatSwitch)
            //{
            //    stateMachine.Player.MxMAnimator.SetRequiredTag("Combat");
            //    //Debug.Log("combat");
            //}
            //else
            //{
            //    stateMachine.Player.MxMAnimator.ClearRequiredTags();
            //}


            //stateMachine.ChangeState(stateMachine.AttackReadyState);

            if (hasSwitched) return;

            hasSwitched = true;
        }

        #endregion
    }
}
