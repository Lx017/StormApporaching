using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public List<RelicData> acquiredRelics = new List<RelicData>();
    [SerializeField] private RelicPool relicPool;

    public void Awake()
    {
        relicPool.ResetFlags();
    }
    public void AddRelic(RelicData data)
    {
        if (acquiredRelics.Contains(data)) return;

        acquiredRelics.Add(data);

        if (data.relicPrefab != null)
        {
            Instantiate(data.relicPrefab, transform.position + data.genOffset, Quaternion.identity, transform); // ��ѡ����λ�õ�
        }

        //Debug.Log($"[RelicManager] �ѻ��������{data.relicName}");
    }

    public bool HasRelic(string relicName)
    {
        foreach (var relic in acquiredRelics)
        {
            if (relic.relicName == relicName)
                return true;
        }
        return false;
    }
}