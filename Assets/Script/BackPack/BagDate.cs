using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.Progress;


public class BagDate
{
    public int count = 21;
    public BagItem[] items {  get; set; }
    private List<int> stack;
    public ItemConfig NullConfig { get; set; }

    private int NowNum = 0;
    public BagDate()
    {
        items = new BagItem[count * 2];
        stack = new List<int>();
        for (int i = 0; i < count; i++)
        {
            stack.Add(count - i - 1);
        }
    }
    public void Add(BagItem item)
    {
        if (NowNum >= count)
        {
            NowNum = count;
            return;
        }
        if (item == null)
            return;
        int idx = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count - 1);
        items[idx] = item;
        NowNum++;
    }
    public void Use(int idx)
    {
        if (idx < 0 || idx > count)
            return;
        if (NowNum <= 0)
            return;
        items[idx].count--;
        if (items[idx].count <= 0)
            Remove(idx);
    }
    public void Remove(int idx)
    {
        if (idx < 0 || idx > count)
            return;
        if (NowNum <= 0)
            return;
        Debug.Log("背包移除物品" + items[idx].itemcfg.itemName + items[idx].count + "个");
        items[idx] = null;
        stack.Add(idx);
        NowNum--;
        
    }
    public void ExChange(int idx1,int idx2)
    {
        if (idx1 < 0 || idx1 > count|| idx2 < 0 || idx2 > count)
            return;
        if (items[idx2] == null)
        {
            items[idx2] = items[idx1];
            items[idx1] = null;
            stack.Remove(idx2);
            stack.Add(idx1);
            return;
        }
        var temp = items[idx1];
        items[idx1] = items[idx2];
        items[idx2] = temp;
    }
    public void Clear()
    {
        if (NowNum <= 0)
            return;
        stack.Clear();
        for (int i = 0; i < count; i++)
        {
            items[i]?.Clear();
            stack.Add(count - i - 1);
        }
    }

}

