using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;
public class ItemMake : MonoBehaviour
{
    public static ItemMake instance;

    public BaseItem item {  get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Init()
    {
        item = new BaseItem();
    }
    public BaseItem GetItem(ItemConfig itemDate)//物品加工
    {
        switch (itemDate.type)
        {
            case ItemType.hp:
                item.Made(new HpItemUse());
                return item;
                //TODO:返回对应物件实例
            case ItemType.attack:
                item.Made(new AttackItemUse());
                return item;
            default:
                return null;
        }
    }
    public BaseItem GetItem(CardsDate itemDate)//卡片加工
    {
        switch (itemDate.cardtype)
        {
            case CardsType.hp:
                item.Made(new HpItemUse());
                return item;
            //TODO:返回对应物件实例
            case CardsType.attack:
                item.Made(new AttackItemUse());
                return item;
            case CardsType.speed:
                item.Made(new SpeedItemUse());
                return item;
            default:
                return null;
        }
    }
}
