using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public enum EquipmetType
{
    none,
    head,
    body,
    foot,
    left,
    right
}
public class EqupmentService : MonoBehaviour
{
    private static EqupmentService instance;

    public static EqupmentService Instance
    {
        get
        {
            // 如果没有实例，自动去找，找不到就自己创建一个
            if (instance == null)
            {
                GameObject obj = new GameObject("EquipmentService");
                instance = obj.AddComponent<EqupmentService>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            EventCenter.Instance.Add(this, "EqupmentServiceInit", Init);
        }
        else
            Destroy(gameObject);
    }
    public EquipmentData data {  get; private set; }
    //TODO：背包的添加改为背包物品的添加而不是配置文件的添加
    public void Init()
    {
        data=new EquipmentData();
        data.Init();
        //EventCenter.Instance.Add<BagItem>(this, "AddEquipment", Add);
        EventCenter.Instance.Add(this, "EqupmentServiceClear", Clear);
    }
    public BagItem Add(BagItem item)
    {
        if (item.itemcfg.type != ItemType.equipment)
            return null;
        return data.Add(item);
    }
    public BagItem Remove(EquipmetType type)
    {
        //var temp = data.Remove(type);
        //if (temp != null)
        //{
        //    EventCenter.Instance.OnTriggerEven("BagAdd", temp);
        //}
        //EventCenter.Instance.OnTriggerEven("EquipmentUiRefresh");
        return data.Remove(type);
    }
    public void Clear()
    {
        data.Clear();
        EventCenter.Instance.OnTriggerEven("EquipmentUiRefresh");
    }
}
