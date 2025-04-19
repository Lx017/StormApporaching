using MxM;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDesign
{
    public class LightAttackState : CombatStateBase
    {
        private bool ableCombat;
        public LightAttackState(PlayerCombatStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            ableCombat = false;

            stateMachine.Player.stats.onAtk = true;

            if (SoundEffectPlayer.shared != null)
                SoundEffectPlayer.shared.PlaySoundEffect("sweep1");


        }

        public override void Exit()
        {
            base.Exit();
            stateMachine.Player.stats.onAtk = false;
            

        }
        #endregion




        #region Main Methods
        void OnCombatEventComplete()
        {
            stateMachine.Player.ReusableData.AtkCount += 1;

            if (stateMachine.Player.ReusableData.AtkCount >= CombatData.ATKDefinetionData.AtkListSize)
            {
                stateMachine.Player.ReusableData.AtkCount = 0;
            }

            stateMachine.ChangeState(stateMachine.AttackReadyState);
        }

        private void OnEventContact(int contactId)
        {

            switch (contactId)
            {
                case 0:
                    if (stateMachine.Player.ReusableData.AtkCount >=2)
                    {
                        break;
                    }
                    ableCombat = true;
                    break;
            }
        }


        #endregion



        #region Reusable Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.CombatSwitch.started += OnCombatSwitch;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.CombatSwitch.started -= OnCombatSwitch;
        }



        #endregion


        #region Input Methods
        protected virtual void OnCombatSwitch(InputAction.CallbackContext context)
        {
            if (!ableCombat) return;

            if (stateMachine.Player.ReusableData.AtkCount == 0)
            {
                stateMachine.ReusableData.OnCombatOne = true;
            } else if (stateMachine.Player.ReusableData.AtkCount == 1)
            {
                stateMachine.ReusableData.OnCombatTwo = true;
            }
        }

        #endregion


    }
}
