using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ItemMake : MonoBehaviour
{
    public static ItemMake instance;

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
    public BaseItem GetItem(ItemConfig itemDate)//物品加工
    {
        switch (itemDate.type)
        {
            case ItemType.hp:
                return new HpItem();
                //TODO:返回对应物件实例
            case ItemType.attack:
                return new AttackItem();
            default:
                return null;
        }
    }
    public BaseItem GetItem(CardsDate itemDate)//卡片加工
    {
        switch (itemDate.cardtype)
        {
            case CardsType.hp:
                return new HpItem();
                //TODO:返回对应物件实例
            case CardsType.attack:
                return new AttackItem();
            case CardsType.speed:
                return new SpeedItem();
            default:
                return null;
        }
    }
}
