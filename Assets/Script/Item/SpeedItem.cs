using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItemUse : ItemUseStrategy
{
    public void Use(int value)
    {
        PlayerManager.instance.player.speed += value;
        Debug.Log("速度提升");
        //触发对应事件
    }
}
