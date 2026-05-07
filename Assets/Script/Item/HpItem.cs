using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem: BaseItem
{
    public void Use(int value)
    {
        PlayerManager.instance.player.HP += value;
        EventCenter.Instance.OnTriggerEven("HPUpDate", PlayerManager.instance.player.HP);
        Debug.Log("生命恢复"+ PlayerManager.instance.player.HP);
        //触发对应事件
    }
}
