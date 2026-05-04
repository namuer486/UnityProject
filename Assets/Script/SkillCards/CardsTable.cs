using System.Collections.Generic;
using UnityEngine;

public enum CardsType
{
    attack,
    hp,
    speed
}
[CreateAssetMenu(menuName = "Card/UpgradeCard")]
public class CardsConfig : ScriptableObject
{
    public static CardsConfig instance;
    public static CardsConfig Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = Resources.Load<CardsConfig>("CardsConfig");
            if (instance == null)
            {
                Debug.LogError("Resources 里找不到 CardsConfig！请检查文件是否放置正确");
            }
            return instance;
        }
    }
    public List<CardsDate> cardsDates = new List<CardsDate>();
    private Dictionary<string, CardsDate> keyValuePairs = new Dictionary<string, CardsDate>();

    public void SaveCards()
    {
        foreach (var card in this.cardsDates)
        {
            keyValuePairs.Add(card.cardname, card);
        }
    }
    public CardsDate GetCard(string name)
    {
        if (!keyValuePairs.ContainsKey(name))
            return null;
        return keyValuePairs[name];
    }
    public List <CardsDate> GetRandowCards(int number)
    {
        List<CardsDate> cards = new List<CardsDate>(cardsDates);
        List<CardsDate> selectcards=new List<CardsDate>(number);
        for (int i = 0; i < number; i++)
        {
            int idx = Random.Range(0, cards.Count);
            selectcards.Add(cards[idx]);
            cards.RemoveAt(idx);
        }
        return selectcards;
    }
}
[System.Serializable]
public class CardsDate
{
    public string cardname;
    public Sprite cardsprite;
    public CardsType cardtype;
    public int carddate;
}
