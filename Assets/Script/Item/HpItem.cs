using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem: BaseItem
{
    public void Use(int value)
    {
        PlayerManager.instance.player.tank.HP += value;
        EventCenter.Instance.OnTriggerEven("HPUpDate", PlayerManager.instance.player.tank.HP);
        Debug.Log("生命恢复"+ PlayerManager.instance.player.tank.HP);
        //触发对应事件
    }
}
