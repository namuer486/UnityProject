using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUi : MonoBehaviour,BasePanel
{
    public EquipmentGrid head;
    public EquipmentGrid body;
    public EquipmentGrid foot;
    public EquipmentGrid left;
    public EquipmentGrid right;

    public void Init()
    {
        EventCenter.Instance.Add(this, "EquipmentUiRefresh", Refresh);
    }
    public void Refresh()
    {
        head.refresh();
        body.refresh();
        foot.refresh();
        left.refresh();
        right.refresh();
    }
   public void Open()
   {
        gameObject.SetActive(true);
   }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
