using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EquipmentGrid : MonoBehaviour,IPointerClickHandler
{
    public EquipmetType type;
    public ItemConfig Config {  get; set; }


    public void refresh()
    {
        if(Config == null) 
            return;
        GetComponent<Image>().sprite = Config.icon;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Pop();
    }
    public void Push(ItemConfig Config)
    {
        if (Config.type == ItemType.equipment && Config.equipmet == type)
        {
            this.Config = Config;
            EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
        }
    }
    public void Pop()
    {
        if(Config == null)
            return;
        EventCenter.Instance.OnTriggerEven("BagAdd", Config, 1);
        Config = null;
        // Ù–‘œ¬Ωµ
    }
}
