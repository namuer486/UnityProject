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
    private EquipmentData data;

    public void Init()
    {
        EventCenter.Instance.Add(this, "EquipmentUiRefresh", Refresh);
        data = EqupmentService.Instance.data;
    }
    public void Refresh()
    {
        head.refresh(data.GetBagItem(EquipmetType.head));
        body.refresh(data.GetBagItem(EquipmetType.body));
        foot.refresh(data.GetBagItem(EquipmetType.foot));
        left.refresh(data.GetBagItem(EquipmetType.left));
        right.refresh(data.GetBagItem(EquipmetType.right));
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
