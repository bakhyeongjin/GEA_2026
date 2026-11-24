using UnityEngine;
using System.Collections.Generic;

public class BattleLog : MonoBehaviour
{
    public int[] damages = { 1200, 3500, 800, 12000, 3500, 950 };
    public int criticalLine = 3000;

    [ContextMenu("전투 기록")]
    void ShowBattleLog()
    {
        int total = 0;
        List<int> criticals = new List<int>();

        foreach (int damage in damages)
        {
            total += damage;

            if (damage >= criticalLine)
            {
                criticals.Add(damage);
            }
        }

        Debug.Log("총 데미지 " + total);
        Debug.Log("치명타 " + criticals.Count + " 회");

        foreach (int critical in criticals)
        {
            Debug.Log("치명타 " + critical);
        }
    }

    [ContextMenu("Set 1")]
    void Set1()
    {
        damages = new int[] { 1200, 3500, 800, 12000, 3500, 950 };
        criticalLine = 3000;
        ShowBattleLog();
    }

    [ContextMenu("Set 3")]
    void Set3()
    {
        damages = new int[] { 999999, 250000, 999999, 500000 };
        criticalLine = 300000;
        ShowBattleLog();
    }
}