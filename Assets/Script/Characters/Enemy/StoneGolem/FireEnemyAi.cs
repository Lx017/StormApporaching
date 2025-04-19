using UnityEngine;
using UnityEngine.AI;

namespace GameDesign
{
    public class FireEnemyAI : MonoBehaviour
    {
        public Transform player;
        private NavMeshAgent agent;
        private Animator animator;
        private FireGolemStats stats;

        public float wakeUpRange = 50f;
        public float throwRange = 20f;
        public float detectionRange = 100f;
        public float attackRange = 2f;

        private bool canStart = false;
        private bool isAwake = false;
        private bool isChasing = false;
        private bool isAttacking = false;

        public bool disableAI = false; // 新增变量，控制 AI 启用/禁用

        public GameObject healthBarPrefab; //血条
        public GameObject uicanvas;
        private GameObject bar;


        //Fireball
        private GameObject fireBallObj;
        private Rigidbody fireBallRb;

        private Vector3 originalLocalPos;
        private Quaternion originalLocalRot;
        private Transform originalParent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            stats = GetComponent<FireGolemStats>();
            StopMovement(); // 确保初始状态停止


            Transform found = FindDeepChild(transform, "FireBall");
            if (found != null)
            {
                fireBallObj = found.gameObject;

                // 添加 Rigidbody（如果没有）
                fireBallRb = fireBallObj.GetComponent<Rigidbody>();
                if (fireBallRb == null)
                    fireBallRb = fireBallObj.AddComponent<Rigidbody>();

                fireBallRb.isKinematic = true; // 初始不受物理影响
                fireBallRb.useGravity = false;

                 originalParent = fireBallObj.transform.parent;
                originalLocalPos = fireBallObj.transform.localPosition;
                originalLocalRot = fireBallObj.transform.localRotation;
            }
        }


        [SerializeField] private float throwForce = 80f;
        private bool hasThrown = false;

        


        void Update()
        {
            if (disableAI) // AI 被禁用时，停止一切行为
            {
                StopAllActions();
                return;
            }

            if (player == null) return;

            float distance = Vector3.Distance(transform.position, player.position);

            // if (hasThrown && fireBallObj.transform.position.y < 0.5f) // 简单的落地判断
            // {
            //     RecallFireBall(); // 自动回收
            // }

            // 唤醒逻辑
            if (!isAwake && distance < wakeUpRange)
            {
                isAwake = true;
                animator.SetBool("wakeUp", true);

                // Canvas worldCanvas = FindObjectOfType<Canvas>(); // 确保这是 World Space 的 Canvas
                if (healthBarPrefab != null && uicanvas != null)
                {
                    bar = Instantiate(healthBarPrefab, uicanvas.transform);
                    HealthBarUI barUI = bar.GetComponent<HealthBarUI>();
                    barUI.target = stats;


                } // 亮血条
            }

            if (!canStart) return;

            // 追踪与攻击逻辑

           if (distance < attackRange)
            {
                SetFireBallActive(true);
                if (!isAttacking)
                {
                    StartAttack();
                }
                SetFireBallActive(false);
            }
            else if (distance < throwRange)
            {
                if (!isAttacking) // 避免近战中还在扔球
                {
                    animator.SetBool("throw", true);
                    Invoke("ThrowAnimationEnd", 1.5f);
                    animator.SetBool("walking", true);

                    agent.isStopped = false;
                    agent.SetDestination(player.position);

                    stats.onAtk = true;
                    isChasing = true;
                }
            }
            else if (distance < detectionRange)
            {
                isChasing = true;
                isAttacking = false;
                stats.onAtk = false;

                animator.SetBool("walking", true);
                animator.SetBool("attacking", false);
                animator.SetBool("throw", false);

                agent.isStopped = false;
                agent.SetDestination(player.position);
            }



        }

        // 1. **停止移动 + 清除速度**
        public void StopMovement()
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        // 2. **完全禁用 AI**
        public void StopAllActions()
        {
            isChasing = false;
            isAttacking = false;
            stats.onAtk = isAttacking;
            animator.SetBool("walking", false);
            animator.SetBool("attacking", false);
            animator.SetBool("throw", false);

            StopMovement();
        }

        // 3. **进入攻击状态**
        private void StartAttack()
        {
            StopMovement();
            isAttacking = true;
            stats.onAtk = isAttacking;
            animator.SetBool("attacking", true);
            animator.SetBool("walking", false);
            animator.SetBool("throw",false);
            
        }

        public void OnAbleStart()
        {
            canStart = true;
            agent.isStopped = false;
        }

        // 4. **攻击动画结束时触发**
        public void OnAttackAnimationEnd()
        {
            isAttacking = false;
            stats.onAtk = isAttacking;
            animator.SetBool("attacking", false);

            if (disableAI) // 如果 AI 被禁用，停止攻击逻辑
            {
                StopAllActions();
                return;
            }

            if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                StartAttack(); // 继续攻击
            }
            else
            {
                agent.isStopped = false;
            }
        }

           public void ThrowAnimationEnd()
        {
            
            animator.SetBool("throw", false);
        }

    /// helper function
        private Transform FindDeepChild(Transform parent, string childName)
        {
            foreach (Transform child in parent)
            {
                if (child.name == childName)
                    return child;
                Transform result = FindDeepChild(child, childName);
                if (result != null)
                    return result;
            }
            return null;
        }

        public void ThrowFireBall()
        {

            stats.onAtk = true;
            if (fireBallObj == null || fireBallRb == null) return;
            EnemyDamageArea damageArea = fireBallObj.GetComponent<EnemyDamageArea>();
                if (damageArea != null)
                {
                    damageArea.owner = stats; //
                    damageArea.EnableCollider();
                }
   
            fireBallObj.transform.SetParent(null);

            // 开启物理模拟
            fireBallRb.isKinematic = false;
            fireBallRb.useGravity = true;

            // 朝玩家扔出去
            Vector3 direction = (player.position - fireBallObj.transform.position).normalized;
            fireBallRb.linearVelocity = direction * throwForce;
            hasThrown = true;
            Debug.Log("❄️ 火球投掷！");
        }

        public void RecallFireBall()
        {
            fireBallObj.SetActive(true); 
            stats.onAtk = false;
            hasThrown = false;

            fireBallRb.linearVelocity = Vector3.zero;
            fireBallRb.angularVelocity = Vector3.zero;
            fireBallRb.isKinematic = true;
            fireBallRb.useGravity = false;

            fireBallObj.transform.SetParent(originalParent);
            fireBallObj.transform.localPosition = originalLocalPos;
            fireBallObj.transform.localRotation = originalLocalRot;

            EnemyDamageArea damageArea = fireBallObj.GetComponent<EnemyDamageArea>();
            if (damageArea != null)
            {
                damageArea.DisableCollider();
            }


            Debug.Log("🔄 火球已回收至手上");
        }

        private void SetFireBallActive(bool isActive)
        {
            if (fireBallObj != null)
            {
                fireBallObj.SetActive(isActive);
            }
        }
    }
}