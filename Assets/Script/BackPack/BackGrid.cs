using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class BackGrid:MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IDragHandler, IEndDragHandler
{
    public int GridItemID { get; set; }
    public ItemConfig GridItemcfg {  get; set; }

    public BagItem item { get; set; }

    private Image BackImage;

    private TextMeshProUGUI itemtext;
    public Transform itemPanel {  get; set; }
    private Button use;
    private Vector2 itemRect;


    private bool IsOpen {  get; set; }
    private bool IsMove {  get; set; }
    public void Init()
    {
        IsOpen = false;
        itemPanel.GetComponent<RectTransform>().anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition + Vector2.down * 100;
        itemtext = itemPanel.Find("ItemText").GetComponent<TextMeshProUGUI>();
        use = itemPanel.Find("Use").GetComponent<Button>();
        use.onClick.AddListener(Use);
    }
    public void Set(BagItem item)
    {
        this.item = item;
        GridItemcfg = item.itemcfg;
        itemPanel.SetAsLastSibling();
        BackImage = GetComponent<Image>();
        if (GridItemcfg == null)
        {
            BackImage.sprite = null;
            return;
        }
        BackImage.sprite = GridItemcfg.icon;
    }
    public void OnBeginDrag(PointerEventData eventData)//拖拽三件套TODO：完成物品的拖拽交换
    {
        if (GridItemcfg == null)
            return;
        IsMove=true;
        transform.SetAsLastSibling();
        GetComponent<Image>().raycastTarget = false;
        itemRect = GetComponent<RectTransform>().anchoredPosition;
        transform.position = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (GridItemcfg == null)
            return;
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (GridItemcfg == null)
            return;
        IsMove =false;
        GameObject gameObject = eventData.pointerEnter;
        BackGrid newgrid = gameObject.GetComponent<BackGrid>();
        if (gameObject != null&& newgrid != null)
        {
            BackPackManager.instance.ExChange(GridItemID, newgrid.GridItemID);
        }
        //装备判断
        EquipmentGrid equipmentgrid = gameObject.GetComponent<EquipmentGrid>();
        if (gameObject != null && equipmentgrid != null&&item.itemcfg.type==ItemType.equipment&&item.itemcfg.equipmet==equipmentgrid.type)
        {
            EqupmentService.Instance.Add(item);
            BackPackManager.instance.UseItem(GridItemID);
            EventCenter.Instance.OnTriggerEven("BackPackUiUpdate");
            EventCenter.Instance.OnTriggerEven("EquipmentUiRefresh");

            //装备服务
        }
        GetComponent<RectTransform>().anchoredPosition = itemRect;
        GetComponent<Image>().raycastTarget = true;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GridItemcfg == null )
            return;
        if (IsMove)
            return;
        if (GridItemcfg.type == ItemType.equipment)
            return;
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            itemtext.text = "道具名：" + GridItemcfg.itemName;
            itemPanel.gameObject.SetActive(true);
        }
        else
        {
            itemPanel.gameObject.SetActive(false);
        }
    }
    private void Use()
    {
        BackPackManager.instance.UseItem(GridItemID);
        itemPanel.gameObject.SetActive(false);
        IsOpen = !IsOpen;
        Debug.Log("使用物品" + (GridItemID));
    }
    public void Clear()
    {
        item = null;
        GridItemcfg = null;
        //GridItemID = -1;
        GetComponent<Image>().sprite = null;
        if (itemPanel != null)
            itemPanel.gameObject.SetActive(false);
    }
}
