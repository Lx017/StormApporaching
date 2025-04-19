using UnityEngine;
namespace GameDesign
{
    public class CombatStateBase : IState
    {
        protected PlayerCombatStateMachine stateMachine;
        protected PlayerCombatData CombatData;

        protected CombatStateBase(PlayerCombatStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            CombatData = stateMachine.Player.Data.CombatData;
        }

        public virtual void Enter()
        {
            //Debug.Log("State: " + GetType().Name);
            AddInputActionsCallbacks();
            
            
        }
        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }
        public virtual void HandleInput() { }

        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitionEvent()
        {
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
        }

        public virtual void OnTriggerExit(Collider collider)
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Update() { }


        protected virtual void AddInputActionsCallbacks()
        {

        }

        protected virtual void RemoveInputActionsCallbacks()
        {

        }

        protected void StartAnimation(int animationHash)
        {
        }

        protected void StopAnimation(int animationHash)
        {
        }


    }
}
