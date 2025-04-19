using UnityEngine;

namespace GameDesign
{
    public abstract class ActorStats : MonoBehaviour
    {

        public float maxHealth = 100f;

        public float currentHealth = 100f;

        public float healthRegen = 0f;

        public float maxShield = 0f;

        public float currentShield = 0f;

        public float attackPower = 10f;

        public float defense = 0f;

        [Range(0f, 1f)]
        public float critRate = 0f;

        [Min(1f)]
        public float critMultiplier = 2f;

        public float moveSpeed = 5f;

        public float attackSpeed = 1f;

        public bool canBeKnockedBack = false;

        public bool isDead = false;

        private Rigidbody rb;

        protected virtual void Awake()
        {
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            currentShield = Mathf.Min(currentShield, maxShield);
        }

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.Log("No Rigidbody");
            }
        }

        protected virtual void Update()
        {
            if (currentShield < 0f)
            {
                currentShield = 0f;
            }
            if (healthRegen > 0f && currentHealth < maxHealth)
            {
                currentHealth += healthRegen * Time.deltaTime;
                currentHealth = Mathf.Min(currentHealth, maxHealth);
            }
        }

        public virtual void TakeDamage(float damageAmount, Vector3? knockBackForce = null, bool enableHitReaction = false)
        {
            damageAmount = Mathf.Max(0f, damageAmount * (1f - defense / (defense + 100f)));

            float remainingDamage = ApplyShield(damageAmount);

            currentHealth -= remainingDamage;

            //handle knock back
            if (knockBackForce.HasValue && canBeKnockedBack)
            {
                if (rb != null)
                {
                    rb.AddForce(knockBackForce.Value, ForceMode.Impulse);
                }
            }

            if (currentHealth <= 0f && isDead == false)
            {
                isDead = true;
                Die();
            }
        }

        protected virtual float ApplyShield(float incomingDamage)
        {
            if (currentShield <= 0f)
                return incomingDamage;

            float absorbed = Mathf.Min(currentShield, incomingDamage);
            currentShield -= absorbed;
            return incomingDamage - absorbed;
        }

        public abstract void Die();

        public void SetMaxHealth(float newMax)
        {
            maxHealth = Mathf.Max(0f, newMax);
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }

        public void AddMaxHealth(float amount)
        {
            maxHealth = Mathf.Max(0f, maxHealth + amount);
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }

        public void AddHealth(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }

        public void AddHealthRegen(float value)
        {
            healthRegen = Mathf.Max(0f, healthRegen + value);
        }

        public void AddMaxShield(float amount)
        {
            maxShield = Mathf.Max(0f, maxShield + amount);
            currentShield = Mathf.Min(currentShield + amount, maxShield);
        }

        public void AddShield(float amount)
        {
            currentShield += amount;
            currentShield = Mathf.Clamp(currentShield, 0f, maxShield);
        }
        public void AddMoveSpeed(float amount)
        {
            moveSpeed = Mathf.Max(0f, moveSpeed + amount);
        }
        public void SetMoveSpeed(float speed)
        {
            moveSpeed = Mathf.Max(0f, speed);
        }

        public void SetAttackSpeed(float newAttackSpeed)
        {
            attackSpeed = Mathf.Max(0f, newAttackSpeed);
        }

        public void SetAttackPower(float newValue)
        {
            attackPower = Mathf.Max(0f, newValue);
        }

        public void AddAttackPower(float amount)
        {
            attackPower = Mathf.Max(0f, attackPower + amount);
        }

        public void SetDefense(float value)
        {
            defense = Mathf.Max(0f, value);
        }

        public void AddDefense(float value)
        {
            defense = Mathf.Max(0f, value + defense);
        }

        public void SetCritRate(float newRate)
        {
            critRate = Mathf.Clamp01(newRate);
        }

        public void AddCritRate(float amount)
        {
            critRate = Mathf.Clamp01(critRate + amount);
        }

        public void SetCritMultiplier(float multiplier)
        {
            critMultiplier = Mathf.Max(1f, multiplier);
        }

        public void AddCritMultiplier(float amount)
        {
            critMultiplier = Mathf.Max(1f, critMultiplier + amount);
        }

        public bool IsCriticalHit()
        {
            return Random.value < critRate;
        }
    }
}
