using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : BaseItem
{
    public void Use(int value)
    {
        PlayerManager.instance.player.AKT += value;
        Debug.Log("攻击提升");
        //触发对应事件
    }
}
