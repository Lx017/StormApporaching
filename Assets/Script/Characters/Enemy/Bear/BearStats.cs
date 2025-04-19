using UnityEngine;

namespace GameDesign
{
    public class BearStats : EnemyStats
    {

        private Animator animator;
        private UnityEngine.AI.NavMeshAgent agent;
        private BearAI BearAI;
        private SoundEffectPlayer soundPlayer;



        protected override void Start()
        {
            base.Start();
            soundPlayer = FindObjectOfType<SoundEffectPlayer>();
            animator = GetComponent<Animator>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            BearAI = GetComponent<BearAI>();
        }

        public override void TakeDamage(float damageAmount, Vector3? knockBackForce = null, bool enableHitReaction = false)
        {
            base.TakeDamage(damageAmount, knockBackForce);
            BearAI.StopMovement();
            // BearAI.disableAI = true;
            animator.SetBool("hitted", true);
            animator.SetBool("running", false);

            SoundEffectPlayer.play("horsehit1", 1f);
        }

        public void OnHitEnd()
        {
            animator.SetBool("hitted", false);
            animator.SetBool("walking", true);
            animator.SetBool("running",true);
            agent.isStopped = false; 
        
            // BearAI.disableAI = false;
        }

        public override void Die()
        {
            BearAI.StopMovement();
            // BearAI.disableAI = true;

            animator.SetBool("hitted", false);
            animator.SetBool("walking", false);
            animator.SetBool("running", false);
            animator.SetBool("attacking", false);

          
            animator.SetTrigger("Die");
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
