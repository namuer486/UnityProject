using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackGrid:MonoBehaviour,IPointerDownHandler,IPointerUpHandler, IPointerClickHandler
{
    public int GridItemID {  get; set; }
    public int PassWorld { private get; set; }


    private void Start()
    {
        Button button = GetComponent<Button>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GridItemID <= 0)
            return;
        EventCenter.Instance.OnTriggerEven("UseItem", GridItemID - PassWorld);
        Debug.Log("使用物品");
    }
    public void Clear()
    {
        GridItemID = -1;
    }
}
