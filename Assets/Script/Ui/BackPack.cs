using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackPack : MonoBehaviour,BasePanel
{
    public BagDate bagDate;
    public Button bagbutton;
    public List<Button> buttons;

    private int Col = 7;
    private float Width = 100;
    private float Heigh = 100;
    private Vector3 scale;
    private Vector2 pos;

    private int PassWorld = 1332;//加密

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        pos = Vector2.zero;
        Debug.Log(pos.x+"  "+pos.y);
        buttons= new List<Button>();
        Create();
        EventCenter.Instance.Add(this, "BackPackUiUpdate", ReShow);

    }
    private void Create()
    {
        bagDate = BackPackManager.instance.bagDate;
        int Row = bagDate.count / Col;
        for (int i = 0; i < Row; i++)
        {
            for(int j = 0; j < Col; j++)
            {
                Button newbagbutton = Instantiate<Button>(bagbutton);
                TextMeshProUGUI num = newbagbutton.transform.Find("Num").GetComponent<TextMeshProUGUI>();
                num.gameObject.SetActive(false);
                newbagbutton.gameObject.SetActive(true);
                newbagbutton.transform.SetParent(transform);
                newbagbutton.GetComponent<RectTransform>().anchoredPosition = pos;
                scale = newbagbutton.GetComponent<RectTransform>().localScale;
                buttons.Add(newbagbutton);
                pos += Vector2.right * Width * scale.x;
            }
            pos.x = 0;
            pos += Vector2.down * Heigh * scale.y;
        }
        //TODO:背包格子不能Col被整除的话会有遗漏
        Debug.Log("背包创建完毕");
    }
    private void ReShow()
    {
        bagDate = BackPackManager.instance.bagDate;
        for (int i = 0;i < bagDate.count;i++)
        {
            TextMeshProUGUI num = buttons[i].transform.Find("Num").GetComponent<TextMeshProUGUI>();
            BackGrid backGrid = buttons[i].GetComponent<BackGrid>();
            if (bagDate.items[i].itemcfg != null)
            {
                buttons[i].image.sprite = bagDate.items[i].itemcfg.icon;
                //backGrid.GridItemID = bagDate.items[i].itemcfg.ItemID;
                backGrid.PassWorld = PassWorld;
                backGrid.GridItemID = PassWorld+i;//存入编号
                if (bagDate.items[i].count > 1)
                {
                    num.text = bagDate.items[i].count.ToString();
                    num.gameObject.SetActive(true);
                }
                continue;
            }
            buttons[i].image.sprite = null;
            backGrid.Clear();
        }
        Debug.Log("背包界面刷新完成");
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
