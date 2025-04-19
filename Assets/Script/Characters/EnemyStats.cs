using UnityEngine;
using System.Collections.Generic;

namespace GameDesign
{
    public class EnemyStats : ActorStats
    {
        public float experienceReward = 20f;
        
        static public HashSet<EnemyStats> enemyStats = new HashSet<EnemyStats>();
        public bool onAtk = false;
        public static GameObject explosionEffectPrefab; 
        protected override void Awake()
        {
            base.Awake();
            enemyStats.Add(this);
        }
        protected override void Start(){
            base.Start();
            ScaleDifficultyWithTime();
            explosionEffectPrefab = Resources.Load<GameObject>("ExplosionSphere"); // no .prefab extension
            EnemyManager.Instance?.RegisterEnemy(this);
        }

        private void OnEnable()
        {
            
        }
        public override void Die()
        {
            enemyStats.Remove(this);
            EventManager.TriggerEvent<EnemyDiedEvent, EnemyStats>(this);
        }
        
        public float distanceToPlayer()//for audio player
        {
            Vector3 playerPos = PlayerStats.GetPlayerPosition();
            float distance = Vector3.Distance(playerPos, transform.position);
            return distance; 
        }

        public static void DoAOE(Vector3 center, float range, float damage, float aoeForce)
        {
            if (explosionEffectPrefab != null){
                GameObject fx = GameObject.Instantiate(explosionEffectPrefab, center, Quaternion.identity);
                ExplosionEffect effect = fx.GetComponent<ExplosionEffect>();
                if (effect != null)
                {
                    effect.maxScale = range * 1f * Random.Range(0.5f, 1.5f);
                }
            }
            float disToPlayer = Vector3.Distance(center, PlayerStats.GetPlayerPosition());
            float randomNumber = Random.Range(0f,4f);
            if (randomNumber < 1f)
            {
                SoundEffectPlayer.shared.PlaySoundEffect("explode2", disToPlayer);
            }
            else if (randomNumber < 2f)
            {
                SoundEffectPlayer.shared.PlaySoundEffect("explode3", disToPlayer);
            }
            else if (randomNumber < 3f)
            {
                SoundEffectPlayer.shared.PlaySoundEffect("explode4", disToPlayer);
            }
            else
            {
                SoundEffectPlayer.shared.PlaySoundEffect("explode1", disToPlayer);
            }

            foreach (EnemyStats enemy in enemyStats)
            {
                if (enemy == null) continue;

                float distance = Vector3.Distance(center, enemy.transform.position);
                if (distance <= range)
                {
                    // Deal damage
                    enemy.TakeDamage(damage);

                    // Apply outward force
                    Rigidbody rb = enemy.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 direction = (enemy.transform.position - center).normalized;
                        rb.AddForce(direction * aoeForce, ForceMode.Impulse);
                    }
                }
            }
        }

        void OnDestroy()
        {
            EnemyManager.Instance?.UnregisterEnemy(this);
        }

        public override void TakeDamage(float damageAmount, Vector3? knockBackForce = null, bool enableHitReaction = false)
        {
            base.TakeDamage(damageAmount, knockBackForce);
            EventManager.TriggerEvent<EnemyDamagedEvent, float>(damageAmount);
            Vector3 randomLocalPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(1.5f, 2.5f), Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + randomLocalPosition;
            FloatingTextManager.Instance.ShowFloatingText(spawnPosition, damageAmount);
        }

        private void ScaleDifficultyWithTime()
        {
            float elapsed = Time.timeSinceLevelLoad; // 游戏运行的时间（秒）

            float difficultyFactor = 1f + 0.2f * Mathf.Floor(elapsed / 60f); // 每分钟提升20%

            maxHealth *= difficultyFactor;
            currentHealth = maxHealth;

            attackPower *= difficultyFactor;
        }
    }
}
