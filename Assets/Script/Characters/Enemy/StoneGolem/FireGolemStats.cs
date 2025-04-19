using UnityEngine;

namespace GameDesign
{
    public class FireGolemStats : EnemyStats
    {

        private Animator animator;
        private UnityEngine.AI.NavMeshAgent agent;
        private FireEnemyAI FireEnemyAI;
        private SoundEffectPlayer soundPlayer;



        protected override void Start()
        {
            base.Start();
            soundPlayer = FindObjectOfType<SoundEffectPlayer>();
            animator = GetComponent<Animator>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            FireEnemyAI = GetComponent<FireEnemyAI>();
        }

        public override void TakeDamage(float damageAmount, Vector3? knockBackForce = null, bool enableHitReaction = false)
        {
            base.TakeDamage(damageAmount, knockBackForce);
            FireEnemyAI.StopMovement();
            FireEnemyAI.disableAI = true;
            animator.SetBool("hit", true);

            if (soundPlayer != null){
                soundPlayer.PlaySoundEffect("hit1");
            }
        }

        public void OnHitEnd()
        {
            animator.SetBool("hit", false);
            FireEnemyAI.disableAI = false;
        }

        public override void Die()
        {
            FireEnemyAI.StopMovement();
            FireEnemyAI.disableAI = true;
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
