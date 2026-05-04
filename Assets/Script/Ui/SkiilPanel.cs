using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkiilPanel : MonoBehaviour,BasePanel
{
    public Button SkillCard;
    private Vector3 posbut;
    public int CardNum;
    public void Init()
    {
        EventCenter.Instance.Add(this, "CloseSkiiPanle", Close);
    }
    public void Open()
    {
        posbut = new Vector3(-300, 0, 0);
        List<CardsDate> cards = CardsConfig.instance.GetRandowCards(CardNum);
        for (int i = 0; i < CardNum; i++)
        {
            Button button = Instantiate<Button>(SkillCard);
            if (button != null)
            {
                button.GetComponent<Card>().Config = cards[i];
                button.transform.parent = transform;
                button.transform.GetComponent<RectTransform>().localPosition = posbut;
                button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text=cards[i].cardname;
                button.image.sprite = cards[i].cardsprite;
            }
            posbut.x += 400;

        }
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
