using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUi : MonoBehaviour,BasePanel
{
    private Button btnRestar;
    private Button btnBcak;
    private void Start()
    {
        btnRestar = transform.Find("ReStart").GetComponent<Button>();
        btnBcak = transform.Find("Menu").GetComponent<Button>();
        btnRestar.onClick.AddListener(Resart);
        btnBcak.onClick.AddListener(ReMenu);
    }
    private void Resart()
    {
        EventCenter.Instance.OnTriggerEven("GameBegin");
    }
    private void ReMenu()
    {
        EventCenter.Instance.OnTriggerEven("ComeBackMenu");
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
