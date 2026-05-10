using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EquipmentGrid : MonoBehaviour,IPointerClickHandler
{
    public EquipmetType type;
    public ItemConfig Config {  get; set; }


    public void refresh(BagItem item)
    {
        if(item == null)
        {
            GetComponent<Image>().sprite = null;
            return;
        }
        Config=item.itemcfg;
        GetComponent<Image>().sprite = Config.icon;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Config == null) return;
        var equipment = EqupmentService.Instance.Remove(Config.equipmet);
        if(equipment == null )
            return;
        BackPackManager.instance.Add(equipment);
        GetComponent<Image>().sprite = null;
        Config = null;
    }
}
