using UnityEngine;
using System.Collections;

public class RiseTrigger : MonoBehaviour
{
    public Animator animator; // 物体上的 Animator 组件
    private bool canTrigger = true; // 是否可以触发动画
    private float lastTriggerTime = -12f; // 记录上次触发的时间

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // 自动获取 Animator 组件
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        // 只有当进入的是 Player，且允许触发时，才触发动画
        if (other.CompareTag("Player") && canTrigger)
        {
            Debug.Log("Hit2");
            animator.SetTrigger("StartRise"); // 触发动画
            canTrigger = false; // 禁止立即再次触发
            lastTriggerTime = Time.time; // 记录离开的时间
            StartCoroutine(ResetTriggerCooldown()); // 启动冷却计时
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 当 Player 离开碰撞体时，开始计时
        //if (other.CompareTag("Player"))
        //{
            //lastTriggerTime = Time.time; // 记录离开的时间
            //StartCoroutine(ResetTriggerCooldown()); // 启动冷却计时
        //}
    }

    private IEnumerator ResetTriggerCooldown()
    {
        yield return new WaitForSeconds(12f); // 等待 12 秒
        canTrigger = true; // 允许再次触发
    }
}
