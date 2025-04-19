using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
namespace GameDesign
{
    public class BearAI : MonoBehaviour
    {
    

        public Transform player;
        private NavMeshAgent agent;
        private Animator animator;
        private BearStats stats;

        public float patrolRange = 30f;
        public LayerMask weaponLayer;

        public GameObject assignedWeapon; 
        private GameObject targetWeapon;
        private float chasingDistance = 10f;
        private float strikingDistance = 2f;
        private bool isStriking = false;

        public GameObject healthBarPrefab; //血条
        public GameObject uicanvas;
        private GameObject bar;


        private bool hasWeapon = false;
        private GameObject pickedUpWeapon; 

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            stats = GetComponent<BearStats>();

        


            if (assignedWeapon != null)
            {
                targetWeapon = assignedWeapon;
                PatrolWeapon();
            }
            else
            {
                Debug.LogWarning($"Warning: {gameObject.name} has no weapon assigned.");
                PatrolRandom();
            }

            if (healthBarPrefab != null && uicanvas != null)
            {
                bar = Instantiate(healthBarPrefab, uicanvas.transform);
                HealthBarUI barUI = bar.GetComponent<HealthBarUI>();
                barUI.target = stats;


            } // 亮血条
        }

        void Update()
        {
            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);




                if (distanceToPlayer <= strikingDistance)
                {
                    if (!isStriking)
                    {
                        isStriking = true;
                        stats.onAtk = true;
                        agent.isStopped = true;
                        agent.velocity = Vector3.zero;
                        animator.SetBool("striking", true);
                        animator.SetBool("walking", false);
                        animator.SetBool("running", false);
                    }
                    if(hasWeapon == false) {
                        strikingDistance = 1.5f;
                        Vector3 direction = (player.position - transform.position).normalized;
                        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
                    } else {
                        Transform wrist_R = FindDeepChild(transform, "Wrist_R");
                        if (wrist_R != null && player != null)
                        {
                            Vector3 wristToPlayer = player.position - wrist_R.position;
                            wristToPlayer.y = 0; // 保持水平旋转
                            if (wristToPlayer != Vector3.zero)
                            {
                                Quaternion lookRot = Quaternion.LookRotation(wristToPlayer.normalized);
                                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 10f);
                            }
                        }
                    }

                    return; // 正在攻击，不执行其他逻辑
                }
                else
                {
                    if (isStriking)
                    {
                        // 退出攻击
                        isStriking = false;
                    
                        agent.isStopped = false;
                    }
                }
            
            if (distanceToPlayer <= chasingDistance)
                {
                    agent.SetDestination(player.position);
                    animator.SetBool("running", true);
                    animator.SetBool("walking", false); // 防止动画冲突

                } else 
                {
                    animator.SetBool("running", false);
                    // animator.SetBool("striking", false);
                }
            }


            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (!hasWeapon && targetWeapon != null)
                    PatrolWeapon();
                else
                    PatrolRandom();
            }
        }



        private void PatrolWeapon()
        {
        

            Vector3 weaponPosition = targetWeapon.GetComponent<Collider>().ClosestPoint(agent.nextPosition);
            float realDistance = Vector3.Distance(agent.nextPosition, weaponPosition);

    

            agent.SetDestination(targetWeapon.transform.position);
            animator.SetBool("walking", true);
            Debug.Log($"{realDistance}");
            if ( realDistance < 1.0f) 
            {
                Debug.Log($"🐻 找到武器 {targetWeapon.name}，准备拾取");
                PickUpWeapon(targetWeapon);
            }


        }



        private void PickUpWeapon(GameObject weapon)
        {
            hasWeapon = true;
            pickedUpWeapon = weapon;
            stats.attackPower = 1f;

            Debug.Log($"🐻 {gameObject.name} 拾取了武器 {weapon.name}");
            AttachWeaponToBear();
        }

        private void AttachWeaponToBear()
        {
            if (pickedUpWeapon == null)
            {
                Debug.LogError($"❌ {gameObject.name} 的 pickedUpWeapon 为空，无法附加武器！");
                return;
            }

            Transform handTransform = FindDeepChild(transform, "weaponJnt");


            if (handTransform == null)
            {
                Debug.LogError($"❌ {gameObject.name} 没有找到 Hand 物体，无法附加武器！");
                return;
            }

            pickedUpWeapon.transform.SetParent(handTransform);
            pickedUpWeapon.transform.localPosition = Vector3.zero;
            pickedUpWeapon.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            pickedUpWeapon.SetActive(true);

            Debug.Log($"✅ {gameObject.name} 成功将武器 {pickedUpWeapon.name} 附加到 {handTransform.name}");

            PatrolRandom(); 
        }


        private void PatrolRandom()
        {
            Debug.Log($"🐻 巡逻");
            if (player == null) return;

            agent.SetDestination(player.position);
           
            animator.SetBool("walking", true);
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

        public void OnAttackAnimationEnd()
        {
            isStriking = false;
            stats.onAtk = false;
            
            agent.isStopped = false;
            animator.SetBool("striking", false);
            
        }

        public void StopMovement()
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

    }
}
