using GameDesign;
using UnityEngine;

public class StoneGolemStats : EnemyStats
{
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private GolemEnemyAI GolemEnemyAI;
    private SoundEffectPlayer soundPlayer;

    private int hitCounter = 0; // ÀÛ»ý¹¥»÷´ÎÊý
    private System.Random rng = new System.Random();

    protected override void Start()
    {
        base.Start();
        soundPlayer = FindObjectOfType<SoundEffectPlayer>();
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GolemEnemyAI = GetComponent<GolemEnemyAI>();
    }

    public override void TakeDamage(float damageAmount, Vector3 ? knockBackForce = null, bool enableHitReaction = false)
    {

        if (!GolemEnemyAI.IsAwake())
        {
            return;
        }

        base.TakeDamage(damageAmount, knockBackForce);

        if (!enableHitReaction)
        {
            if (soundPlayer != null)
            {
                soundPlayer.PlaySoundEffect("hit1", this.distanceToPlayer());
            }
            return;
        }

        hitCounter++;

        bool triggerHit = false;

        if (hitCounter == 3 || hitCounter == 4)
        {
            triggerHit = rng.NextDouble() < 0.33;
        }
        else if (hitCounter >= 5)
        {
            triggerHit = true;
        }

        if (triggerHit)
        {
            hitCounter = 0;
            GolemEnemyAI.StopMovement();
            GolemEnemyAI.disableAI = true;
            animator.SetBool("hit", true);
        }

        if (soundPlayer != null)
        {
            soundPlayer.PlaySoundEffect("hit1", this.distanceToPlayer());
        }
    }

    public void OnHitEnd()
    {
        animator.SetBool("hit", false);
        GolemEnemyAI.disableAI = false;
    }

    public override void Die()
    {
        GolemEnemyAI.StopMovement();
        GolemEnemyAI.disableAI = true;
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
