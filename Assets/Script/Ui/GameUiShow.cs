using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUiShow : MonoBehaviour,BasePanel
{
    // Start is called before the first frame update
    public int playerHP = 100;
    public int playerMP = 50;

    private TextMeshProUGUI HPText;
    private TextMeshProUGUI MPText;
    void Start()
    {
        EventCenter.Instance.Add<int>(this, "HPUpDate", HPUpdate);
        EventCenter.Instance.Add<int>(this, "MPUpDate", MPUpdate);
        HPText = transform.Find("HP").GetComponent<TextMeshProUGUI>();
        MPText = transform.Find("MP").GetComponent<TextMeshProUGUI>();
    }
    private void OnDestroy()
    {
        EventCenter.Instance.RemoveAll(this);
    }
    // Update is called once per frame
    void Update()
    {
        HPText.text = "HP" + playerHP;
        MPText.text = "MP" + playerMP;
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    private void HPUpdate(int value)
    {
        playerHP = value;
 
    }
    private void MPUpdate(int value)
    {
        playerMP = value;

    }
}
