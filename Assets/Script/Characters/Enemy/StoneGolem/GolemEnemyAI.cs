using UnityEngine;
using UnityEngine.AI;

namespace GameDesign
{
    public class GolemEnemyAI : MonoBehaviour
    {
        public Transform player;
        private NavMeshAgent agent;
        private Animator animator;
        private StoneGolemStats stats;

        public float wakeUpRange = 5f;
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

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            stats = GetComponent<StoneGolemStats>();
            StopMovement(); // 确保初始状态停止
        }

        public bool IsAwake()
        {
            return isAwake;
        }

        void Update()
        {
            if (disableAI) // AI 被禁用时，停止一切行为
            {
                StopAllActions();
                return;
            }

            if (player == null) return;

            float distance = Vector3.Distance(transform.position, player.position);

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
                if (!isAttacking)
                {
                    StartAttack();
                }
            }
            else if (distance < detectionRange)
            {
                // 追踪玩家
                isChasing = true;
                isAttacking = false;
                stats.onAtk = isAttacking;
                animator.SetBool("walking", true);
                animator.SetBool("attacking", false);
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else if (distance > detectionRange * 1.5f)
            {
                // 停止追踪
                isChasing = false;
                animator.SetBool("walking", false);
                StopMovement();
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
    }
}
