using UnityEngine;

namespace GameDesign
{
    public class IceGolemStats : EnemyStats
    {

        private Animator animator;
        private UnityEngine.AI.NavMeshAgent agent;
        private IceEnemyAI IceEnemyAI;
        private SoundEffectPlayer soundPlayer;



        protected override void Start()
        {
            base.Start();
            soundPlayer = FindObjectOfType<SoundEffectPlayer>();
            animator = GetComponent<Animator>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            IceEnemyAI = GetComponent<IceEnemyAI>();
        }

        public override void TakeDamage(float damageAmount, Vector3? knockBackForce = null, bool enableHitReaction = false)
        {
            base.TakeDamage(damageAmount, knockBackForce);
            IceEnemyAI.StopMovement();
            IceEnemyAI.disableAI = true;
            animator.SetBool("hit", true);

            if (soundPlayer != null){
                soundPlayer.PlaySoundEffect("hit1",this.distanceToPlayer());
            }
        }

        public void OnHitEnd()
        {
            animator.SetBool("hit", false);
            IceEnemyAI.disableAI = false;
        }

        public override void Die()
        {
            IceEnemyAI.StopMovement();
            IceEnemyAI.disableAI = true;
            animator.SetTrigger("death");
            if (soundPlayer != null)
            {
                soundPlayer.PlaySoundEffect("death");
            }
        }

        public void AfterDeath()
        {
            base.Die();
            Destroy(gameObject);
        }
    }
}
