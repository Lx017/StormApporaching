using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Relic/RelicPool")]
public class RelicPool : ScriptableObject
{
    public List<RelicData> allRelics;

    private bool[] selectedFlags;

    // 初始化标记数组（在游戏开始时调用一次）
    public void ResetFlags()
    {
        selectedFlags = new bool[allRelics.Count];
    }

    public List<RelicData> GetRandomRelics(int count)
    {
        List<RelicData> available = new();

        for (int i = 0; i < allRelics.Count; i++)
        {
            if (!selectedFlags[i])
                available.Add(allRelics[i]);
        }

        // 随机选 count 个
        List<RelicData> result = new();
        while (result.Count < count && available.Count > 0)
        {
            int index = Random.Range(0, available.Count);
            result.Add(available[index]);
            available.RemoveAt(index);
        }

        return result;
    }

    public void MarkAsSelected(RelicData selected)
    {
        int index = allRelics.IndexOf(selected);
        if (index >= 0 && selectedFlags != null && index < selectedFlags.Length)
        {
            selectedFlags[index] = true;
        }
    }
}