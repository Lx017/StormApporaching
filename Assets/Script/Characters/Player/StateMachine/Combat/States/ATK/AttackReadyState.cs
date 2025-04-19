using MxM;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDesign
{
    public class AttackReadyState : CombatStateBase
    {

        private bool hasSwitched = false;
        private Coroutine resetComboCoroutine;
        public AttackReadyState(PlayerCombatStateMachine stateMachine) : base(stateMachine)
        {
        }


        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            hasSwitched = false;

            resetComboCoroutine = stateMachine.Player.StartCoroutine(ResetComboAfterDelay(2f));

            
        }
        public override void Exit() {
            base.Exit();


            if (resetComboCoroutine != null)
            {
                stateMachine.Player.StopCoroutine(resetComboCoroutine);
                resetComboCoroutine = null;
            }

        }

        

        public override void Update()
        {
            base.Update();
        }

        #endregion




        #region Timer Methods
        private IEnumerator ResetComboAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            stateMachine.Player.ReusableData.AtkCount = 0;
            stateMachine.ReusableData.CombatCount = 0;
            stateMachine.ReusableData.OnCombatOne = false;
            stateMachine.ReusableData.OnCombatTwo = false;
        }
        #endregion



        #region Main Methods
        private void OnUnEquipEventComplete()
        {
            //stateMachine.Player.ToggleWeapon();
            if (stateMachine.Player.ReusableData.OnJumping) return;
            stateMachine.ChangeState(stateMachine.CombatNullState);
        }

        private void OnEventContact(int contactId)
        {
            switch (contactId)
            {
                case 0:
                    //Debug.Log("UnEquip");
                    if (stateMachine.Player.ReusableData.OnJumping) break;
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
            stateMachine.Player.Input.PlayerActions.Attack.started += OnAttack;
            stateMachine.Player.Input.PlayerActions.CombatSwitch.started += OnCombatSwitch;
        }

        

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.AttackSwitch.started -= OnAttackSwitch;
            stateMachine.Player.Input.PlayerActions.Attack.started -= OnAttack;
            stateMachine.Player.Input.PlayerActions.CombatSwitch.started -= OnCombatSwitch;
        }




        #endregion




        #region Input Methods



        protected virtual void OnCombatSwitch(InputAction.CallbackContext context)
        {
            if (stateMachine.ReusableData.OnCombatOne)
            {
                stateMachine.ChangeState(stateMachine.CombatOneState);
            }
            if (stateMachine.ReusableData.OnCombatTwo)
            {
                stateMachine.ChangeState(stateMachine.CombatTwoState);
            }
        }

        protected virtual void OnAttackSwitch(InputAction.CallbackContext context)
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


            //stateMachine.ChangeState(stateMachine.CombatNullState);

            if (hasSwitched) return;

            hasSwitched = true;
            stateMachine.ReusableData.OnCombatOne = false;
            stateMachine.ReusableData.OnCombatTwo = false;
        }


        protected virtual void OnAttack(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.OnCombatOne = false;
            stateMachine.ReusableData.OnCombatTwo = false;
            stateMachine.ChangeState(stateMachine.LightAttackState);
        }

        #endregion
    }
}
