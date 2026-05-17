using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkiilPanel : MonoBehaviour,BasePanel
{
    public Button SkillCard;
    private Vector3 posbut;

    [SerializeField]
    private int cardnum;
    public int CardNum
    {
        get => cardnum;
    }
    public List<Button> Buttons {  get; private set; }
    public void Init()
    {
        EventCenter.Instance.Add(this, "CloseSkiiPanle", Close);
        Buttons = new List<Button>();
        posbut = new Vector3(-300, 0, 0);
        for (int i = 0; i < CardNum; i++)
        {
            Button button = Instantiate<Button>(SkillCard);
            if (button != null)
            {
                button.GetComponent<Card>().Config = null;
                button.transform.parent = transform;
                button.transform.GetComponent<RectTransform>().localPosition = posbut;
                button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = null;
                button.image.sprite = null;
            }
            posbut.x += 400;
            Buttons.Add(button);
        }
    }
    //TODO:为卡片添加独立的存储数据（卡片库）和卡片组件池
    public void Open()
    {
        
        List<CardsDate> cards = CardsConfig.instance.GetRandowCards(CardNum);//CardsDate只是静态数据CardConfig
        for (int i = 0; i < CardNum; i++)
        {
            Button button = Buttons[i];
            if (button != null)
            {
                button.GetComponent<Card>().Config = cards[i];
                button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text=cards[i].cardname;
                button.image.sprite = cards[i].cardsprite;
            }
        }
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
