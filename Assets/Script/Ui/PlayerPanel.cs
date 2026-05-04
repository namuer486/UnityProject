using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour,BasePanel
{
    private int Level = 0;
    private float Exp = 0;
    private int HP = 0;
    private int MP = 0;

    public float ExpImageUpSpeed = 1f;
    public int ExpLength = 250;

    private TextMeshProUGUI HPText;
    private TextMeshProUGUI MPText;
    private TextMeshProUGUI LevelText;
    private Image ExpImage;

    private void Start()
    {
        
    }
    private void Update()
    {
        HPText.text = "HP:" + HP;
        MPText.text = "MP:" + MP;
        LevelText.text = "Level:" + Level;

        Vector2 ExpUpdateSize = new Vector2(Exp/100f* ExpLength, ExpImage.rectTransform.sizeDelta.y);
        ExpImage.rectTransform.sizeDelta = ExpUpdateSize;
    }
    public void Init()
    {
        EventCenter.Instance.Add<int>(this, "HPUpDate", HPUpDate);
        EventCenter.Instance.Add<int>(this, "MPUpDate", MPUpDate);
        EventCenter.Instance.Add<int>(this, "LevelUpDate", LevelUpDate);
        EventCenter.Instance.Add<int>(this, "ExpUpDate", ExpUpDate);

        HPText = transform.Find("HP").GetComponent<TextMeshProUGUI>();
        MPText = transform.Find("MP").GetComponent<TextMeshProUGUI>();
        LevelText = transform.Find("Level").GetComponent<TextMeshProUGUI>();
        ExpImage = transform.Find("Exp").GetComponent<Image>();
    }
    private void HPUpDate(int value)
    {
        HP = value;
    }
    private void MPUpDate(int value)
    {
        MP = value;
    }
    private void LevelUpDate(int value)
    {
        Level = value;
    }
    private void ExpUpDate(int value)
    {
        Exp = (float)value;
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
