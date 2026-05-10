using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackManager : MonoBehaviour
{
    public static BackPackManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            EventCenter.Instance.Add(this, "BackPackManagerInit", Init);
        }
        else
            Destroy(this);
    }
    public BagDate bagDate { get; private set; }
    private int Bagitemnum = 0;
    private int tempidx = -1;
    public void Init()
    {
        bagDate = new BagDate();
        EventCenter.Instance.Add<BagItem>(this, "BagAdd", Add);
        EventCenter.Instance.Add<int>(this, "UseItem", UseItem);
        EventCenter.Instance.Add<int>(this, "ItemChangeSetNowIdx", SetNowIdx);
        EventCenter.Instance.Add<int>(this, "ItemChangeChangeIdx", ChangeIdx);
        EventCenter.Instance.Add(this, "BagClear", Clear);
    }
    public void Add(BagItem item)
    {
        bagDate.Add(item);
        EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
    }
    public void Remove(int idx)
    {
        bagDate.Remove(idx);
        EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
    }
    public void UseItem(int idx)
    {
        bagDate.Use(idx);
        EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
    }
    public void ExChange(int idx1,int idx2)
    {
        bagDate.ExChange(idx1,idx2);
        EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
    }
    //public void Add(ItemConfig itemcfg,int count=1)
    //{
    //    BagItem bagItem = null;
    //    foreach(var item in bagDate.items)
    //    {
    //        if(item.itemcfg!=null&&item.itemcfg.ItemID==itemcfg.ItemID&& item.count + count <= itemcfg.maxCount)
    //        {
    //            bagItem = item;
    //            break;
    //        }
    //    }
    //    if(bagItem!=null)
    //    {
    //        bagItem.count += count;
    //        return;
    //    }
    //    if (Bagitemnum >= bagDate.count)
    //    {
    //        Debug.Log("背包已满");
    //        return;
    //    }
    //    BagItem newbagItem = new BagItem(itemcfg, count);
    //    bagDate.Add(newbagItem);
    //    Bagitemnum++;
    //    EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");

    //}
    //public void Add(BagItem item)
    //{
    //    BagItem bagItem = null;
    //    var BagCfg = item.itemcfg;
    //    var count = item.count;
    //    foreach(var it in bagDate.items)
    //    {
    //        if(it.itemcfg!=null&& it.itemcfg.ItemID== BagCfg.ItemID&& item.count + count <= BagCfg.maxCount)
    //        {
    //            bagItem = item;
    //            break;
    //        }
    //    }
    //    if(bagItem!=null)
    //    {
    //        bagItem.count += count;
    //        return;
    //    }
    //    if (Bagitemnum >= bagDate.count)
    //    {
    //        Debug.Log("背包已满");
    //        return;
    //    }
    //    BagItem newbagItem = new BagItem(itemcfg, count);
    //    bagDate.Add(newbagItem);
    //    Bagitemnum++;
    //    EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");

    //}
    //public void UseItem(int itemidx)
    //{
    //    if(itemidx < 0 || itemidx >= bagDate.count)
    //    {
    //        return;
    //    }
    //    if (bagDate.items[itemidx].itemcfg==null)
    //    {
    //        Debug.Log("没有物品");
    //        return; 
    //    }
    //    bagDate.items[itemidx].count--;
    //    if (bagDate.items[itemidx].count <= 0)
    //    {
    //        Debug.Log("背包销毁物品" + bagDate.items[itemidx].itemcfg.itemName);
    //        ItemMake.instance.GetItem(bagDate.items[itemidx].itemcfg)?.Use(bagDate.items[itemidx].itemcfg.effectNum);
    //        bagDate.Remove(itemidx);
    //        Bagitemnum--;
    //        EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
    //        return;
    //    }
    //    ItemMake.instance.GetItem(bagDate.items[itemidx].itemcfg)?.Use(bagDate.items[itemidx].itemcfg.effectNum);
    //    Debug.Log("背包减少物品" + bagDate.items[itemidx].itemcfg.itemName + "剩余数量" + bagDate.items[itemidx].count);
    //    EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
    //}
    public void SetNowIdx(int nowidx) 
    {
        if (nowidx < 0 || nowidx >= bagDate.count)
            return;
        Debug.Log("开始移动");
        tempidx = nowidx;
    }
    public void ChangeIdx(int changeidx)
    {
        if (tempidx < 0||changeidx < 0 || changeidx >= bagDate.count)
            return;
        (bagDate.items[tempidx], bagDate.items[changeidx]) = (bagDate.items[changeidx], bagDate.items[tempidx]);
        tempidx = -1;
        EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
        Debug.Log("移动结束");
    }
    private void Clear()
    {
        bagDate.Clear();
        Bagitemnum = 0;
        EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
    }
}
