using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BagDate
{
    public int count = 21;
    public BagItem[] items {  get; set; }
    public ItemConfig NullConfig { get; set; }

    public BagDate()
    {
        items = new BagItem[count];
        for (int i = 0; i < count; i++)
        {
            items[i] = new BagItem(NullConfig, -1);
        }
    }
    public void Add(BagItem item,int idx)
    {
        items[idx].count=item.count;
        items[idx].itemcfg = item.itemcfg;
        Debug.Log("背包添加物品" + item.itemcfg.itemName + item.count + "个");
    }
    public void Remove(int idx)
    {
        items[idx].count = -1;
        items[idx].itemcfg = NullConfig;
    }

}

