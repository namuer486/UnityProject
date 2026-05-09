using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : MonoBehaviour
{
    private Dictionary<EquipmetType, BagItem> pairs;
    public void Init()
    {
        pairs = new Dictionary<EquipmetType, BagItem>();
    }
    public BagItem Add(BagItem item)
    {
        if(item == null)
            return null;
        var itemtype = item.itemcfg.equipmet;
        if (pairs.ContainsKey(itemtype))
        {
            //ÇÐ»»
            var bagItem = pairs[itemtype];
            pairs[itemtype] = item;
            return bagItem;
        }
        pairs.Add(itemtype, item);
        return null;
    }
    public BagItem Remove(EquipmetType type)
    {
        if(!pairs.ContainsKey(type))
        {
            Debug.Log("Î´×°±¸");
            return null;
        }
        var bagItem = pairs[type];
        pairs[type] = null;
        pairs.Remove(type);
        return bagItem;
    }
    public BagItem GetBagItem(EquipmetType type)
    {
        if(pairs.ContainsKey(type))
            return pairs[type];
        return null;
    }
    public void Clear()
    {
        pairs.Clear();
    }
}
