using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Card : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    public CardsDate Config {  get; set; }
    private BaseItem item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Config == null)
        {
            Debug.Log("ø®∆¨Œ¥ÃÌº”≈‰÷√Œƒº˛");
            return;
        }
        item = ItemMake.instance.GetItem(Config);
        item.Use(Config.carddate);
        EventCenter.Instance.OnTriggerEven("CloseSkiiPanle");
    }
}
