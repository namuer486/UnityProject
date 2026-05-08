using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BagItem//动态数据部分
{

    public BagItem(ItemConfig itemcfg, int count)
    {
        this.itemcfg = itemcfg;
        this.count = count;
    }
    public ItemConfig itemcfg { get; set; }
    public int count { get; set; }

    public void Clear()
    {
        itemcfg = null;
        count = 0;
    }
}

