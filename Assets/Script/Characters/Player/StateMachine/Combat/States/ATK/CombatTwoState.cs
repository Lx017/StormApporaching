using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDesign
{
    public class CombatTwoState : CombatStateBase
    {
        public CombatTwoState(PlayerCombatStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();


            stateMachine.Player.stats.onAtk = true;

            if (SoundEffectPlayer.shared != null)
                SoundEffectPlayer.shared.PlaySoundEffect("sweep2");



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
            stateMachine.ReusableData.CombatCount += 1;
            if (stateMachine.ReusableData.CombatCount >= CombatData.ATKDefinetionData.CombatTwoListSize)
            {
                stateMachine.ReusableData.CombatCount = 0;
                stateMachine.ReusableData.OnCombatTwo = false;
            }
            stateMachine.ChangeState(stateMachine.AttackReadyState);
        }


        #endregion



        #region Reusable Methods


        #endregion



        #region Input Methods


        #endregion


    }
}
