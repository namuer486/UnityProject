using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUi : MonoBehaviour, BasePanel
{
    // Start is called before the first frame update
    public Transform startbuttunpos;
    public Button start;
    void Start()
    {
        start = transform.Find("Start").GetComponent<Button>();
        start.onClick.AddListener(() =>
        {
            Debug.Log("°´Å¥°´ÏÂ");
            EventCenter.Instance.OnTriggerEven("GameBegin");
        });
    }
    private void OnDestroy()
    {
        EventCenter.Instance.RemoveAll(this);
    }

    // Update is called once per frame
    void Update()
    {
        
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
