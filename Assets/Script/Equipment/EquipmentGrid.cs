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
            return;
        Config=item.itemcfg;
        GetComponent<Image>().sprite = Config.icon;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Config == null) return;
        EqupmentService.Instance.Remove(Config.equipmet);
        GetComponent<Image>().sprite = null;
        Config = null;
    }
}
