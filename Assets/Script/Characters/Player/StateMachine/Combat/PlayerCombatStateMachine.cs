
using UnityEngine;

namespace GameDesign
{
    public class PlayerCombatStateMachine : StateMachine
    {
        public Player Player { get; }
        public LightAttackState LightAttackState { get; }
        public CombatNullState CombatNullState { get; }
        public AttackReadyState AttackReadyState { get; }
        public CombatOneState CombatOneState { get; }
        public CombatTwoState CombatTwoState { get; }
        public PlayerCombatStateReusableData ReusableData { get; private set; }



        public PlayerCombatStateMachine(Player player)
        {
            Player = player;
            ReusableData = new PlayerCombatStateReusableData();
            LightAttackState = new LightAttackState(this);
            CombatNullState = new CombatNullState(this);
            AttackReadyState = new AttackReadyState(this);
            CombatOneState = new CombatOneState(this);
            CombatTwoState = new CombatTwoState(this);

        }

    }
}
