using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    private bool enable=false;
    private bool ispause=false;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.Add(this, "InputOpen", Enable);
        EventCenter.Instance.Add(this, "InputClose", Disable);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("输入状态" + enable);
        if(!enable)
            return;
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("打开背包");
            EventCenter.Instance.OnTriggerEven("PushBackPack");
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("打开面板");
            EventCenter.Instance.OnTriggerEven("PushPlayerPanel");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("游戏暂停");
            ispause = !ispause;
            if(ispause)
                EventCenter.Instance.OnTriggerEven("GamePause");
            else
                EventCenter.Instance.OnTriggerEven("GamePauseOver");
            EventCenter.Instance.OnTriggerEven("PushPuasePanel");
            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("打开技能选择");
            EventCenter.Instance.OnTriggerEven("PushCardsPanel");
            
        }
        
    }
    private void Enable() => enable = true;
    private void Disable() => enable = false;
}
