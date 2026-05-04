using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaueseUi : MonoBehaviour,BasePanel
{
    private Button menu;
    private Button restart;

    public void Close()
    {
        gameObject.SetActive(false);
        menu.onClick.RemoveAllListeners();
        restart.onClick.RemoveAllListeners();
    }
    public void Open()
    {
        menu = transform.Find("Menu").GetComponent<Button>();
        restart = transform.Find("ReStart").GetComponent<Button>();
        gameObject.SetActive(true);
        menu.onClick.AddListener(() =>
        {
            EventCenter.Instance.OnTriggerEven("ComeBackMenu");
        });
        restart.onClick.AddListener(() =>
        {
            EventCenter.Instance.OnTriggerEven("GameBegin");
        });
    }
}
