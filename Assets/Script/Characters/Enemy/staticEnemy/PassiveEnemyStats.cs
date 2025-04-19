using UnityEngine;

namespace GameDesign
{
    public class PassiveEnemyStats : EnemyStats
    {
        private SoundEffectPlayer soundPlayer;

        protected override void Start()
        {
            base.Start();
            soundPlayer = FindObjectOfType<SoundEffectPlayer>();
        }

        public override void TakeDamage(float damageAmount, Vector3? knockBackForce = null, bool enableHitReaction = false)
        {
            base.TakeDamage(damageAmount, knockBackForce);

            if (soundPlayer != null)
            {
                soundPlayer.PlaySoundEffect("hit1");
            }
        }

        public override void Die()
        {
            if (soundPlayer != null)
            {
                soundPlayer.PlaySoundEffect("death");
            }

            base.Die();
            Destroy(gameObject);  // 直接销毁，无动画
        }
    }
}

