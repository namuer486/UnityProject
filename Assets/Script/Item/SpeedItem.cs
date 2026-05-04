using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : BaseItem
{
    public void Use(int value)
    {
        PlayerManager.instance.move.speed += value;
        Debug.Log("速度提升");
        //触发对应事件
    }
}
