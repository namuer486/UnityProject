using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem
{
    public ItemUseStrategy itemUse {  get; private set; }

    public void Made(ItemUseStrategy itemUse)
    {
        this.itemUse = itemUse;
    }
    public void Use(int value)
    {
        this.itemUse?.Use(value);
    }
}
