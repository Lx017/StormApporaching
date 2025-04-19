using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDesign
{
    public class CombatOneState : CombatStateBase
    {
        public CombatOneState(PlayerCombatStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();


            stateMachine.Player.stats.onAtk = true;

            if (SoundEffectPlayer.shared != null)
                SoundEffectPlayer.shared.PlaySoundEffect("sweep3");



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
            if (stateMachine.ReusableData.CombatCount >= CombatData.ATKDefinetionData.CombatOneListSize)
            {
                stateMachine.ReusableData.CombatCount = 0;
                stateMachine.ReusableData.OnCombatOne = false;
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
