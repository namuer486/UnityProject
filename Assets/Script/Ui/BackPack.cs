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
    public Vector2 backpos = new Vector2(100, -100);
    public Transform itemPanel;

    private int Col = 7;
    private float Width = 100;
    private float Heigh = 100;
    private Vector3 scale;
    private Vector2 pos;
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
        pos = backpos;
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
                //TextMeshProUGUI num = newbagbutton.transform.Find("Num").GetComponent<TextMeshProUGUI>();
                //num.gameObject.SetActive(false);
                newbagbutton.gameObject.SetActive(true);
                newbagbutton.transform.SetParent(transform);
                newbagbutton.GetComponent<RectTransform>().anchoredPosition = pos;
                scale = newbagbutton.GetComponent<RectTransform>().localScale;
                buttons.Add(newbagbutton);
                pos += Vector2.right * Width * scale.x;

                Transform itemP = Instantiate(itemPanel);//物品面板
                itemP.gameObject.SetActive(false);
                newbagbutton.GetComponent<BackGrid>().itemPanel = itemP;
                itemP.SetParent(transform);
                newbagbutton.GetComponent<BackGrid>().Init();
                itemP.gameObject.SetActive(false);
            }
            pos.x = backpos.x;
            pos += Vector2.down * Heigh * scale.y;
        }
        //TODO:背包格子不能Col被整除的话会有遗漏
        Debug.Log("背包创建完毕");
    }
    private void ReShow()//刷新
    {
        bagDate = BackPackManager.instance.bagDate;
        for (int i = 0;i < bagDate.count;i++)
        {
            //TextMeshProUGUI num = buttons[i].transform.Find("Num").GetComponent<TextMeshProUGUI>();
            BackGrid backGrid = buttons[i].GetComponent<BackGrid>();
            backGrid.GridItemID = i;
            if (bagDate.items[i].itemcfg != null)
            {
                backGrid.GridItemcfg = bagDate.items[i].itemcfg;
                backGrid.Set();
                //if (bagDate.items[i].count > 1)
                //{
                //    num.text = bagDate.items[i].count.ToString();
                //    num.gameObject.SetActive(true);
                //    continue;
                //}
                //num.gameObject.SetActive(false);
                continue;
            }
            //num.gameObject.SetActive(false);
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
