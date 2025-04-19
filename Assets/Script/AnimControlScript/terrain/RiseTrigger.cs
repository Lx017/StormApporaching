using UnityEngine;
using System.Collections;

public class RiseTrigger : MonoBehaviour
{
    public Animator animator; // �����ϵ� Animator ���
    private bool canTrigger = true; // �Ƿ���Դ�������
    private float lastTriggerTime = -12f; // ��¼�ϴδ�����ʱ��

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // �Զ���ȡ Animator ���
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        // ֻ�е�������� Player����������ʱ���Ŵ�������
        if (other.CompareTag("Player") && canTrigger)
        {
            Debug.Log("Hit2");
            animator.SetTrigger("StartRise"); // ��������
            canTrigger = false; // ��ֹ�����ٴδ���
            lastTriggerTime = Time.time; // ��¼�뿪��ʱ��
            StartCoroutine(ResetTriggerCooldown()); // ������ȴ��ʱ
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �� Player �뿪��ײ��ʱ����ʼ��ʱ
        //if (other.CompareTag("Player"))
        //{
            //lastTriggerTime = Time.time; // ��¼�뿪��ʱ��
            //StartCoroutine(ResetTriggerCooldown()); // ������ȴ��ʱ
        //}
    }

    private IEnumerator ResetTriggerCooldown()
    {
        yield return new WaitForSeconds(12f); // �ȴ� 12 ��
        canTrigger = true; // �����ٴδ���
    }
}
